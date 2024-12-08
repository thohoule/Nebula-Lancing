using System;
using System.Collections.Generic;
using UnityEngine;
using TeaSteep.Ability;
using TeaSteep.Character.Status.Effect;
using Interlace;
using Assets.Coordinators.AI;

namespace Assets.Code.Characters.Weapons
{
    public abstract class BaseWeapon : IWeapon
    {
        protected AbilityInstance<AbilityContext> ability { get; private set; }

        public bool CanCharge { get; protected set; }
        public float ChargeRate { get; protected set; }
        public float ChargeValue { get; protected set; }

        protected BaseWeapon(NetActorControl caster)
        {
            ability = new AbilityInstance<AbilityContext>(caster,
                FireCondition, onFire);
        }

        protected BaseWeapon(AvatarCoord avatarCoord)
        {
            ability = new AbilityInstance<AbilityContext>(avatarCoord,
                FireCondition, onFire);
        }

        protected BaseWeapon(AgentCoord agentCoord)
        {
            ability = new AbilityInstance<AbilityContext>(agentCoord,
                FireCondition, onFire);
        }

        protected BaseWeapon(PlayerCoord playerCoord)
        {
            ability = new AbilityInstance<AbilityContext>(playerCoord,
                FireCondition, onFire);
        }

        protected BaseWeapon(AbilityInstance<AbilityContext> ability)
        {
            this.ability = ability;
        }

        public virtual bool FireCondition(ActionInstance<AbilityContext> instance)
        {
            return true;
        }

        public virtual void Charge()
        {
            if (CanCharge)
                ChargeValue = Mathf.Clamp01(ChargeValue +
                    (ChargeRate * Time.deltaTime));
        }

        public bool CanFire(int charge)
        {
            return CanFire(new AbilityContext(charge));
        }

        public bool CanFire(AbilityContext context)
        {
            return ability.ActionUseable(context);
        }

        public virtual void Fire()
        {
            AbilityContext context = new AbilityContext(ChargeValue);
            ability.Use(context);
        }

        protected abstract void onFire(ActionInstance<AbilityContext> instance);
    }

    public interface IPrimaryWeapon : IWeapon
    { }

    public interface IWeapon
    {
        bool CanCharge { get; }
        float ChargeRate { get; }
        float ChargeValue { get; }

        bool FireCondition(ActionInstance<AbilityContext> instance);
        void Charge();
        void Fire();
    }
}
