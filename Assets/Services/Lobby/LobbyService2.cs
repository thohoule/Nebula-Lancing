using FishNet.Connection;
using FishNet.Object;
using System;
using System.Collections.Generic;
using UnityEngine;
using Interlace;
using TeaSteep;
using Assets.Services.Player;

namespace Assets.Services
{
    public class LobbyService2 : NetworkBehaviour
    {
        //private Dictionary<int, int> connectionIdToSeat;

        [SerializeField, ReadOnly]
        private PlayerHandler2 player1;
        [SerializeField, ReadOnly]
        private PlayerHandler2 player2;
        [SerializeField, ReadOnly]
        private PlayerHandler2 player3;
        [SerializeField, ReadOnly]
        private PlayerHandler2 player4;

        public static LobbyService2 Instance;

        public static PlayerHandler2 Player1 { get => Instance.player1; }
        public static PlayerHandler2 Player2 { get => Instance.player2; }
        public static PlayerHandler2 Player3 { get => Instance.player3; }
        public static PlayerHandler2 Player4 { get => Instance.player4; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }

            Instance = this;
        }

        public static bool IsSeatAssigned(int seatNumber)
        {
            return Instance.getHandler(seatNumber) != null;
        }

        #region Get
        public static PlayerHandler2 GetMemberByConnection(NetworkConnection connection)
        {
            //if (instance.connectionIdToSeat.TryGetValue(connection.ClientId,
            //    out int seat))
            //{
            //    return GetPlayerBySeat(seat);
            //}

            if (TryGetMemberByConnection(connection, out PlayerHandler2 handler))
                return handler;

            Debug.LogError(string.Format("Unable to find player for connection {0}",
                connection.ClientId));
            return null;
        }

        public static bool TryGetMemberByConnection(NetworkConnection connection,
            out PlayerHandler2 playerHandler)
        {
            //if (instance.connectionIdToSeat.TryGetValue(connection.ClientId,
            //    out int seat))
            //{
            //    playerHandler = GetPlayerBySeat(seat);
            //    return true;
            //}

            foreach (var player in GetActivePlayers())
            {
                if (connection.ClientId == 
                    player.ProfileHandler.Entity.Connection.ClientId)
                {
                    playerHandler = player;
                    return true;
                }
            }

            playerHandler = null;
            return false;
        }

        public static ProfileHandler GetProfileByConnection(NetworkConnection connection)
        {
            return GetMemberByConnection(connection)?.ProfileHandler ?? null;
        }

        public static PlayerHandler2 GetPlayerBySeat(int seat)
        {
            switch (seat)
            {
                case 1: return Instance.player1;
                case 2: return Instance.player2;
                case 3: return Instance.player3;
                case 4: return Instance.player4;
                default: return null;
            }
        }

        public static IEnumerable<PlayerHandler2> GetActivePlayers()
        {
            if (Player1 != null)
                yield return Player1;
            if (Player2 != null)
                yield return Player2;
            if (Player3 != null)
                yield return Player3;
            if (Player4 != null)
                yield return Player4;
        }

        private PlayerHandler2 getHandler(int seatNumber)
        {
            switch (seatNumber)
            {
                case 1:
                    return player1;
                case 2:
                    return player2;
                case 3:
                    return player3;
                case 4:
                    return player4;
                default:
                    return null;
            }
        }

        #endregion

        #region Downstream
        [TargetRpc, ObserversRpc]
        internal void OnPlayerAssign(NetworkConnection connection, PlayerHandler2 handler, int seatNumber)
        {
            switch (seatNumber)
            {
                case 1:
                    player1 = handler;
                    break;
                case 2:
                    player2 = handler;
                    break;
                case 3:
                    player3 = handler;
                    break;
                case 4:
                    player4 = handler;
                    break;
            }

            SyncService.Instance.syncHandler(handler);
        }

        [TargetRpc]
        internal void OnAssignOtherUIHandler(NetworkConnection connection, PlayerHandler2 handler)
        {
            handler.ActorHandler.StatusUI = StatusService.GetNextOther();
        }
        #endregion
    }
}
