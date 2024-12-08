using System;
using System.Collections.Generic;
using UnityEngine;
using TeaSteep.Ability;
using TeaSteep.Character.Status.Effect;
using Assets.Code.Gameplay.Testing;
using Assets.Code.Characters.Weapons.Common;
using Interlace;
using Assets.Coordinators.AI;

namespace Assets.Code.Characters.Weapons.Primary
{
    public class TestShooterWeapon : BaseWeapon, IPrimaryWeapon
    {
        private DateTime lastFired;
        private float fireTime;

        public TestShooterWeapon(NetActorControl caster) :
            base(caster)
        {
            //lastFired = default;
            fireTime = 0;
        }

        public TestShooterWeapon(AvatarCoord caster) :
            base(caster)
        {
            fireTime = 0;
        }

        public TestShooterWeapon(AgentCoord caster) :
            base(caster)
        {
            fireTime = 0;
        }

        public TestShooterWeapon(PlayerCoord caster) :
            base(caster)
        {
            fireTime = 0;
        }

        public override bool FireCondition(ActionInstance<AbilityContext> instance)
        {
            //Don't ask
            fireTime = (float)DateTime.Now.Subtract(lastFired).TotalMilliseconds / 1000;

            if (fireTime >= 0.5f)
                return true;
            
            //fireTime += Time.deltaTime;
            return false;

            //return DateTime.Now.Subtract(lastFired).TotalMilliseconds >= 500;
            //return lastFired.Subtract(DateTime.Now).TotalMilliseconds >= 2000;
        }

        protected override void onFire(ActionInstance<AbilityContext> instance)
        {
            var virtualProjectile = new ImpactProjectile(instance, 20);

            //temp, get projectile from pool (a net pool)
            //SimpleProjectile projectile = new SimpleProjectile();

            var projectile = PoolSpawner.Create<BallProjectileAsset>();
            instance.AttachObject(projectile);
            projectile.EnableModel();

            //set projectile settings
            ProjectileSettings settings = new ProjectileSettings();
            settings.Speed = 50;
            settings.Direction = ability.Caster.transform.forward;
            settings.SphereExtent = 1;
            settings.CastType = CastType.Ray;
            settings.LayerMask = -1;

            projectile.Settings = settings;
            projectile.Fire(virtualProjectile, ability.Caster.Forward);

            ChargeValue = 0;
            lastFired = DateTime.Now;
            fireTime = 0;
        }
    }
}
