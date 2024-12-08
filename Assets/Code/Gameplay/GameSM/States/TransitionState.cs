using UnityEngine;
using TeaSteep;
using Assets.Code.Gameplay.Network;
using Assets.Code.Interlace.Gameplay;
using Assets.Code.Interlace.V3.Player;

namespace Assets.Code.Gameplay.GameSM.States
{
    public class TransitionState : MonoBehaviour, ITeaContent
    {
        [SerializeField]
        private AvatarSpawn player1Spawn;

        public void OnStateStart(GameLoopControl control)
        {
            //Spawn players
            PlayerClientControl.SpawnAvatars();

            Debug.Log("Trans state reached");
            GameplaySyncControl.StartTransition();
            //Spawn Enemies
            EnemySpawning.SpawnAll();

            //disable and release mock ships

            //Spawn real avatars, let player control handle
            //foreach (var client in Lobby.Registered)
            //{
            //    var player = client.Player;

            //    switch (player.AssingedLane.Number)
            //    {
            //        case 1:
            //            LobbyInterlaceControl.SetSeat1(player);
            //            player1Spawn.UseSpawn(client);
            //            break;
            //    }
            //}

            //send transaction to all to begin Player Control
            //PlayingInterlaceControl.StartControlState();
            //EnemySpawning.SpawnAll();
            //GameplayTransactions.EnablePlayControls();

            //change state to Encounter

        }

        public void OnStateEnd()
        {
        }

        public void OnStateUpdate(GameLoopControl control)
        {

        }
    }
}
