using System;
using UnityEngine;
using FishNet.Object;
using FishNet.Connection;
using Assets.Game;
using Assets.Game.Character;
using Interlace.Sync;
using Assets.Entities;
using Assets.Code.Gameplay;

namespace Interlace
{
    public class PlayerHandler : NetworkBehaviour
    {
        public const string Small_Ship_Text = "Small Ship";
        public const string Medium_Ship_Text = "Medium Ship";
        public const string Large_Ship_Text = "Large Ship";

        internal PlayerEntity Entity { get; private set; }
        //internal
        //internal AvatarHandler AvatarHandler { get; set; }

        [SerializeField]
        private int seatNumber;
        [SerializeField]
        private PrepShipBase mockShip;
        [SerializeField]
        private PrepUIBase prepUI;
        [SerializeField]
        private AvatarSpawnBase spawnPoint;

        public int SeatNumber { get => seatNumber; }
        public bool IsAssignedPlayer { get; internal set; }
        public bool OwnershipEnabled { get; private set; }
        public bool IsDisconnected { get; private set; }

        public AvatarCoord Coord { get; set; }

        public override void OnStartClient()
        {
            base.OnStartClient();

            //OnResyncRequest(); --Need to access through service to Orator
        }

        #region Downstream
        [ObserversRpc]
        internal void OnSetShipType(int shipType)
        {
            Entity.Prep.SelectedShip = shipType;
            //set model and UI
            mockShip.SetShip(Entity.Prep);

            switch (Entity.Prep.SelectedShip)
            {
                case 0:
                    prepUI.SetSelectShipText(Small_Ship_Text);
                    break;
                case 1:
                    prepUI.SetSelectShipText(Medium_Ship_Text);
                    break;
                case 2:
                    prepUI.SetSelectShipText(Large_Ship_Text);
                    break;
            }
        }

        [ObserversRpc(RunLocally = true)]
        internal void OnReadyChange(bool readyState)
        {
            Entity.Prep.IsReady = readyState;
            prepUI.SetReadyState(readyState);
        }

        [ObserversRpc]
        [TargetRpc]
        internal void OnAssignedConnection(NetworkConnection connection, 
            PlayerEntity player)
        {
            this.Entity = player;
        }

        [ObserversRpc]
        [TargetRpc]
        internal void OnFirstLoad(NetworkConnection connection)
        {
            //Load Prep model if in prep state
            //prepUI.gameObject.SetActive(true);
            OnSetShipType(Entity.Prep.SelectedShip); //this always has to be assigned
            //mockShip.gameObject.SetActive(true);
            //mockShip.PlayEnterAnimation();

            if (SyncLobbyState.SyncCheck())
                OnLobbyStart();
        }

        [TargetRpc]
        internal void OnEnableEdit(NetworkConnection connection)
        {
            prepUI.EnableOwnerEdit();
            OwnershipEnabled = true;
        }

        [TargetRpc]
        internal void OnSyncObserver(NetworkConnection connection,
            PlayerEntity player)
        {
            //OnAssignedConnection(null, player);
            //OnSetShipType();
        }

        [ObserversRpc]
        internal void OnUnexpectedDisconnect()
        {
            IsDisconnected = true;
        }
        #endregion

        #region Upstream
        //[ServerRpc(RequireOwnership = false)]
        //internal void OnResyncRequest(NetworkConnection connection = null)
        //{
        //    OnSyncObserver(connection, player);
        //}
        #endregion

        #region Local
        public void OnLobbyStart()
        {
            prepUI.gameObject.SetActive(true);
            mockShip.gameObject.SetActive(true);
            mockShip.PlayEnterAnimation();
        }

        public void OnLobbyEnd()
        {
            prepUI.gameObject.SetActive(false);
        }

        public void OnTransitionStart()
        {
            mockShip.PlayTransitionAnimation();
        }

        public void OnTransitionEnd()
        {
            mockShip.gameObject.SetActive(false);
        }

        public void OnEncounterStart()
        {

        }
        #endregion

        #region Server
        internal void UseSpawn()
        {
            var puppet = spawnPoint.UseSpawn(Entity);
            Coord = puppet.gameObject.AddComponent<AvatarCoord>();

            Coord.Assign(puppet);
        }
        #endregion
    }
}
