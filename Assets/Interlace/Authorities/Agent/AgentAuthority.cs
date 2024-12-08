using Assets.Code.Gameplay;
using Assets.Code.Interlace.Actor.AI.Temp;
using Assets.Coordinators.AI;
using Assets.Game.Character;
using Assets.Interlace.Core.CoordHelper;
using FishNet.Connection;
using System;
using System.Collections.Generic;
using TeaAI;
using TeaAI.Example;
using TeaAI.Example.Options;
using UnityEngine;

namespace Interlace.Authorities
{
    public class AgentAuthority : MonoBehaviour
    {
        //[SerializeField]
        //private AgentCoord agentPrefab;

        private List<AgentCoord> activeAgents;

        private static AgentAuthority instance;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
                return;
            }

            instance = this;
            activeAgents = new List<AgentCoord>();
        }

        private void spawnAll()
        {
            foreach (var spawn in FindObjectsOfType<AgentSpawning>())
            {
                var coord = CoordHelper.UnpackCoord<ActorHandler, AgentCoord>(
                    spawn.PuppetHandler, null, null);

                coord.GetComponent<PathingEventModule>().RaisePatrolNetworkChange(
                    spawn.Patrol);

                coord.Primary = new PlasmaGunWeapon(coord);
                coord.GetComponent<RangedAttackOption>().AssignListener(coord.FirePrimary);

                activeAgents.Add(coord);
            }
        }

        private void oldSpawnCode(AgentSpawning spawn)
        {
            var handler = spawn.UseSpawn();
            var agent = Instantiate(spawn.CoordPrefab);
            agent.Initialize();
            agent.gameObject.SetActive(false);


            agent.transform.position = handler.transform.position;
            handler.transform.SetParent(agent.transform, true);

            agent.GetComponent<PathingEventModule>().RaisePatrolNetworkChange(spawn.Patrol);

            agent.SetHandler(handler);

            agent.Primary = new PlasmaGunWeapon(agent);

            activeAgents.Add(agent);
        }

        public static void SpawnAll()
        {
            instance.spawnAll();
        }

        public static void EnableAgents()
        {
            foreach (var agent in instance.activeAgents)
                agent.gameObject.SetActive(true);
        }

        public static void DisableAgents()
        {

        }
    }
}
