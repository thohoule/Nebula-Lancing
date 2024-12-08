using Assets.Game;
using UnityEngine;
using TeaSteep;
using Assets.Services;

namespace Interlace.Sync
{
    public class SyncLobbyState : IStartContent, IEndContent
    {
        public void OnStateStart(TransitionOperation operation)
        {
            Debug.Log("Entered Lobby Sync State");
        }

        //public void OnStateStart()
        //{
        //    //foreach (var handler in LobbyService.GetActivePlayers())
        //    //    handler.OnLobbyStart();
        //    Debug.Log("Entered Lobby Sync State");
        //}

        public void OnStateEnd(TransitionOperation operation)
        {
            foreach (var handler in LobbyService.GetActivePlayers())
                handler.OnLobbyEnd();
        }

        //public void OnStateEnd()
        //{
        //    foreach (var handler in LobbyService.GetActivePlayers())
        //        handler.OnLobbyEnd();
        //}

        public static bool SyncCheck()
        {
            return SyncHandlerService.SyncValue == 1;
        }
    }
}
