using Assets.Game;
using FishNet;
using FishNet.Object;
using UnityEngine;

namespace Interlace
{
    public class PrepServiceHandler : NetworkBehaviour
    {
        [SerializeField]
        private OwnerOrator ownerOrator;
        [SerializeField]
        private LobbyUIBase lobbyUI;

        private static PrepServiceHandler instance;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
                return;
            }

            instance = this;
        }

        #region Downstream
        //[ObserversRpc]
        //internal void OnSetReadyTimer(float value)
        //{
        //    lobbyUI.SetReadyTimerText(value.ToString());
        //}

        [ObserversRpc]
        internal void OnStartCountdown(float time)
        {
            lobbyUI.StartCountdown(time);
        }

        [ObserversRpc]
        internal void OnHideCountdownTimer()
        {
            lobbyUI.HideCountdown();
        }
        #endregion

        //public static void CycleShipSelectionLeft()
        //{
        //    instance.ownerOrator.CycleShipSelectionLeft();
        //}

        //public static void CycleShipSelectionRight()
        //{
        //    instance.ownerOrator.CycleShipSelectionRight();
        //}

        //public static void ToggleReady()
        //{
        //    instance.ownerOrator.ToggleReadyState();
        //}

        //public static void SetShipSelection()
        //{

        //}

        //public static void SetReadyTimerUI(float value)
        //{
        //    instance.OnSetReadyTimer(value);
        //}

        public static void StartCountdown(float time)
        {
            instance.OnStartCountdown(time);
        }

        public static void HideCountdownTimer()
        {
            instance.OnHideCountdownTimer();
        }
    }
}
