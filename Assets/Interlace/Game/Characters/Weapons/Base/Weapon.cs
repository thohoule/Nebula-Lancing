

namespace Assets.Game.Character
{
    public abstract class Weapon
    {
        public abstract bool CanFire(int charge);
        public abstract void Fire(int charge);
    }
}
