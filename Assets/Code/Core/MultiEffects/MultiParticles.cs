using UnityEngine;
using System.Collections.Generic;

namespace Assets.Code
{
    public class MultiParticles : MonoBehaviour
    {
        [SerializeField]
        private List<ParticleSystem> systems;

        public List<ParticleSystem> Systems { get => systems; }
        public bool IsPlaying { get; private set; }

        public void PlayAll()
        {
            IsPlaying = true;
            foreach (var system in Systems)
            {
                system?.Play();
            }
        }

        public void StopAll()
        {
            foreach (var system in Systems)
            {
                system?.Stop();
            }
            IsPlaying = false;
        }
    }
}
