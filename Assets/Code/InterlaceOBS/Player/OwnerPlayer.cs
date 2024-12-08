using System;
using System.Collections.Generic;
using FishNet.Object;
using FishNet.Connection;
using Assets.Code.Gameplay;
using UnityEngine;

namespace Assets.Code.Gameplay
{
    public class OwnerPlayer : NetworkBehaviour
    {
        private PlayerClientHandler playerHandler;

        public PlayerClient Player { get => playerHandler?.Player; }

        internal void SetHandler(PlayerClientHandler handler)
        {
            playerHandler = handler;
        }

        #region Upstream
        [ServerRpc]
        public void SetShipType(int typeValue,
            NetworkConnection connection = null)
        {
            playerHandler.OnSetShipType(typeValue);
        }

        [ServerRpc]
        public void SetPrimaryWeapon(string weaponAsset, 
            NetworkConnection connection = null)
        {
            playerHandler.OnSetPrimaryWeapon(weaponAsset);
        }

        [ServerRpc]
        public void SetSecondaryWeapon(string weaponAsset,
            NetworkConnection connection = null)
        {
            playerHandler.OnSetSecondaryWeapon(weaponAsset);
        }

        [ServerRpc]
        public void ToggleReadyState(NetworkConnection connection = null)
        {
            Debug.Log("Toggle Server hit");
            playerHandler.OnReadyChange(!Player.Prep.IsReady);
        }
        #endregion
    }
}
