//using System.Collections;
//using UnityEngine;
//using TeaSteep.Character;
//using TeaSteep.Character.Status;
//using TeaSteep.Character.Status.Effect;
//using TeaSteep.Character.Controller;
//using FishNet.Object.Synchronizing;
//using Assets.Code.Characters.Weapons;
//using Assets.Code.Gameplay;
//using Assets.Code.Gameplay.PlayingSM;

//namespace Assets.Code.Characters
//{
//    [RequireComponent(typeof(ActorController))]
//    public class NetActor : NetPrefabAsset, IGameEntity
//    {
//        private const float Shield_Tick = 1;

//        private StatusManager status;
//        private EffectManager effects;
//        private ImplementedResolver resolver;
//        private float shieldRefreshTime;
//        private float shieldTickTime;
//        private bool shieldBroke;
//        private float deathSpin;

//        [SerializeField]
//        private float turnSpeed = 1;
//        [SerializeField]
//        private ParticleSystem damagedParticles;
//        [SerializeField]
//        private MultiParticles deathEffect;

//        //protected Vector3 targetGoal;

//        public override bool TypeSearchEnabled => true;

//        public ActorController Controller { get; private set; }
//        public StatusManager Status { get { return status ?? (status = new StatusManager()); } }
//        public EffectManager Effects { get { return effects ?? (effects = new EffectManager()); } }
//        public Vector3 Forward { get { return transform.position + transform.forward; } }
//        public float TurnSpeed { get => turnSpeed; protected set => turnSpeed = value; }

//        public IPrimaryWeapon PrimaryWeapon { get; set; }
//        public BaseWeapon SecondaryWeapon { get; set; }
//        public ParticleSystem DamagedParticles { get => damagedParticles; set => damagedParticles = value; }
//        public MultiParticles DeathEffect { get => deathEffect; set => deathEffect = value; }

//        [SyncVar]
//        public PlayerClient owner;
//        [SyncVar]
//        public int Health;
//        [SyncVar]
//        public int MaxHealth;
//        [SyncVar]
//        public int Shield;
//        [SyncVar]
//        public int MaxShield;
//        [SyncVar]
//        public int ShieldRefreshRate;
//        [SyncVar]
//        public float ShieldRefreshDelay;
//        [SyncVar]
//        public bool IsDead;

//        public bool DualChargeEnabled { get; private set; }
//        public float DualChargeRate { get; private set; }
//        public float DualChargeValue { get; private set; }
//        public int GetHealth { get => Health; }
//        public int GetShield { get => Shield; }
//        public bool GetIsDead { get => IsDead; }
//        //public float PrimaryChargeRate { get; private set; }
//        //public float PrimaryCharge { get; private set; }

//        private void Awake()
//        {
//            Controller = GetComponent<ActorController>();
//            resolver = new ImplementedResolver(this);
//        }

//        private void Update()
//        {
//            //bypass();
//            //var location = Camera.main.WorldToScreenPoint(transform.position);
//            //var direction = (targetGoal - location).normalized;

//            //var degree = transform.eulerAngles.y;
//            //var goalDegree = Vector2.Angle(Vector2.up, direction);

//            //if (degree != goalDegree)
//            //{
//            //    var lerp = Mathf.Lerp(degree, goalDegree, TurnSpeed * Time.deltaTime);
//            //    Controller.Local.TargetOrientation = Quaternion.Euler(0, degree + lerp, 0);
//            //    //transform.eulerAngles = new Vector3(0, degree + lerp, 0);
//            //}

//            Controller.ControllerUpdate();

//            if (transform.position.y != 0)
//                transform.position = new Vector3(transform.position.x,
//                    0, transform.position.z);

//            if (IsServer)
//                refreshShield();

//            if (Health <= 0 && !IsDead)
//            {
//                //death
//                deathEffect?.PlayAll();
//                DamagedParticles?.Stop();
//                StartCoroutine(playDeathAnimation());
//                IsDead = true;
//            }
//            //else if (Health <= 50 && !deathEffect.IsPlaying)
//            //{
//            //    //deathEffect.StopAll();
//            //    //if (deathEffect.Systems[0].isStopped)
//            //        deathEffect?.PlayAll();
//            //}
//            else if (DamagedParticles != null)
//            {
//                if (!DamagedParticles.isPlaying && Health <= 30)
//                    DamagedParticles.Play();
//                else if (DamagedParticles.isPlaying && Health > 30)
//                    DamagedParticles.Stop();
//            }
//        }

//        public void InflictDamage(int amount)
//        {
//            shieldRefreshTime = 0;

//            var block = Shield - amount;

//            if (block <= 0)//goes to health
//            {
//                if (Shield != 0)
//                {
//                    Shield = 0;
//                }

//                shieldBroke = true;
//                PlayingControl.PlayingUI.ShieldCooldown.StartCooldown(blankHanlder,
//                    ShieldRefreshDelay * 3);
//                shieldRefreshTime = 0;
//                shieldTickTime = Shield_Tick;
//                Health = Mathf.Clamp(Health + block, 0, 999);
//            }
//            else
//            {
//                shieldTickTime = Shield_Tick;
//                Shield = block;
//            }
//        }

//        private IEnumerator playDeathAnimation()
//        {
//            float deathTime = 0;
//            Vector3 deathDirection = new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));

//            while (deathTime < 4)
//            {
//                deathSpin = 5;
//                var currentEuler = Controller.Local.TargetOrientation.eulerAngles;
//                Controller.Local.TargetOrientation = Quaternion.Euler(0, currentEuler.y + deathSpin, 0);

//                Controller.AddMove(deathDirection * 2);

//                deathTime += Time.deltaTime;
//                yield return null;
//            }

//            gameObject.SetActive(false);
//        }

//        private void blankHanlder()
//        {

//        }

//        private void refreshShield()
//        {
//            if (Shield != MaxShield)
//            {
//                bool canRecover = shieldBroke ?
//                    shieldRefreshTime >= (ShieldRefreshDelay * 3) :
//                    shieldRefreshTime >= ShieldRefreshDelay;

//                if (canRecover)
//                {
//                    shieldBroke = false;

//                    if (shieldTickTime >= Shield_Tick)
//                    {
//                        Shield = Mathf.Clamp(Shield + ShieldRefreshRate, 0, MaxShield);
//                        shieldTickTime = 0;
//                    }
//                    else
//                        shieldTickTime += Time.deltaTime;
//                }
//                else
//                    shieldRefreshTime += Time.deltaTime;
//            }
//        }

//        //private void bypass()
//        //{
//        //    var screenPos = new Vector3(targetGoal.x, targetGoal.y,
//        //        Camera.main.transform.position.y);
//        //    var position = Camera.main.ScreenToWorldPoint(screenPos);
//        //    //position = new Vector3(position.x, 0, position.y);
//        //    var direction =  (position - transform.position).normalized;
//        //    var degrees = Vector2.SignedAngle(Vector2.up, new Vector2(direction.x, direction.z));
//        //    //var rad = Mathf.Deg2Rad * degrees;
//        //    //var rotation = Quaternion.FromToRotation(transform.eulerAngles, direction);

//        //    //var rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(direction),
//        //    //    TurnSpeed * Time.deltaTime);
//        //    Controller.Local.TargetOrientation = Quaternion.Euler(new Vector3(0, -degrees, 0));
//        //}

//        public void Move(Vector2 movement)
//        {
//            var impulse = new Vector3(movement.x, 0, movement.y);
//            //transmorm value to the rotation of the controller
//            impulse = Controller.TransformVector(impulse);

//            Controller.Impulse(impulse);
//        }

//        public void AimTowards(Vector3 target)
//        {
//            var screenPos = new Vector3(target.x, target.y,
//                Camera.main.transform.position.y);
//            var position = Camera.main.ScreenToWorldPoint(screenPos);
//            //position = new Vector3(position.x, 0, position.y);
//            var direction = (position - transform.position).normalized;
//            var degrees = Vector2.SignedAngle(Vector2.up, new Vector2(direction.x, direction.z));

//            AimTowards(-degrees);
//        }

//        public void AimTowards(float degrees)
//        {
//            Controller.Local.TargetOrientation = Quaternion.Euler(new Vector3(0, degrees, 0));
//        }

//        //public void AimTowards(Vector2 target)
//        //{
//        //    targetGoal = target;
//        //}

//        public void ChargePrimary()
//        {
//            PrimaryWeapon.Charge();
//            //PrimaryCharge = Mathf.Clamp01(PrimaryCharge +
//            //    (PrimaryChargeRate * Time.deltaTime));
//        }

//        public void FirePrimary()
//        {
//            //Use Primary Action at PrimaryCharge amount
//            PrimaryWeapon.Fire();
//            //Debug.Log("Pew Pew");
//        }

//        public void ChargeSecondary()
//        {

//        }

//        public void FireSecondary()
//        {
//            SecondaryWeapon.Fire();
//        }

//        public void ChargeDual()
//        {
//            //Combo action 
//        }

//        public void FireDual()
//        {

//        }

//        //public void UpdateEffects()
//        //{
//        //}
//    }
//}
