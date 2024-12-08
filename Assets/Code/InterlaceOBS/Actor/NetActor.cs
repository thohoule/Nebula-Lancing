using System.Collections;
using UnityEngine;
using TeaSteep.Character;
using TeaSteep.Character.Controller;
using FishNet.Object;
using Assets.Code.Characters.Weapons;

namespace Assets.Code.Characters
{
    public class NetActor : NetPrefabAsset
    {
        private float deathSpin;

        [SerializeField]
        private ParticleSystem damagedParticles;
        [SerializeField]
        private MultiParticles deathEffect;
        [SerializeField]
        private int health;
        [SerializeField]
        private int maxHealth;
        [SerializeField]
        private int shield;
        [SerializeField]
        private int maxShield;
        [SerializeField]
        private int shieldRefreshRate;
        [SerializeField]
        private float shieldRefreshDelay;
        [SerializeField]
        private bool isDead;

        public ActorController Controller { get; private set; }
        //public BaseWeapon PrimaryWeapon { get; set; }
        public ParticleSystem DamagedParticles { get => damagedParticles; set => damagedParticles = value; }
        public MultiParticles DeathEffect { get => deathEffect; set => deathEffect = value; }
        public int Health { get => health; internal set => health = value; }
        public int MaxHealth { get => maxHealth; internal set => maxHealth = value; }
        public int Shield { get => shield; internal set => shield = value; }
        public int MaxShield { get => maxShield; internal set => maxShield = value; }
        public int ShieldRefreshRate { get => shieldRefreshRate; internal set => shieldRefreshRate = value; }
        public float ShieldRefreshDelay { get => shieldRefreshDelay; internal set => shieldRefreshDelay = value; }
        public bool IsDead { get => isDead; internal set { isDead = value; if (value) onDeath(); } }

        private void Awake()
        {
            Controller = GetComponent<ActorController>();
        }

        private void Update()
        {
            if (IsOwner && !IsDead)
                Controller.ControllerUpdate();

            if (transform.position.y != 0)
                transform.position = new Vector3(transform.position.x,
                    0, transform.position.z);
        }

        #region Actor Death
        protected virtual void onDeath()
        {
            //death
            deathEffect?.PlayAll();
            damagedParticles?.Stop();
            StartCoroutine(playDeathAnimation());
        }

        private IEnumerator playDeathAnimation()
        {
            float deathTime = 0;
            Vector3 deathDirection = new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));

            while (deathTime < 4)
            {
                deathSpin = 5;
                var currentEuler = Controller.Local.TargetOrientation.eulerAngles;
                Controller.Local.TargetOrientation = Quaternion.Euler(0, currentEuler.y + deathSpin, 0);

                Controller.AddMove(deathDirection * 2);

                deathTime += Time.deltaTime;
                yield return null;
            }

            gameObject.SetActive(false);
        }
        #endregion

        #region Movement
        public void Move(Vector2 movement)
        {
            if (IsDead)
                return;

            Debug.Log(string.Format("I {0} got movement for {1}", gameObject.name,
                movement));

            var impulse = new Vector3(movement.x, 0, movement.y);
            //transmorm value to the rotation of the controller
            impulse = Controller.TransformVector(impulse);

            Controller.Impulse(impulse);
        }

        public void AimTowards(Vector3 target)
        {
            if (IsDead)
                return;

            var screenPos = new Vector3(target.x, target.y,
                Camera.main.transform.position.y);
            var position = Camera.main.ScreenToWorldPoint(screenPos);
            //position = new Vector3(position.x, 0, position.y);
            var direction = (position - transform.position).normalized;
            var degrees = Vector2.SignedAngle(Vector2.up, new Vector2(direction.x, direction.z));

            AimTowards(-degrees);
        }

        public void AimTowards(float degrees)
        {
            if (IsDead)
                return;

            Controller.Local.TargetOrientation = Quaternion.Euler(new Vector3(0, degrees, 0));
        }
        #endregion
    }
}
