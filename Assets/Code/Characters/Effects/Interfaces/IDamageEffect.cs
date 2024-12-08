using TeaSteep.Character.Status.Effect;

namespace Assets.Code.Characters
{
    public interface IDamageEffect : IImplementedEffect
    {
        void OnInvoke(EffectInstance<AbilityContext> instance);
    }
}
