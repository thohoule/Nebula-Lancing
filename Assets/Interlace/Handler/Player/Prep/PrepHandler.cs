using System;
using UnityEngine;
using FishNet.Object;
using FishNet.Connection;
using Assets.Game;
using Assets.Game.Character;
using Interlace.Sync;
using Assets.Entities;
using Assets.Services;
using Assets.Game.UI.Prep;

namespace Interlace
{
    [RequireComponent(typeof(PlayerHandler2))]
    public partial class PrepHandler : NetworkBehaviour
    {
        public const string Small_Ship_Text = "Small Ship";
        public const string Medium_Ship_Text = "Medium Ship";
        public const string Large_Ship_Text = "Large Ship";

        private PlayerHandler2 playerHandler;

        private int seatNumber { get => playerHandler.ProfileHandler.SeatNumber; }
        private PrepEntity prep { get => playerHandler.ProfileHandler.Entity.Prep; }

        public bool OwnershipEnabled { get; private set; }
        public ClientLocalMethods ClientLocal { get; private set; }

        private void Awake()
        {
            playerHandler = GetComponent<PlayerHandler2>();
            ClientLocal = new ClientLocalMethods(this);
        }

        #region Downsteam
        [ObserversRpc]
        [TargetRpc]
        internal void OnFirstLoad(NetworkConnection connection)
        {
            ClientLocal.RefreshMockShip();
        }

        [TargetRpc]
        internal void OnEnableEdit(NetworkConnection connection)
        {
            var ui = PrepService.GetPrepUI(seatNumber);
            ui.EnableOwnerEdit();
            OwnershipEnabled = true;
        }
        #endregion

        #region Local
        //public void RefreshMockShip()
        //{
        //    var mockShip = PrepService.GetMockShip(seatNumber);
        //    mockShip.SetShip(prep);
        //}

        //public void RefreshShipTypeName()
        //{
        //    var ui = PrepService.GetPrepUI(seatNumber);

        //    switch (prep.SelectedShip)
        //    {
        //        case 0:
        //            ui.SetSelectShipText(Small_Ship_Text);
        //            break;
        //        case 1:
        //            ui.SetSelectShipText(Medium_Ship_Text);
        //            break;
        //        case 2:
        //            ui.SetSelectShipText(Large_Ship_Text);
        //            break;
        //    }
        //}

        //public void RefreshReadyState()
        //{
        //    var ui = PrepService.GetPrepUI(seatNumber);
        //    ui.SetReadyState(prep.IsReady);
        //}

        //public void StartPrepState()
        //{
        //    var mockShip = PrepService.GetMockShip(seatNumber);
        //    mockShip.gameObject.SetActive(true);
        //    mockShip.PlayEnterAnimation();

        //    var ui = PrepService.GetPrepUI(seatNumber);
        //    ui.gameObject.SetActive(true);
        //}

        //public void EndPrepState()
        //{
        //    var ui = PrepService.GetPrepUI(seatNumber);
        //    ui.gameObject.SetActive(false);
        //}

        //public void OnTransitionStart()
        //{
        //    var mockShip = PrepService.GetMockShip(seatNumber);
        //    mockShip.PlayTransitionAnimation();
        //}

        //public void OnTransitionEnd()
        //{
        //    var mockShip = PrepService.GetMockShip(seatNumber);
        //    mockShip.gameObject.SetActive(false);
        //}
        #endregion
    }
}
