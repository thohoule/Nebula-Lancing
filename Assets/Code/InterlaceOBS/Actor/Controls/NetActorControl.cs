using UnityEngine;
using TeaSteep;
using TeaSteep.Character;
using Assets.Code.Characters;
using TeaSteep.Character.Status.Effect;
using TeaSteep.Character.Status;
using Assets.Code.Gameplay.PlayingSM;
using FishNet.Connection;
using Assets.Code.Characters.Weapons;

namespace Assets.Code.Characters
{
    public class NetActorControl : MonoBehaviour, IGameEntity
    {
        private const float Shield_Tick = 1;

        private NetActorHandler handler;
        private StatusManager status;
        private EffectManager effects;
        private ImplementedResolver resolver;
        private float shieldRefreshTime;
        private float shieldTickTime;
        private bool shieldIsBroken;

        public NetActor Actor { get => handler.Actor; }
        public StatusManager Status { get { return status ?? (status = new StatusManager()); } }
        public EffectManager Effects { get { return effects ?? (effects = new EffectManager()); } }
        public Vector3 Position { get => transform.position; }
        public Vector3 Forward { get { return transform.position + transform.forward; } }
        public BaseWeapon PrimaryWeapon { get; set; }
        public BaseWeapon SecondaryWeapon { get; set; }

        public int Health { get => Actor.Health; set => handler.OnHealthChange(null, value); }
        public int Shield { get => Actor.Shield; set => handler.OnShieldChange(null, value); }
        public int MaxShield { get => Actor.MaxShield; set => handler.OnShieldChange(null, value); }
        public int ShieldRefreshRate { get => Actor.ShieldRefreshRate; }
        public float ShieldRefreshDelay { get => Actor.ShieldRefreshDelay; }
        public bool IsDead { get => Actor.IsDead; set => handler.OnIsDeadChange(null, value); }

        private void Awake()
        {
            resolver = new ImplementedResolver(this);
        }

        #region Update
        private void Update()
        {
            //Actor.Controller.ControllerUpdate();
            //Controller.ControllerUpdate();

            if (transform.position.y != 0)
                transform.position = new Vector3(transform.position.x,
                    0, transform.position.z);

            if (!IsDead && Health <= 0)
            {
                IsDead = true;
            }
        }
        #endregion

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

        #region Sync
        public void Resync(NetworkConnection connection)
        {
            handler.OnHealthChange(connection, Health);
        }
        #endregion

        public static NetActorControl CreateControl(NetActorHandler handler)
        {
            var control = handler.gameObject.AddComponent<NetActorControl>();
            control.handler = handler;

            return control;
        }
    }
}
