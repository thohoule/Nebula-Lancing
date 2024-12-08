//using System;
//using System.Collections.Generic;
//using System.Linq;
//using FishNet;
//using FishNet.Object;
//using FishNet.Connection;
//using FishNet.Transporting;
//using UnityEngine;
//using Assets.Code.Gameplay.Network;

//namespace Assets.Code.Gameplay.GameSM
//{
//    public class PreparingTransaction : NetworkBehaviour
//    {
//        private static PreparingTransaction instance;
//        private static TransactionThread current;

//        #region Set Equipment
//        #endregion

//        #region Ready Up
//        public static void ReadyUp(TransactionHandler handler)
//        {
//            if (current.IsLocked)
//                return;

//            current.Begin(handler);


//        }

//        public static void UnreadyUp(TransactionHandler handler)
//        {
//            if (current.IsLocked)
//                return;

//            current.Begin(handler);
//        }

//        private void setReadyState(bool value)
//        {
//            setClientReady(value);
//            current.End(TransactionResult.Successful());
//        }

//        [ServerRpc(RequireOwnership = false)]
//        private void setClientReady(bool value, NetworkConnection connection = null)
//        {
//            var client = Lobby.GetClientByConnection(connection);
//            client.Player.Prep.IsReady = true;
//        }
//        #endregion

//        #region Start Transition
//        public static void StartTransition(TransactionHandler handler)
//        {
//            if (current.IsLocked)
//                return;

//            current.Begin(handler);
//        }
//        #endregion
//    }
//}
