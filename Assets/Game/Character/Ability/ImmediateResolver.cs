using Assets.Code.Characters;
using TeaSteep.Character.Status.Effect;

namespace Assets.Game.Character
{
    public class ImmediateResolver : EffectResolver<AbilityContext>
    {
        public ImmediateResolver(Game.IGameEntity actor) :
            base(actor)
        { }

        public override EffectInstance<T> AddEffect<T>(IActionEffect<T> effect)
        {
            if (effect is IActionEffect<AbilityContext>)
                return AddEffect(effect as IActionEffect<AbilityContext>)
                    as EffectInstance<T>; //rework this mess

            return null;
        }

        public EffectInstance<AbilityContext> AddEffect(IActionEffect<AbilityContext> effect)
        {
            ImmediateEffect immediateEffect = new ImmediateEffect(this, effect);
            immediateEffect.Invoke();

            return immediateEffect;
        }

        private class ImmediateEffect : EffectInstance<AbilityContext>
        {
            private IActionEffect<AbilityContext> effect;
            private IContinuousEffect continuousEffect;
            private IDamageEffect damageEffect;

            public bool IsContinuous { get { return continuousEffect != null; } }

            public ImmediateEffect(EffectResolver resolver,
                IActionEffect<AbilityContext> effect) :
                base(resolver.Caster, effect)
            {
                this.effect = effect;
                continuousEffect = effect as IContinuousEffect;
                damageEffect = effect as IDamageEffect;
            }

            public void Invoke()
            {
                damageEffect?.OnInvoke(this);
            }
        }
    }
}
