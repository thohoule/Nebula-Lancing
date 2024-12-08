using Interlace;
using Assets.Code.Characters.Weapons;
using Assets.Code.Characters.Weapons.Primary;
using Assets.Code.Characters;
using Assets.Coordinators.AI;

namespace Assets.Game.Character
{
    public class PlasmaGunWeapon : Weapon
    {
        private TestShooterWeapon plasmaGun;

        public PlasmaGunWeapon(AvatarCoord coord)
        {
            plasmaGun = new TestShooterWeapon(coord);
        }

        public PlasmaGunWeapon(AgentCoord agentCoord)
        {
            plasmaGun = new TestShooterWeapon(agentCoord);
        }

        public PlasmaGunWeapon(PlayerCoord playerCoord)
        {
            plasmaGun = new TestShooterWeapon(playerCoord);
        }

        public override bool CanFire(int charge)
        {
            return plasmaGun.CanFire(charge);
        }

        public override void Fire(int charge)
        {
            plasmaGun.Fire();
        }
    }
}
