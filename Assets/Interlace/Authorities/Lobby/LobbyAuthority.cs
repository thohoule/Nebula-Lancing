using UnityEngine;
using FishNet.Connection;
using Assets.Services;
using System.Collections.Generic;
using Assets.Entities;
using TeaSteep;
using Assets.Services.Character;
using Assets.Code.Characters.Weapons.Primary;
using Assets.Game.Character;

namespace Interlace
{
    public class LobbyAuthority : MonoBehaviour
    {
        private Dictionary<int, int> connectionIdToSeat;
        private Dictionary<string, int> profileToSeat;

        [SerializeField, ReadOnly]
        private PlayerCoord player1Coord;
        [SerializeField, ReadOnly]
        private PlayerCoord player2Coord;

        private static LobbyAuthority instance;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
                return;
            }

            instance = this;
            connectionIdToSeat = new Dictionary<int, int>();
            profileToSeat = new Dictionary<string, int>();
        }

        #region Seat and Create
        internal static TransactionResult CanbeSeated(NetworkConnection connection,
            string profileID, out PlayerHandler2 handler)
        {
            if (instance.profileToSeat.TryGetValue(profileID, out int seat))
            {
                //reconnect
            }

            if (TryGetEmptySeat(connection, profileID, out PlayerUnpacker unpacker))
            {
                handler = instance.buildPlayer(unpacker, connection, profileID);
                return TransactionResult.Successful();
            }

            handler = null;
            return TransactionResult.Error("Join rejected, Lobby is full.");
        }

        internal static bool TryGetEmptySeat(NetworkConnection connection,
            string profileID, out PlayerUnpacker unpacker)
        {
            if (!LobbyService2.IsSeatAssigned(1))
            {
                instance.connectionIdToSeat.Add(connection.ClientId, 1);
                instance.profileToSeat.Add(profileID, 1);
                unpacker = PrepService.GetUnpacker(1);
                //unpacker = instance.seat1;
                return true;
            }
            if (!LobbyService2.IsSeatAssigned(2))
            {
                instance.connectionIdToSeat.Add(connection.ClientId, 2);
                instance.profileToSeat.Add(profileID, 2);
                unpacker = PrepService.GetUnpacker(2);
                //unpacker = instance.seat2;
                return true;
            }
            if (!LobbyService2.IsSeatAssigned(3))
            {
                instance.connectionIdToSeat.Add(connection.ClientId, 3);
                instance.profileToSeat.Add(profileID, 3);
                unpacker = PrepService.GetUnpacker(3);
                //unpacker = instance.seat3;
                return true;
            }
            if (!LobbyService2.IsSeatAssigned(4))
            {
                instance.connectionIdToSeat.Add(connection.ClientId, 4);
                instance.profileToSeat.Add(profileID, 4);
                unpacker = PrepService.GetUnpacker(4);
                //unpacker = instance.seat4;
                return true;
            }

            unpacker = null;
            return false;
        }

        private PlayerHandler2 buildPlayer(PlayerUnpacker unpacker,
            NetworkConnection connection, string profileID)
        {
            var prep = loadPrepEnitiy(profileID);

            PlayerEntity entity = new PlayerEntity()
            {
                Connection = connection,
                ProfileName = profileID,
                Prep = prep,
                SeatNumber = unpacker.SeatNumber
            };

            //SyncAuthority.SyncPlayer(connection);
            //syncAllHandlersToClient(connection);

            var coord = unpacker.Instantiate(out PlayerHandler2 handler,
                null);
            coord.gameObject.SetActive(false);
            reorder(connection, handler, entity);
            //LobbyService2.Instance.OnPlayerAssign(null, handler, unpacker.SeatNumber);

            switch (unpacker.SeatNumber)
            {
                case 1:
                    player1Coord = coord; break;
                case 2:
                    player2Coord = coord; break;
            }

            coord.PrimaryWeapon = new PlasmaGunWeapon(coord);

            //handler.ProfileHandler.OnEntityAssign(null, entity);
            //handler.PrepHandler.OnFirstLoad(null);
            //AvatarService2.ServerLocal.SetAvatar(connection, handler.ActorHandler);
            //SyncAuthority.SyncPlayer(connection);
            //handler.PrepHandler.OnEnableEdit(connection);

            return handler;
        }

        private void reorder(NetworkConnection connection, PlayerHandler2 handler,
            PlayerEntity entity)
        {
            SyncAuthority.SyncPlayer(connection);
            syncAllHandlersToClient(connection);
            handler.ProfileHandler.OnEntityAssign(null, entity);
            handler.PrepHandler.OnFirstLoad(null);
            AvatarService2.ServerLocal.SetAvatar(connection, handler.ActorHandler);

            foreach (var player in LobbyService2.GetActivePlayers())
            {
                LobbyService2.Instance.OnAssignOtherUIHandler(
                    player.ClientManager.Connection, handler);
            }

            LobbyService2.Instance.OnPlayerAssign(null, handler, entity.SeatNumber);
            handler.PrepHandler.OnEnableEdit(connection);
        }

        private PrepEntity loadPrepEnitiy(string profileID)
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

        private void syncAllHandlersToClient(NetworkConnection connection)
        {
            foreach (var player in LobbyService2.GetActivePlayers())
            {
                player.ProfileHandler.OnEntityAssign(connection,
                    player.ProfileHandler.Entity);

                LobbyService2.Instance.OnPlayerAssign(connection,
                    player, player.ProfileHandler.SeatNumber);

                LobbyService2.Instance.OnAssignOtherUIHandler(connection,
                    player);

                if (SyncAuthority.SyncValue == 0)
                    player.PrepHandler.OnFirstLoad(connection);
            }

            //for (int seat = 1; seat <= 4; seat++)
            //{
            //    var playerHandler = LobbyService2.GetPlayerBySeat(seat);
            //    if (playerHandler.IsAssignedPlayer)
            //    {
            //        playerHandler.OnAssignedConnection(connection,
            //            playerHandler.Entity);

            //        if (SyncControl.CurrentPhase == 0)
            //        {
            //            playerHandler.OnFirstLoad(connection);
            //        }
            //    }
            //}
        }
        #endregion

        #region Enable and Disable
        public static void EnableHandlers()
        {
            instance.enableHandlers();
        }

        private void enableHandlers()
        {
            if (player1Coord != null)
                player1Coord.Enable();
            if (player2Coord != null)
                player2Coord.Enable();
        }
        #endregion

        #region Spawn
        public static void SpawnPuppets()
        {
            if (instance.player1Coord != null)
                instance.player1Coord.SpawnPuppet();
            if (instance.player2Coord != null)
                instance.player2Coord.SpawnPuppet();
        }
        #endregion

        #region Get
        public static PlayerCoord GetCoordByConnection(NetworkConnection connection)
        {
            if (instance.connectionIdToSeat.TryGetValue(connection.ClientId, 
                out int seatNumber))
            {
                switch (seatNumber)
                {
                    case 1:
                        return instance.player1Coord;
                    case 2:
                        return instance.player2Coord;
                }
            }

            return null;
        }
        #endregion
    }
}
