using System;
using UnityEngine;
using TeaSteep.Ability;
using Assets.Game;
using IGameEntity = Assets.Game.IGameEntity;

namespace Assets.Code.Characters.Weapons
{
    public class ImpactProjectile : IVirtualProjectile
    {
        private bool lockHit;

        public ActionInstance<AbilityContext> Instance { get; private set; }
        public float Damage { get; set; }

        public ImpactProjectile(ActionInstance<AbilityContext> instance, float damage)
        {
            Instance = instance;
            Damage = damage;
        }

        public void OnFire()
        {
        }

        public void OnHit(IProjectileObject projectile, RaycastHit hit, out bool stepToHit)
        {
            stepToHit = true;
            if (lockHit)
                return;

            lockHit = true;
            var target = hit.collider.GetComponentInParent<Game.IGameEntity>();

            Debug.Log(string.Format("Hit {0}!", hit.collider.gameObject.name));

            //Apply effect to actor
            if (target != null)
            {
                Instance.Context.Target = target;
                Instance.AddEffectToTarget(target, new ImpactDamage(Instance, Damage));
                projectile.Dispose();
            }
            else
            {
                Debug.Log("Hit non-actor, action disposed.");
                Instance.Dispose();
            }
        }

        public void OnTraversal()
        {
        }
    }
}
