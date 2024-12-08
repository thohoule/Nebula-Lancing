using Assets.Game;
using System;
using UnityEngine;

namespace Interlace
{
    [Serializable]
    public class SeatInfo
    {
        [SerializeField]
        private int seatNumber;
        [SerializeField]
        private PrepShipBase mockShip;
        [SerializeField]
        private PrepUIBase prepUI;
        [SerializeField]
        private GameObject spawnPlacement;

        public int SeatNumber { get => seatNumber; }
        public PrepShipBase MockShip { get => mockShip; }
        public PrepUIBase PrepUI { get => prepUI; }
        public Vector3 SpawnPosition { get => spawnPlacement.transform.position; }
    }
}
