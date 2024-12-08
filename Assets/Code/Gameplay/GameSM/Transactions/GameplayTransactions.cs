using System;
using System.Collections.Generic;
using System.Linq;
using FishNet;
using FishNet.Object;
using FishNet.Connection;
using FishNet.Transporting;
using UnityEngine;
using Assets.Code.Gameplay.Network;

namespace Assets.Code.Gameplay.GameSM
{
    public class GameplayTransactions : NetworkBehaviour
    {
        [SerializeField]
        private GameLoopControl loopControl;

        private static GameplayTransactions instance;
        private static TransactionThread current;

        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogError("GameplayTransactions: Only one instance allowed.");
                return;
            }

            instance = this;
        }

        public override void OnStartServer()
        {
            base.OnStartServer();

            loopControl.OnHostStart();
        }

        //public static void EnablePlayControls()//TransactionHandler handler)
        //{
        //    if (/*current.IsLocked || */!instance.IsServer)
        //        return;

        //    //current.Begin(handler);

        //    instance.enableAllPlayersControl();
        //    //current.End(TransactionResult.Successful());
        //}

        //[ObserversRpc]
        //private void enableAllPlayersControl()
        //{
        //    PlayingSM.PlayingControl.EnableControlling();
        //}
    }
}
