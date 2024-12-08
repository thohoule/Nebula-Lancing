using TeaSteep.Character.Status.Effect;
using TeaSteep.Character.Status;
using UnityEngine;
using Assets.Game;
using TeaSteep.Character;
using Assets.Game.Character;
using Assets.Entities;

namespace Interlace
{
    public class AvatarCoord : MonoBehaviour, IGameEntity
    {
        private const float Shield_Tick = 1;

        [SerializeField]
        private AvatarHandler handler;

        protected IActorEntity entity { get => handler.Entity; }

        protected StatusManager status;
        protected EffectManager effects;
        //private ImplementedResolver resolver;
        private float shieldRefreshTime;
        private float shieldTickTime;
        private bool shieldIsBroken;

        public StatusManager Status { get { return status ?? (status = new StatusManager()); } }
        public EffectManager Effects { get { return effects ?? (effects = new EffectManager()); } }
        public int Health { get => entity.Health; set => handler.OnHealthChange(value); }
        public int MaxHealth { get => entity.MaxHealth; set => handler.OnMaxHealthChange(value); }
        public int Shield { get => entity.Shield; set => handler.OnShieldChange(value); }
        public int MaxShield { get => entity.MaxShield; set => handler.OnShieldChange(value); }
        public int ShieldRefreshRate { get => entity.ShieldRefreshRate; set => handler.Entity.ShieldRefreshRate = value; }
        public float ShieldRefreshDelay { get => entity.ShieldRefreshDelay; }
        public bool IsDead { get => entity.IsDead; set => handler.OnDeathChange(value); }
        public Weapon PrimaryWeapon { get; set; }
        public Weapon SecondaryWeapon { get; set; }

        public Vector3 Position { get => transform.position; }
        public Vector3 Forward { get { return transform.position + transform.forward; } }

        //public void Assign(ActorHandler handler, Weapon primary, 
        //    Weapon secondary)
        //{
        //    this.handler = handler;
        //    PrimaryWeapon = primary;
        //    SecondaryWeapon = secondary;
        //}

        public void Assign(AvatarHandler handler)
        {
            this.handler = handler;
        }

        private void Update()
        {
            if (transform.position.y != 0)
                transform.position = new Vector3(transform.position.x,
                    0, transform.position.z);

            if (!IsDead && Health <= 0)
            {
                IsDead = true;
            }
        }

        #region Damage and Refresh
        public void InflictDamage(int amount)
        {
            shieldRefreshTime = 0;

            var block = Shield - amount;

            if (block <= 0)//goes to health
            {
                if (Shield != 0)
                {
                    Shield = 0;
                }

                shieldIsBroken = true;
                handler.OnShieldCooldown();
                //PlayingControl.PlayingUI.ShieldCooldown.StartCooldown(blankHanlder,
                //    ShieldRefreshDelay * 3);
                shieldRefreshTime = 0;
                shieldTickTime = Shield_Tick;
                Health = Mathf.Clamp(Health + block, 0, 999);
            }
            else
            {
                shieldTickTime = Shield_Tick;
                Shield = block;
            }
        }

        private void refreshShield()
        {
            if (Shield != MaxShield)
            {
                bool canRecover = shieldIsBroken ?
                    shieldRefreshTime >= (ShieldRefreshDelay * 3) :
                    shieldRefreshTime >= ShieldRefreshDelay;

                if (canRecover)
                {
                    shieldIsBroken = false;

                    if (shieldTickTime >= Shield_Tick)
                    {
                        Shield = Mathf.Clamp(Shield + ShieldRefreshRate, 0, MaxShield);
                        shieldTickTime = 0;
                    }
                    else
                        shieldTickTime += Time.deltaTime;
                }
                else
                    shieldRefreshTime += Time.deltaTime;
            }
        }
        #endregion

        #region Command Handling
        internal void OnPrimaryFire(int charge)
        {
            if (PrimaryWeapon.CanFire(charge))
                PrimaryWeapon.Fire(charge);
        }

        internal void OnSecondaryFire(int charge)
        {
            if (SecondaryWeapon.CanFire(charge))
                SecondaryWeapon.Fire(charge);
        }
        #endregion

        #region Create
        public static void CreatCoord()
        {

        }
        #endregion
    }
}
