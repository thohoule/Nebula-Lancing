using UnityEngine;
using FishNet.Object;
using Assets.Code;

namespace Assets.Game.Character
{
    public class AvatarPuppet : NetworkBehaviour
    {
        [SerializeField]
        private ParticleSystem damagedParticles;
        [SerializeField]
        private MultiParticles deathEffect;

        public ParticleSystem DamagedParticles { get => damagedParticles; set => damagedParticles = value; }
        public MultiParticles DeathEffect { get => deathEffect; set => deathEffect = value; }
    }
}
