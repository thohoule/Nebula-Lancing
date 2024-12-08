using UnityEngine;
using FishNet.Connection;
using FishNet.Object;
using Assets.Code.Gameplay;

namespace Assets.Code.Characters
{
    public class NetActorHandler : NetworkBehaviour
    {
        [SerializeField]
        private NetActor actor;

        public NetActor Actor { get => actor; protected set => actor = value; }
        internal NetActorControl Control { get; set; }

        #region Downstream
        [ObserversRpc]
        [TargetRpc]
        internal protected virtual void OnHealthChange(NetworkConnection connection,
            int value)
        {
            Actor.Health = value;
        }

        [ObserversRpc]
        [TargetRpc]
        internal protected virtual void OnMaxHealthChange(NetworkConnection connection,
            int value)
        {
            Actor.MaxHealth = value;
        }

        [ObserversRpc]
        [TargetRpc]
        internal protected virtual void OnShieldChange(NetworkConnection connection,
            int value)
        {
            Actor.Shield = value;
        }

        [ObserversRpc]
        [TargetRpc]
        internal protected virtual void OnMaxShieldChange(NetworkConnection connection,
            int value)
        {
            Actor.MaxShield = value;
        }

        [ObserversRpc]
        internal protected virtual void OnShieldCooldown()
        {

        }

        /*Not sure if these two are needed*/
        internal protected virtual void OnShieldRefreshRateChange(NetworkConnection connection, 
            int value)
        {
            Actor.ShieldRefreshRate = value;
        }

        internal protected virtual void OnShieldRefreshDelayChange(NetworkConnection connection,
            float value)
        {
            Actor.ShieldRefreshDelay = value;
        }

        [ObserversRpc]
        [TargetRpc]
        internal protected virtual void OnIsDeadChange(NetworkConnection connection,
            bool value)
        {
            Actor.IsDead = value;
        }
        #endregion

        #region Upstream
        [ServerRpc (RequireOwnership = false)]
        internal void Move(Vector2 direction)
        {
            Actor.Move(direction);
        }

        #region Fire Weapon
        [ServerRpc (RequireOwnership = false)]
        internal void FirePrimary()
        {
            //actor.PrimaryWeapon.Fire();
            Debug.Log("PWF");
            Control.PrimaryWeapon.Fire();
        }

        [ServerRpc (RequireOwnership = false)]
        internal void FireSecondary()
        {
            Control.SecondaryWeapon.Fire();
        }
        #endregion
        #endregion
    }

    //public class NetAvatarOwnerHandler : NetActorHandler
    //{
    //    protected internal override void OnHealthChange(NetworkConnection connection, int value)
    //    {
    //        Actor.Health = value;
    //        //update UI
    //    }
    //}

    public interface IAvatarUIHandler
    {
        void SetHealth(int value);
        void SetMaxHealth(int value);
        void SetShield(int value);
        void SetMaxShield(int value);
        void SetSheildCooldown(float seconds);
        void SetPrimaryCooldown(float seconds);
        void SetSecondaryCooldown(float seconds);
    }

    //public class MainPlayerUIHandler
}
