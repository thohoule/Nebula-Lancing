using FishNet.Connection;
using UnityEngine;
using Assets.Entities;
using Assets.Services;

namespace Interlace
{
    public class LobbyControl : MonoBehaviour
    {
        private static LobbyControl instance;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
                return;
            }

            instance = this;
        }

        #region Assign
        public static TransactionResult CanBeSeated(NetworkConnection connection, 
            string profileName, out PlayerHandler handler)
        {
            if (LobbyService.TryGetMemberByConnection(connection,
                out PlayerHandler playerHandler))
            {
                //reconnection
            }

            if (LobbyService.TryGetEmptySeat(connection, out handler))
            {
                assignPlayer(handler, connection, profileName);
                return TransactionResult.Successful();
            }

            //if (!LobbyService.Player1.IsAssignedPlayer)
            //{
            //    handler = LobbyService.Player1;
            //    assignPlayer(handler, connection, profileName);
            //    return TransactionResult.Successful();
            //}
            //if (!LobbyService.Player2.IsAssignedPlayer)
            //{
            //    handler = LobbyService.Player2;
            //    assignPlayer(handler, connection, profileName);
            //    return TransactionResult.Successful();
            //}
            //if (!LobbyService.Player3.IsAssignedPlayer)
            //{
            //    handler = LobbyService.Player3;
            //    assignPlayer(handler, connection, profileName);
            //    return TransactionResult.Successful();
            //}
            //if (!LobbyService.Player4.IsAssignedPlayer)
            //{
            //    handler = LobbyService.Player4;
            //    assignPlayer(handler, connection, profileName);
            //    return TransactionResult.Successful();
            //}

            handler = null;
            return TransactionResult.Error("Join rejected, Lobby is full.");
        }

        private static void assignPlayer(PlayerHandler handler, 
            NetworkConnection connection, string profileName)
        {
            var prep = loadPrepEntity(null);

            PlayerEntity entity = new PlayerEntity()
            {
                Connection = connection,
                ProfileName = profileName,
                Prep = prep,
                SeatNumber = handler.SeatNumber
            };

            SyncControl.SyncPlayer(connection);
            syncAllHandlerToClient(connection);
            handler.IsAssignedPlayer = true;
            handler.OnAssignedConnection(null, entity);
            handler.OnFirstLoad(null);
            handler.OnEnableEdit(connection);
        }

        private static void syncAllHandlerToClient(NetworkConnection connection)
        {
            for (int seat = 1; seat <= 4; seat++)
            {
                var playerHandler = LobbyService.GetPlayerBySeat(seat);
                if (playerHandler.IsAssignedPlayer)
                {
                    playerHandler.OnAssignedConnection(connection,
                        playerHandler.Entity);

                    if (SyncControl.CurrentPhase == 0)
                    {
                        playerHandler.OnFirstLoad(connection);
                    }
                }
            }
        }

        private static PrepEntity loadPrepEntity(string profileID)
        {
            /*load the client's last know prep settings, useful
             for loading a game in progress*/

            //testing, instead of loading for now always create
            /*If unable to load, create a new profile for client and create
             a default PrepEntity*/
            return createPrepEntity();
        }

        /// <summary>
        /// Creates a new prep entity for a new client
        /// </summary>
        /// <returns></returns>
        private static PrepEntity createPrepEntity()
        {
            return PrepEntity.DefualtEntity();
        }
        #endregion
    }
}
