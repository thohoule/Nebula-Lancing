using Assets.Game.Character;
using Assets.Game;
using UnityEngine;
using Assets.Code.Gameplay.PlayingSM.UI;

namespace Interlace
{
    /// <summary>
    /// Scene object that stores needed assets to connect to a player of the same seat.
    /// </summary>
    public class SeatObject : MonoBehaviour
    {
        [SerializeField]
        private PlayerUnpacker unpacker;
        [SerializeField]
        private int seatNumber;
        [SerializeField]
        private PrepShipBase mockShip;
        [SerializeField]
        private PrepUIBase prepUI;
        [SerializeField]
        private GameObject spawnPlacement;

        public PlayerUnpacker Unpacker { get => unpacker; }
        public int SeatNumber { get => seatNumber; }
        public PrepShipBase MockShip { get => mockShip; }
        public PrepUIBase PrepUI { get => prepUI; }
        public Vector3 SpawnPosition { get => spawnPlacement.transform.position; }
    }
}
