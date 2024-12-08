using System;
using UnityEngine;
using TeaSteep.Ability;

namespace Assets.Code.Characters.Weapons.Secondary
{
    public delegate void HitHandler(Game.IGameEntity target, RaycastHit hit);

    public class GrabberProjectile : IVirtualProjectile
    {
        private HitHandler hitHandler;

        public ActionInstance<AbilityContext> Instance { get; private set; }

        public GrabberProjectile(ActionInstance<AbilityContext> instance,
            HitHandler hitHandler)
        {
            Instance = instance;
            this.hitHandler = hitHandler;
        }

        public void OnFire()
        {
        }

        public void OnHit(IProjectileObject projectile, RaycastHit hit, out bool stepToHit)
        {
            stepToHit = true;

            var target = hit.collider.GetComponent<Game.IGameEntity>();

            Debug.Log(string.Format("Hit {0}!", hit.collider.gameObject.name));

            if (target != null) //check if enemy
            {
                hitHandler?.Invoke(target, hit);
                projectile.Dispose();
            }
            else
                fizzle();
        }

        public void OnTraversal()
        {
            //fizzle explosion animation
        }

        private void fizzle()
        {
            Instance.Dispose();
        }
    }
}
