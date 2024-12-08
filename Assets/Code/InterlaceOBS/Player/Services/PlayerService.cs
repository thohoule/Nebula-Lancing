using Assets.Code.Gameplay;
using Assets.Code.Gameplay.Network;
using System;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.Characters;

namespace Assets.Code.Gameplay
{
    public class PlayerService : MonoBehaviour
    {
        //private PlayerClient player1;
        //private PlayerClient player2;
        //private PlayerClient player3;
        //private PlayerClient player4;

        //[SerializeField]
        //private PlayerClientOwnerHandler ownerHandler;
        //[SerializeField]
        //private OwnerPlayer owner;
        [SerializeField]
        private PlayerClientHandler ownerHandler;
        [SerializeField]
        private NetActorHandler mainHandler;
        [SerializeField]
        private PlayerClientHandler player1Handler;
        [SerializeField]
        private PlayerClientHandler player2Handler;
        [SerializeField]
        private PlayerClientHandler player3Handler;
        [SerializeField]
        private PlayerClientHandler player4Handler;
        [SerializeField]
        private AvatarSpawn spawn1;

        private static PlayerService instance;

        //internal static OwnerPlayer Owner { get => instance.owner; }
        internal static PlayerClientHandler OwnerHandler { get => instance.ownerHandler; }
        internal static NetActorHandler MainAvatarHandler { get => instance.mainHandler; set => instance.mainHandler = value; }
        //internal static PlayerClientOwnerHandler OwnerHandler { get => instance.ownerHandler; }
        public static PlayerClientHandler Player1 { get => instance.player1Handler; }
        public static PlayerClientHandler Player2 { get => instance.player2Handler; }
        public static PlayerClientHandler Player3 { get => instance.player3Handler; }
        public static PlayerClientHandler Player4 { get => instance.player4Handler; }
        //public static PlayerClient OwnerPlayer { get => OwnerHandler.Player; }
        public static int ActiveCount { get => getCount(); }

        private void Awake()
        {
            instance = this;
        }

        //public static void SetPlayer(PlayerClient player)
        //{
        //    switch (player.SeatNumber)
        //    {
        //        case 1:
        //            if (instance.player1Handler.IsAssigned)
        //                throw new InvalidOperationException("Player 1 was already set.");
        //            //instance.player1Handler.as(player);
        //            ActiveCount++;
        //            break;
        //        case 2:
        //            if (instance.player2 != null)
        //                throw new InvalidOperationException("Player 2 was already set.");
        //            instance.player2 = player;
        //            ActiveCount++;
        //            break;
        //        case 3:
        //            if (instance.player3 != null)
        //                throw new InvalidOperationException("Player 3 was already set.");
        //            instance.player3 = player;
        //            ActiveCount++;
        //            break;
        //        case 4:
        //            if (instance.player4 != null)
        //                throw new InvalidOperationException("Player 4 was already set.");
        //            instance.player4 = player;
        //            ActiveCount++;
        //            break;
        //    }
        //}

        public static IEnumerable<PlayerClientHandler> GetActivePlayers()
        {
            if (Player1.IsAssigned)
                yield return Player1;
            if (Player2.IsAssigned)
                yield return Player2;
            if (Player3.IsAssigned)
                yield return Player3;
            if (Player4.IsAssigned)
                yield return Player4;
        }

        internal static void SetOwnerHandler(PlayerClientHandler handler)
        {
            instance.ownerHandler = handler;
        }

        private static int getCount()
        {
            int amount = 0;

            if (Player1.IsAssigned)
                amount++;
            if (Player2.IsAssigned)
                amount++;
            if (Player3.IsAssigned)
                amount++;
            if (Player4.IsAssigned)
                amount++;

            return amount;
        }

        internal static void useSpawn1(RegisteredClient client)
        {
            Debug.Log("Spawn used");
            instance.spawn1.UseSpawn(client);
        }
    }
}
