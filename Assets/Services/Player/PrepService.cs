using System;
using UnityEngine;
using FishNet.Object;
using FishNet.Connection;
using Assets.Game;
using Assets.Game.Character;
using Interlace.Sync;
using Assets.Entities;
using Assets.Code.Gameplay;
using System.Collections.Generic;
using Interlace;
using Assets.Code.Gameplay.PlayingSM.UI;

namespace Assets.Services
{
    public class PrepService : NetworkBehaviour
    {
        [SerializeField]
        private SeatObject seat1;
        [SerializeField]
        private SeatObject seat2;
        [SerializeField]
        private SeatObject seat3;
        [SerializeField]
        private SeatObject seat4;

        private static PrepService instance;

        private void Awake()
        {
            instance = this;
        }

        private PrepShipBase getMockShip(int seatNumber)
        {
            return getSeat(seatNumber).MockShip;
        }

        private PrepUIBase getUI(int seatNumber)
        {
            return getSeat(seatNumber).PrepUI;
        }

        private Vector3 getSpawnPosition(int seatNumber)
        {
            return getSeat(seatNumber).SpawnPosition;
        }

        public PlayerUnpacker getUnpacker(int seatNumber)
        {
            return getSeat(seatNumber).Unpacker;
        }

        private SeatObject getSeat(int seatNumber)
        {
            switch (seatNumber)
            {
                case 1:
                    return seat1;
                case 2:
                    return seat2;
                case 3:
                    return seat3;
                default:
                    return seat4;
            }
        }

        public static PrepShipBase GetMockShip(int seatNumber)
        {
            return instance.getMockShip(seatNumber);
        }

        public static PrepUIBase GetPrepUI(int seatNumber)
        {
            return instance.getUI(seatNumber);
        }

        public static Vector3 GetSpawnPosition(int seatNumber)
        {
            return instance.getSpawnPosition(seatNumber);
        }

        public static PlayerUnpacker GetUnpacker(int seatNumber)
        {
            return instance.getUnpacker(seatNumber);
        }
    }
}
