using UnityEngine;

namespace Assets.Code.Characters
{
    public class ShipAsset : PrefabAsset
    {
        public const string Small_Ship = "SmallSpaceship";
        public const string Medium_Ship = "MediumSpaceship";
        public const string Large_Ship = "LargeSpaceship";

        [SerializeField]
        private ParticleSystem damagedParticles;
        [SerializeField]
        private MultiParticles deathEffect;
        [SerializeField]
        private GameObject primaryAttachPoint;
        [SerializeField]
        private GameObject secondaryAttachPoint;

        public ParticleSystem DamagedParticles { get => damagedParticles; }
        public MultiParticles DeathEffect { get => deathEffect; }
        public GameObject PrimaryAttachPoint { get => primaryAttachPoint; }
        public GameObject SecondaryAttachPoint { get => secondaryAttachPoint; }
    }
}
