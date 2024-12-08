using System;
using System.Collections.Generic;
using FishNet.Object;
using FishNet.Connection;
using Assets.Code.Gameplay;

namespace Assets.Code.Gameplay
{
    public class PlayerClientOwnerHandler : PlayerClientHandler
    {
        public PlayerClient Player { get => player; }

        #region Downstream
        [TargetRpc]
        internal void OnOwnerAssign(NetworkConnection connection,
            PlayerClient player)
        {
            this.player = player;
        }
        #endregion

        #region Upstream
        [ServerRpc]
        public void ToggleReadyState(NetworkConnection connection = null)
        {
            OnReadyChange(!player.Prep.IsReady);
        }
        #endregion
    }
}
