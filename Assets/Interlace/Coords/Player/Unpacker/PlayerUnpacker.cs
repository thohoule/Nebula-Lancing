using UnityEngine;

namespace Interlace
{
    public class PlayerUnpacker : CoordUnpacker<PlayerHandler2, PlayerCoord>
    {
        [SerializeField]
        private int seatNumber;

        public int SeatNumber { get => seatNumber; }
    }
}
