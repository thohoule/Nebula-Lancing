using System;
using UnityEngine;
using TeaSteep;
using FishNet.Object;
using Assets.Code.Gameplay.GameSM;
using Assets.Code.Gameplay;

namespace Assets.Code.Gameplay.Network
{
    public class HostSetup : NetworkBehaviour
    {
        [SerializeField]
        private GameLoopControl gameControl;
        [SerializeField]
        private PlayingInterlace playingInterlace;
        [SerializeField]
        private LobbyInterlace lobbyInterlace;

        public override void OnStartServer()
        {
            base.OnStartServer();

            if (IsServer)
            {
                gameControl.gameObject.SetActive(true);
                addPlayingInterlaceControl();
                addLobbyInterlaceControl();
            }
        }

        private void addPlayingInterlaceControl()
        {
            GameObject interlaceObject = new GameObject("Playing Interlace Control");
            interlaceObject.SetParentAndLocals(gameObject);

            var control = interlaceObject.AddComponent<PlayingInterlaceControl>();
            control.Initialize(playingInterlace);
        }

        private void addLobbyInterlaceControl()
        {
            GameObject interlaceObject = new GameObject("Lobby Interlace Control");
            interlaceObject.SetParentAndLocals(gameObject);

            var control = interlaceObject.AddComponent<LobbyInterlaceControl>();
            control.Initialize(lobbyInterlace);
        }
    }
}
