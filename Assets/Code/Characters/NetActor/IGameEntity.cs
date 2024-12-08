using TeaSteep.Character;

namespace Assets.Code.Characters
{
    public interface IGameEntity : IActor
    {
        int Health { get; }
        int Shield { get; }
        bool IsDead { get; }

        void InflictDamage(int damage);
    }
}
