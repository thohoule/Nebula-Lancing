using System;
using System.Collections.Generic;
using UnityEngine;
using TeaSteep;
using TeaSteep.Ability;
using TeaSteep.Character.Status.Effect;
using Assets.Code.Gameplay.Testing;
using Assets.Code.Characters.Weapons.Common;
using Interlace;

namespace Assets.Code.Characters.Weapons.Secondary
{
    public class TestGrabberWeapon : BaseWeapon
    {
        private const int fire_Delay = 1500;

        private DateTime lastFired;
        private bool isGrabbing;
        private Game.IGameEntity grabTarget;
        private GameObject mockGrabber;

        public TestGrabberWeapon(NetActorControl caster) :
            base(caster)
        {
            lastFired = default;
        }

        public TestGrabberWeapon(AvatarCoord caster) : 
            base(caster)
        {
            lastFired = default;
        }

        public override bool FireCondition(ActionInstance<AbilityContext> instance)
        {
            if (isGrabbing)
                return true;
            
            return DateTime.Now.Subtract(lastFired).TotalMilliseconds >= fire_Delay;
        }

        protected override void onFire(ActionInstance<AbilityContext> instance)
        {
            if (isGrabbing)
            {
                //grabber effect
                instance.Context.Target = grabTarget;
                instance.AddEffectToTarget(grabTarget, new ImpactDamage(instance, 20));

                mockGrabber.transform.parent = null;
                mockGrabber.SetActive(false);

                instance.Dispose();
            }
            else
            {
                //var virtualProjectile = new ImpactProjectile(instance, 5);
                var virtualProjectile = new GrabberProjectile(instance, grabHandler);

                var projectile = PoolSpawner.Create<GrabberProjectileAsset>();
                instance.AttachObject(projectile);
                projectile.EnableModel();

                //set projectile settings
                ProjectileSettings settings = new ProjectileSettings();
                settings.Speed = 40;
                settings.Direction = ability.Caster.transform.forward;
                settings.SphereExtent = 1;
                settings.CastType = CastType.Ray;
                settings.LayerMask = -1;

                projectile.transform.rotation = Quaternion.LookRotation(
                    settings.Direction);

                projectile.Settings = settings;
                projectile.Fire(virtualProjectile, ability.Caster.Forward);

                ChargeValue = 0;

                lastFired = DateTime.Now;
            }
        }

        private void grabHandler(Game.IGameEntity target, RaycastHit hit)
        {
            isGrabbing = true;
            grabTarget = target;

            if (mockGrabber == null)
            {
                //add grabber model
                var mockPrefab = PrefabAsset.GetPrefab<MockGrabberAsset>();
                mockGrabber = GameObject.Instantiate(mockPrefab).gameObject;
            }
            else
            {
                //activate mock grabber
                mockGrabber.SetActive(true);
            }

            mockGrabber.SetParentAndLocals(target.gameObject);
            mockGrabber.transform.position = hit.transform.position;

            //rotate using hit direction
        }
    }
}
