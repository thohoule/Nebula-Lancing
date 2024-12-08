using UnityEngine;
using FishNet.Object;
using Assets.Entities;
using TeaAI.Example;
using Interlace;
using Assets.Coordinators.AI;

namespace Assets.Game.Character
{
    public class AgentSpawning : NetworkBehaviour
    {
        [SerializeField]
        private ActorHandler puppetPrefab;
        [SerializeField]
        private PatrolNetwork patrol;
        [SerializeField]
        private AgentCoord coordPrefab;

        public ActorHandler PuppetHandler { get => puppetPrefab; }
        public PatrolNetwork Patrol { get => patrol; }
        public AgentCoord CoordPrefab { get => coordPrefab; }

        public ActorHandler UseSpawn()
        {
            var puppetInstance = Instantiate(puppetPrefab);
            puppetInstance.transform.position = transform.position;

            Spawn(puppetInstance.gameObject);
            return puppetInstance;
        }
    }
}
