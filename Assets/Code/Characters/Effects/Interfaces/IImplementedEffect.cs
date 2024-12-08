using TeaSteep.Character.Status.Effect;

namespace Assets.Code.Characters
{
    public interface IImplementedEffect : IActionEffect<AbilityContext>
    {
    }

    public interface IImplementedEffect<T> : IImplementedEffect
    {
        T Subcontext { get; }
    }
}
