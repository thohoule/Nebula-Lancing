using Assets.Services;
using Interlace;
using Interlace.Authorities;
using UnityEngine;
using TeaSteep;

namespace Assets.Game
{
    public class EncounterState : IStartContent, IUpdateContent<GameplayControl>
    {
        public void OnStateStart(TransitionOperation operation)
        {
            Debug.Log("Encounter State Entered.");
            SyncControl.SetState(3);
            SyncAuthority.SetSync(3);

            LobbyAuthority.SpawnPuppets();
            LobbyAuthority.EnableHandlers();

            foreach (var player in LobbyService2.GetActivePlayers())
            {
                CameraService.SetCamera(player.ProfileHandler.Entity.Connection,
                    player.ActorHandler);
            }

            //Activate Player and Agent Controls
            //AvatarService.EnableAll(); //done in sync version
            //AgentService.EnableAll();
            AgentAuthority.EnableAgents();
        }

        //public void OnStateStart()
        //{
        //    Debug.Log("Encounter State Entered.");
        //    SyncControl.SetState(3);
        //    SyncAuthority.SetSync(3);

        //    LobbyAuthority.SpawnPuppets();
        //    LobbyAuthority.EnableHandlers();

        //    foreach (var player in LobbyService2.GetActivePlayers())
        //    {
        //        CameraService.SetCamera(player.ProfileHandler.Entity.Connection,
        //            player.ActorHandler);
        //    }

        //    //Activate Player and Agent Controls
        //    //AvatarService.EnableAll(); //done in sync version
        //    //AgentService.EnableAll();
        //    AgentAuthority.EnableAgents();
        //}

        public void OnStateUpdate(BranchOperation operation, GameplayControl branch)
        {

        }

        //public void OnStateUpdate(ControlMachine<GameplayControl> controlMachine)
        //{
        //    /*Won't work, Avatar Handler is universal, because their is no
        //    owned actor handler there is nothing to assign to Player handler*/

        //    //var partyWipe = true;

        //    //foreach (var player in LobbyService.GetActivePlayers())
        //    //{
        //    //    if (!player.AvatarHandler.Entity.IsDead)
        //    //    {
        //    //        partyWipe = false;
        //    //        break;
        //    //    }
        //    //}

        //    //if (partyWipe)
        //    //{
        //    //    //Lose State
        //    //}
        //}
    }
}
