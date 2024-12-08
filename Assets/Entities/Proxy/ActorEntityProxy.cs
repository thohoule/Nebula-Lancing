using Interlace;
using UnityEngine;

namespace Assets.Entities
{
    public abstract class ActorEntityProxy : MonoBehaviour
    {
        protected ActorHandler handler { get; set; }
        protected ActorEntity entity { get => handler.Entity; }

        public int Health { get => entity.Health; set => handler.OnHealthChange(value); }
        public int MaxHealth { get => entity.MaxHealth; set => handler.OnMaxHealthChange(value); }
        public int Shield { get => entity.Shield; set => handler.OnShieldChange(value); }
        public int MaxShield { get => entity.MaxShield; set => handler.OnShieldChange(value); }
        public int ShieldRefreshRate { get => entity.ShieldRefreshRate; set => handler.Entity.ShieldRefreshRate = value; }
        public float ShieldRefreshDelay { get => entity.ShieldRefreshDelay; }
        public bool IsDead { get => entity.IsDead; set => handler.OnDeathChange(value); }
    }
}
