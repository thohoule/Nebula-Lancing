using Assets.Code.Gameplay;
using FishNet.Object;
using FishNet.Connection;
using Assets.Code.Gameplay.Network;

namespace Assets.Code.Interlace.Gameplay
{
    public class GameplaySyncControl
    {
        public static void SyncNewPlayer(RegisteredClient clientNew)
        {
            /*loop all current players server side, besides joining and
             send assign player RCPs for each active player*/

            foreach (var targetHandler in PlayerService.GetActivePlayers())
            {
                if (targetHandler.Player != clientNew.Player)
                {
                    targetHandler.OnAssginPlayer(clientNew.Connection, 
                        targetHandler.Player);
                    targetHandler.OnSyncPrep(clientNew.Connection, 
                        targetHandler.Player.Prep);
                }
            }
        }

        public static void StartTransition()
        {
            GameplayService.Handler.OnTransition();
        }
    }
}
