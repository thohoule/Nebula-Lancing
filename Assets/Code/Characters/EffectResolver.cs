using System;
using System.Collections.Generic;
using TeaSteep;
using TeaSteep.Character.Status;
using TeaSteep.Character.Status.Effect;

namespace Assets.Code.Characters
{
    public class ImplementedResolver : TeaSteep.Character.Status.Effect.EffectResolver<AbilityContext>
    {
        public ImplementedResolver(IGameEntity actor) :
            base(actor)
        { }

        public override EffectInstance<T> AddEffect<T>(IActionEffect<T> effect)
        {
            if (effect is IActionEffect<AbilityContext>)
                return AddEffect(effect as IActionEffect<AbilityContext>) as EffectInstance<T>;

            //unhandled types, but this can be used for non-caster branches
            return null;
        }

        public EffectInstance<AbilityContext> AddEffect(IActionEffect<AbilityContext> effect)
        {
            ImplementedEffect effectInstance = new ImplementedEffect(this, effect);

            if (effectInstance.IsContinuous)
            {

            }
            else
            {
                //resolve now per test Implementation
                effectInstance.Invoke();
            }

            return effectInstance;
        }

        private class ImplementedEffect : EffectInstance<AbilityContext>
        {
            private IActionEffect<AbilityContext> effect;
            private IContinuousEffect continuousEffect;
            private IDamageEffect damageEffect;

            public bool IsContinuous { get { return continuousEffect != null; } }

            public ImplementedEffect(EffectResolver resolver, 
                IActionEffect<AbilityContext> effect) :
                base(resolver.Caster, effect)
            {
                this.effect = effect as IActionEffect<AbilityContext> ??
                    throw new NullReferenceException("Effect is null.");
                continuousEffect = effect as IContinuousEffect;
                damageEffect = effect as IDamageEffect;
            }

            public void Invoke()
            {
                if (damageEffect != null)
                {
                    damageEffect.OnInvoke(this);
                }
            }
        }
    }
}
