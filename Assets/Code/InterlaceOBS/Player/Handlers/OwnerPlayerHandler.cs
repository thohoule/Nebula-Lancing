//using System;
//using System.Collections.Generic;
//using FishNet.Object;
//using FishNet.Connection;
//using Assets.Code.Gameplay;
//using FishNet.Demo.AdditiveScenes;

//namespace Assets.Code.Gameplay
//{
//    public class OwnerPlayerHandler : NetworkBehaviour
//    {
//        private PlayerClient playerClient;
//        public PlayerClient Player { get => playerClient; }

//        #region Downstream
//        [TargetRpc]
//        internal void OnOwnerAssign(NetworkConnection connection,
//            PlayerClient player)
//        {
//            playerClient = player;
//        }
//        #endregion

//        #region Upstream
//        [ServerRpc]
//        public void ToggleReadyState(NetworkConnection connection = null)
//        {
//            OnReadyChange(!player.Prep.IsReady);
//        }
//        #endregion
//    }
//}
