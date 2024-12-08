using Assets.Game;
using UnityEngine;
using TeaSteep;
using Assets.Services;
using Interlace;

namespace Assets.Services.Sync
{
    public class SyncLobbyState : IStartContent, IEndContent
    {
        public void OnStateStart(TransitionOperation operation)
        {
            //foreach (var player in LobbyService2.GetActivePlayers())
            //    StartPrep(player);
        }

        public void OnStateEnd(TransitionOperation operation)
        {
            foreach (var player in LobbyService2.GetActivePlayers())
                player.PrepHandler.ClientLocal.EndPrepState();
        }

        public static bool SyncCheck()
        {
            return SyncHandlerService.SyncValue == 1;
        }

        public void StartPrep(PlayerHandler2 player)
        {
            player.PrepHandler.ClientLocal.StartPrepState();
        }
    }
}
