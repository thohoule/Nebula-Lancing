using System;
using UnityEngine;
using FishNet.Object;
using FishNet.Connection;
using Assets.Game;
using Assets.Game.Character;
using Interlace.Sync;
using Assets.Entities;
using Assets.Code.Gameplay;
using Assets.Game.UI.Prep;

namespace Interlace
{
    [RequireComponent(typeof(PlayerHandler2))]
    public class ProfileHandler : NetworkBehaviour //Prep Handler
    {
        private PlayerHandler2 playerHandler;

        internal PlayerEntity Entity { get; private set; }

        public int SeatNumber { get => Entity.SeatNumber; }

        private void Awake()
        {
            playerHandler = GetComponent<PlayerHandler2>();
        }

        #region Downstream
        [ObserversRpc]
        [TargetRpc]
        internal void OnEntityAssign(NetworkConnection connection, 
            PlayerEntity entity)
        {
            Entity = entity;
        }

        [ObserversRpc]
        internal void OnSetShipType(int shipType)
        {
            Entity.Prep.SelectedShip = shipType;
            playerHandler.PrepHandler.ClientLocal.RefreshMockShip();
            playerHandler.PrepHandler.ClientLocal.RefreshShipTypeName();
        }

        [ObserversRpc(RunLocally = true)]
        internal void OnReadyChange(bool readyState)
        {
            Entity.Prep.IsReady = readyState;
            playerHandler.PrepHandler.ClientLocal.RefreshReadyState();
        }
        #endregion
    }
}
