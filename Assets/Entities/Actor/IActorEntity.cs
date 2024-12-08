
namespace Assets.Entities
{
    public interface IActorEntity
    {
        int Health { get; }
        int MaxHealth { get; }
        int Shield { get; }
        int MaxShield { get; }
        int ShieldRefreshRate { get; }
        float ShieldRefreshDelay { get; }
        bool IsDead { get; }
    }
}
