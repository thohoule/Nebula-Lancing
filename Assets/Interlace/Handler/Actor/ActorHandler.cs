using System;
using UnityEngine;
using FishNet.Object;
using FishNet.Connection;
using TeaSteep.Character;
using Assets.Entities;
using Assets.Code;
using Assets.Code.Characters;
using TeaSteep;
using Assets.Game;

namespace Interlace
{
    [RequireComponent(typeof(ActorEntity))]
    public class ActorHandler : NetworkBehaviour
    {
        internal protected ActorEntity Entity { get; set; }

        public ShipAsset Puppet { get; internal set; }
        public CameraTarget CameraTarget { get; internal set; }
        public ParticleSystem DamagedParticles { get; set; }
        public MultiParticles DeathEffect { get; set; }

        public StatusUIHandler StatusUI { get; set; }

        public override void OnStartClient()
        {
            base.OnStartClient();


        }

        protected virtual void Awake()
        {
            Entity = GetComponent<ActorEntity>();
        }

        #region Downstream
        [ObserversRpc]
        internal void OnHealthChange(int value)
        {
            var oldValue = Entity.Health;
            Entity.Health = value;
            onHealthChange(oldValue, value);
        }

        [ObserversRpc]
        internal void OnMaxHealthChange(int value)
        {
            var oldValue = Entity.MaxHealth;
            Entity.MaxHealth = value;
            onMaxHealthChange(oldValue, value);
        }

        [ObserversRpc]
        internal void OnShieldChange(int value)
        {
            var oldValue = Entity.Shield;
            Entity.Shield = value;
            onShieldChange(oldValue, value);
        }

        [ObserversRpc]
        internal void OnMaxShieldChange(int value)
        {
            var oldValue = Entity.MaxShield;
            Entity.MaxShield = value;
            OnMaxShieldChange(oldValue, value);
        }

        [ObserversRpc]
        internal void OnDeathChange(bool value)
        {
            Entity.IsDead = value;
        }

        [ObserversRpc]
        internal void OnShieldCooldown()
        {
            //play animation
        }
        #endregion

        public void RefreshStatus()
        {
            StatusUI.SetMaxHealth(Entity.MaxHealth);
            StatusUI.SetHealth(Entity.Health);
            StatusUI.SetMaxShield(Entity.MaxShield);
            StatusUI.SetShield(Entity.Shield);
        }

        #region Overrides for UI
        protected virtual void onHealthChange(int oldValue, int currentValue)
        {
            StatusUI.SetHealth(currentValue);
        }

        protected virtual void onMaxHealthChange(int oldValue, int currentValue)
        {
            StatusUI.SetMaxHealth(currentValue);
        }

        protected virtual void onShieldChange(int oldValue, int currentValue)
        {
            StatusUI.SetShield(currentValue);
        }

        protected virtual void OnMaxShieldChange(int oldValue, int currentValue)
        {
            StatusUI.SetMaxShield(currentValue);
        }

        protected virtual void onIsDeadChange(bool value)
        {
            StatusUI.SetHealth(0);
            StatusUI.SetShield(0);
        }
        #endregion
    }
}
