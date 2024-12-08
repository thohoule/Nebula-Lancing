using UnityEngine;
using Assets.Code.Characters;
using TeaSteep;
using FishNet.Connection;
using FishNet.Object;
using Assets.Code.Gameplay.Network;
using Assets.Code.Characters.Weapons.Primary;
using Assets.Code.Characters.Weapons.Secondary;
//using Assets.Code.Characters.AI;
//using Assets.Code.Characters.AI.Pathing;

namespace Assets.Code.Gameplay
{
    public class EnemySpawning : NetworkBehaviour
    {
        [SerializeField]
        private NetActor enemyPrefab;
        //[SerializeField]
        //private PatrolCluster patrol;

        public void UseSpawn()
        {
            var enemy = Instantiate(enemyPrefab);
            //var agent = enemy.GetComponent<EnemyAgent>();
            //var patrolGoal = enemy.GetComponent<IPatrolGoal>();

            //set any other info (AI)
            enemy.transform.position = transform.position;

            //patrolGoal.SetPatrol(patrol);

            Spawn(enemy.gameObject, LocalConnection);

            //var handler = agent.GetComponent<NetActorHandler>();
            //var control = NetActorControl.CreateControl(handler);
            //control.PrimaryWeapon = new TestShooterWeapon(control);

            //handler.Control = control;
        }

        public static void SpawnAll()
        {
            foreach (var spawn in FindObjectsOfType<EnemySpawning>())
                spawn.UseSpawn();
        }
    }
}
