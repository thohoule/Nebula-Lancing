using UnityEngine;
using Assets.Game;
using TeaSteep.Character.Status;
using TeaSteep.Character.Status.Effect;
using Assets.Entities;

namespace Interlace.OBS
{
    public abstract class ActorCoord : MonoBehaviour, IGameEntity
    {
        protected StatusManager status;
        protected EffectManager effects;
        //private ImplementedResolver resolver;
        //private float shieldRefreshTime;
        //private float shieldTickTime;
        //private bool shieldIsBroken;
        protected internal ActorHandler handler { get; internal set; }
        protected ActorEntity entitiy { get => handler.Entity; }

        public int Health { get => entitiy.Health; set => handler.OnHealthChange(value); }

        public int Shield { get => entitiy.Shield; set => handler.OnShieldChange(value); }

        public bool IsDead { get => entitiy.IsDead; set => handler.OnDeathChange(value); }

        public StatusManager Status => status;

        public EffectManager Effects => effects;

        public Vector3 Position => transform.position;

        public Vector3 Forward => transform.position + transform.forward;

        public abstract void InflictDamage(int damage);
    }
}
