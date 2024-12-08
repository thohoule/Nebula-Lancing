using FishNet.Connection;
using System;
using System.Collections.Generic;
using UnityEngine;
using Interlace;

namespace Assets.Services
{
    public class LobbyService : MonoBehaviour
    {
        private static LobbyService instance;

        private Dictionary<int, int> connectionIdToSeat;

        [SerializeField]
        private PlayerHandler player1;
        [SerializeField]
        private PlayerHandler player2;
        [SerializeField]
        private PlayerHandler player3;
        [SerializeField]
        private PlayerHandler player4;

        public static PlayerHandler Player1 { get => instance.player1; }
        public static PlayerHandler Player2 { get => instance.player2; }
        public static PlayerHandler Player3 { get => instance.player3; }
        public static PlayerHandler Player4 { get => instance.player4; }

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
                return;
            }

            instance = this;
            connectionIdToSeat = new Dictionary<int, int>();
        }

        #region Get
        public static PlayerHandler GetPlayerBySeat(int seat)
        {
            switch (seat)
            {
                case 1: return instance.player1;
                case 2: return instance.player2;
                case 3: return instance.player3;
                case 4: return instance.player4;
                default: return null;
            }
        }

        internal static PlayerHandler GetMemberByConnection(NetworkConnection connection)
        {
            if (instance.connectionIdToSeat.TryGetValue(connection.ClientId,
                out int seat))
            {
                return GetPlayerBySeat(seat);
            }

            Debug.LogError(string.Format("Unable to find player for connection {0}",
                connection.ClientId));
            return null;
        }

        internal static bool TryGetMemberByConnection(NetworkConnection connection,
            out PlayerHandler playerHandler)
        {
            if (instance.connectionIdToSeat.TryGetValue(connection.ClientId,
                out int seat))
            {
                playerHandler = GetPlayerBySeat(seat);
                return true;
            }

            playerHandler = null;
            return false;
        }

        public static IEnumerable<PlayerHandler> GetActivePlayers()
        {
            if (Player1.IsAssignedPlayer)
                yield return Player1;
            if (Player2.IsAssignedPlayer)
                yield return Player2;
            if (Player3.IsAssignedPlayer)
                yield return Player3;
            if (Player4.IsAssignedPlayer)
                yield return Player4;
        }
        #endregion

        #region Assign
        internal static bool TryGetEmptySeat(NetworkConnection connection,
            out PlayerHandler handler)
        {
            if (!Player1.IsAssignedPlayer)
            {
                instance.connectionIdToSeat.Add(connection.ClientId, 1);
                handler = Player1;
                return true;
            }
            if (!Player2.IsAssignedPlayer)
            {
                instance.connectionIdToSeat.Add(connection.ClientId, 2);
                handler = Player2;
                return true;
            }
            if (!Player3.IsAssignedPlayer)
            {
                instance.connectionIdToSeat.Add(connection.ClientId, 3);
                handler = Player3;
                return true;
            }
            if (!Player4.IsAssignedPlayer)
            {
                instance.connectionIdToSeat.Add(connection.ClientId, 4);
                handler = Player4;
                return true;
            }

            handler = null;
            return false;
        }

        //public static bool CanBeSeated(NetworkConnection connection, string playerName)
        //{
        //    if (instance.connectionIdToSeat.TryGetValue(connection.ClientId,
        //        out int seat))
        //    {

        //    }

        //    PlayerEntity player = new PlayerEntity()
        //    {
        //        Connection = connection,
        //        ProfileName = playerName,
        //        Prep = new PrepEntity(),
        //        SeatNumber = 
        //    }
        //}
        #endregion
    }
}
