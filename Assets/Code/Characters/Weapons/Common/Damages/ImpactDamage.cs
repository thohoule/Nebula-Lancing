using System;
using UnityEngine;
using TeaSteep.Ability;
using TeaSteep.Character.Status.Effect;

namespace Assets.Code.Characters
{
    public class ImpactDamage : IActionEffect<AbilityContext>, IDamageEffect
    {
        public ActionInstance<AbilityContext> ActionInstance { get; private set; }
        public float Damage { get; private set; }

        //public InvokeEffectHandler<AbilityContext> OnInvoke => apply;

        public ImpactDamage(ActionInstance<AbilityContext> instance, float damage)
        {
            ActionInstance = instance;
            Damage = Mathf.Abs(damage);
        }

        private void apply(EffectInstance<AbilityContext> effectInstance)
        {
            //effectInstance.Target.Status.SubtractFromStatValue(AvatarStats.Health, Damage);

            Debug.Log("Effect was applied, action is disposed.");
            effectInstance.ActionInstance.Dispose();
        }

        public void OnInvoke(EffectInstance<AbilityContext> instance)
        {
            //instance.Target.Status.SubtractFromStatValue(AvatarStats.Health,
            //    Damage);
            var target = instance.ActionInstance.Context.Target;
            target.InflictDamage((int)Damage);
            //target.Health = Mathf.Clamp(target.Health - Damage, 0, float.MaxValue);

            Debug.Log(string.Format("Effect was applied (Dmg: {0}), action is disposed.",
                Damage));
            instance.ActionInstance.Dispose();
        }
    }
}
