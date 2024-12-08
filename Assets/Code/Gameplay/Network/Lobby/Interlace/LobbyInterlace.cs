using UnityEngine;
using TeaSteep;
using FishNet.Object;
using FishNet.Object.Synchronizing;

namespace Assets.Code.Gameplay
{
    public class LobbyInterlace : NetworkBehaviour
    {
        private static LobbyInterlace instance;

        [SyncVar, SerializeField]//ser is temp
        private PlayerClient seat1;

        private void Awake()
        {
            instance = this;
        }

        public static PlayerClient GetPlayer1()
        {
            return instance.seat1;
        }

        internal void SetSeat1(PlayerClient client)
        {
            seat1 = client;
        }
    }
}
