using UnityEngine;
using TeaSteep;
using TeaSteep.Diagnostics;

namespace Assets.Code.Gameplay.Testing
{
    public class InverseStopTest : MonoBehaviour
    {
        [SerializeField]
        private bool run;
        [SerializeField]
        private bool runPhysics;
        [SerializeField]
        private Vector3 target;
        [SerializeField]
        private calcTypes calc;
        [SerializeField]
        private float inverseImpulse = 5;
        [SerializeField]
        private float velocity = 30;
        [SerializeField]
        private float preferredPower = 1;
        [SerializeField]
        private float preferredStopping = 1;
        [SerializeField]
        private bool overTrip;
        //[SerializeField, Range(0, 1)]
        //private float variable1;
        [SerializeField]
        private bool fullScale;

        [SerializeField, ReadOnly]
        private float timeTillGoal;
        [SerializeField, ReadOnly]
        private float inverseTime;
        [SerializeField, ReadOnly]
        private float xTime;
        [SerializeField, ReadOnly]
        private bool isStopping;
        [SerializeField, ReadOnly]
        private float rawCoe;
        [SerializeField, ReadOnly]
        private float impulseCoefficient;
        [SerializeField, ReadOnly]
        private float impulseAmount;
        [SerializeField, ReadOnly]
        private float deltaAmount;
        //[SerializeField, ReadOnly]
        //private float var1Out;
        [SerializeField, ReadOnly]
        private InverseFormulaDiagnostics diagnostics;

        public enum calcTypes
        {
            RawImpulse,
            PreferredImpulse,
            ImpulsePower,
            CoePower
        }

        //private void peek()
        //{
        //    var1Out = Mathf.Cos((Mathf.PI) * variable1);
        //}

        private void Update() //production code test
        {
            if (run)
            {
                float distance = Vector3.Distance(transform.position, target);

                impulseCoefficient = TeaMath.GetInversePower(distance, velocity,
                    inverseImpulse, preferredPower, preferredStopping, out diagnostics);

                if (runPhysics)
                {
                    impulseAmount = impulseCoefficient * inverseImpulse;
                    deltaAmount = impulseAmount * Time.deltaTime;
                    velocity = Mathf.Max(velocity - deltaAmount, 0);

                    var direction = (target - transform.position).normalized;
                    transform.position += direction * (velocity * Time.deltaTime);

                    if (distance < 0.1f)
                    {
                        runPhysics = false;
                        run = false;
                    }
                }
            }
        }

        #region Working Update Test Code
        //private void Update()
        //{
        //    if (run)
        //    {
        //        float distance = Vector3.Distance(transform.position, target);

        //        impulseCoefficient = GetInversePower(distance, velocity, inverseImpulse,
        //            preferredPower, preferredStopping, out bool overLimit);

        //        overTrip = overLimit;

        //        if (runPhysics)
        //        {
        //            impulseAmount = impulseCoefficient * inverseImpulse;
        //            deltaAmount = impulseAmount * Time.deltaTime;
        //            velocity = Mathf.Max(velocity - deltaAmount, 0);

        //            var direction = (target - transform.position).normalized;
        //            transform.position += direction * (velocity * Time.deltaTime);

        //            if (distance < 0.1f)
        //            {
        //                runPhysics = false;
        //                run = false;
        //            }
        //        }
        //    }
        //}
        #endregion

        #region Old Test Code
        private void oldTest()
        {
            if (run)
            {
                rawCoe = getImpulseCoefficient();

                if (fullScale)
                    //impulseCoefficient = Mathf.Clamp01((1 + rawCoe) / 2);
                    impulseCoefficient = Mathf.Clamp01(rawCoe + 1f);
                else
                    impulseCoefficient = rawCoe;

                if (impulseCoefficient >= preferredStopping)
                {
                    isStopping = true;
                    var preferredImpulse = inverseImpulse * preferredPower;
                    var impulsePower = impulseCoefficient * preferredImpulse;
                    var coePower = impulseCoefficient * inverseImpulse;

                    if (rawCoe > 0.5f && !overTrip)
                    {
                        overTrip = true;
                        runPhysics = false;
                    }

                    if (calc == calcTypes.RawImpulse)
                        impulseAmount = inverseImpulse;
                    else if (calc == calcTypes.PreferredImpulse)
                        impulseAmount = preferredImpulse;
                    else if (calc == calcTypes.ImpulsePower)
                        impulseAmount = impulsePower;
                    else
                        impulseAmount = coePower;

                    deltaAmount = impulseAmount * Time.deltaTime;

                    if (runPhysics)
                        velocity = Mathf.Max(velocity - deltaAmount, 0);
                }
                else
                    isStopping = false;

                if (runPhysics)
                {
                    var direction = (target - transform.position).normalized;
                    //(transform.position * -1).normalized;
                    transform.position += direction * (velocity * Time.deltaTime);
                }

                float distance = Vector3.Distance(transform.position, target);
                if (distance == 0)
                    run = false;
            }
        }
        #endregion

        private static float GetInversePower(float distance, float velocity, 
            float inverseImpulse, float preferredPower, float preferredStopping,
            out bool overLimit)
        {
            overLimit = false;
            if (velocity == 0 || inverseImpulse == 0 || distance == 0)
                return 0;

            var timeTillGoal = distance / velocity;
            var inverseTime = velocity / (inverseImpulse * preferredPower);

            if (timeTillGoal > inverseTime)
                return 0;

            var xTime = timeTillGoal / inverseTime;

            var rawCoe = Mathf.Cos(Mathf.PI * xTime);
            var recommendedPower = Mathf.Clamp01(rawCoe + 1);

            overLimit = rawCoe > 0.5f;

            if (recommendedPower >= preferredStopping)
                return recommendedPower;

            return 0;
        }

        private float getImpulseCoefficient()
        {
            if (velocity == 0)
                return -1;

            if (inverseImpulse == 0)
                return -1;

            float distance = Vector3.Distance(
                transform.position, target);
            timeTillGoal = distance / velocity;
            inverseTime = velocity / (inverseImpulse * preferredPower);

            if (timeTillGoal > inverseTime)
                return -1;

            xTime = timeTillGoal / inverseTime;

            if (fullScale)
                return Mathf.Cos(Mathf.PI * xTime);
            else
                return Mathf.Cos((Mathf.PI / 2) * xTime);
        }
    }
}
