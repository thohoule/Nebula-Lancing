using System;
using System.Collections;
using UnityEngine;
using TeaSteep;
using TeaSteep.Ability;
using FishNet.Object;

namespace Assets.Code.Characters.Weapons.Common
{
    public class GrabberProjectileAsset : NetPrefabAsset, IProjectileObject
    {
        protected IVirtualProjectile triggers;

        [SerializeField]
        private GameObject modelObject;
        [SerializeField]
        private ParticleSystem hitParticles;
        [SerializeField]
        private ProjectileSettings settings;

        public override bool TypeSearchEnabled => true;

        public bool IsActive { get; private set; }
        public bool IsDisposed { get; private set; }
        public ProjectileSettings Settings { get => settings; set => settings = value; }
        public Vector3 Position { get => transform.position; }
        public DateTime StartTime { get; private set; }
        public float TraversalTime { get; private set; }
        public float TraversalDistance { get; private set; }
        public TelemetryTransform Telemetry { get => new TelemetryTransform(transform, settings.Speed); }

        public void Fire(IVirtualProjectile virtualProjectile, Vector3 position)
        {
            triggers = virtualProjectile;

            transform.position = position;
            StartTime = DateTime.Now;
            TraversalTime = 0;
            TraversalDistance = 0;
            IsActive = true;
            IsDisposed = false;
            triggers.OnFire();
        }

        public void EnableModel()
        {
            modelObject.SetActive(true);
        }

        private void Update()
        {
            onUpdate();

            TraversalTime += Time.deltaTime;
            if (IsActive)
                triggers.OnTraversal();
        }

        protected virtual void onUpdate()
        {
            var step = Settings.Speed * Time.deltaTime;

            if (Settings.CastType == CastType.Ray &&
                RayCast(step, out RaycastHit hit))
            {
                triggers.OnHit(this, hit, out bool stepToHit);

                if (stepToHit)
                    step = hit.distance;
            }
            else if (Settings.CastType == CastType.BufferedRay &&
                RayCast(step, out hit))
            {
                triggers.OnHit(this, hit, out bool stepToHit);

                if (stepToHit) //temp, doesn't add sphere radius to hit distance
                    step = hit.distance;
            }
            else if (Settings.CastType == CastType.Sphere &&
                SphereCast(step, out hit))
            {
                triggers.OnHit(this, hit, out bool stepToHit);

                if (stepToHit)
                    step = hit.distance;
            }
            else if (Settings.CastType == CastType.Box &&
                BoxCast(step, out hit))
            {
                triggers.OnHit(this, hit, out bool stepToHit);

                if (stepToHit)
                    step = hit.distance;
            }

            transform.position += Settings.Direction * step;
            TraversalDistance += step;
        }

        public bool RayCast(float distance, out RaycastHit hit)
        {
            return Physics.Raycast(Position, Settings.Direction, out hit, distance, Settings.LayerMask);
        }

        public bool SphereCast(float distance, out RaycastHit hit)
        {
            return Physics.SphereCast(Position, Settings.SphereExtent, Settings.Direction,
                out hit, distance, Settings.LayerMask);
        }

        public bool BoxCast(float distance, out RaycastHit hit)
        {
            return Physics.BoxCast(Position, Settings.BoxExtents, Settings.Direction, out hit,
                Quaternion.identity, distance, Settings.LayerMask);
        }

        public void Dispose()
        {
            if (IsDisposed)
                return;

            IsDisposed = true;
            playHitAnimation();
            StartCoroutine(delayedDispose());

            //Despawn(NetworkObject);
        }

        private IEnumerator delayedDispose()
        {
            /*Add particle to plasma prefab, then on dispose hide  the projectile
             model and play animation, then dispose.*/

            yield return new WaitForSecondsRealtime(2);
            Despawn(NetworkObject);
        }

        [ObserversRpc]
        private void playHitAnimation()
        {
            modelObject.SetActive(false);
            hitParticles.Play();
        }
    }
}
