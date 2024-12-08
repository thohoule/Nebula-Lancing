using UnityEngine;
using TeaSteep;
using FishNet.Object;
using FishNet.Object.Synchronizing;

namespace Assets.Code.Gameplay
{
    public class LobbyInterlaceControl : NetworkBehaviour
    {
        private LobbyInterlace interlace;

        private static LobbyInterlaceControl instance;

        public void Initialize(LobbyInterlace lobbyInterlace)
        {
            if (instance == null)
            {
                instance = this;
                interlace = lobbyInterlace;
            }
        }

        public static void SetSeat1(PlayerClient player)
        {
            instance?.interlace.SetSeat1(player);
        }
    }
}
