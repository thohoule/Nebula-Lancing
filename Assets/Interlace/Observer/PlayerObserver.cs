//using System;
//using UnityEngine;
//using FishNet.Object;
//using FishNet.Connection;
//using Interlace.Objects;

//namespace Interlace
//{
//    public class PlayerObserver : NetworkBehaviour
//    {
//        public const string Small_Ship_Text = "Small Ship";
//        public const string Medium_Ship_Text = "Medium Ship";
//        public const string Large_Ship_Text = "Large Ship";

//        private PlayerEntity player;

//        [SerializeField]
//        private int seatNumber;
//        [SerializeField]
//        private PrepShipBase mockShip;
//        [SerializeField]
//        private PrepUIBase prepUI;
//        [SerializeField]
//        private AvatarSpawnBase spawnPoint;

//        public override void OnStartClient()
//        {
//            base.OnStartClient();

//            OnResyncRequest();
//        }

//        #region Downstream
//        [ObserversRpc]
//        internal void OnSetShipType(int shipType)
//        {
//            player.Prep.SelectedShip = shipType;
//            //set model and UI
//            mockShip.SetShip(player.Prep);

//            switch (player.Prep.SelectedShip)
//            {
//                case 0:
//                    prepUI.SetSelectShipText(Small_Ship_Text);
//                    break;
//                case 1:
//                    prepUI.SetSelectShipText(Medium_Ship_Text);
//                    break;
//                case 2:
//                    prepUI.SetSelectShipText(Large_Ship_Text);
//                    break;
//            }
//        }

//        [ObserversRpc]
//        internal void OnAssignedConnection(PlayerEntity player)
//        {
//            this.player = player;
//        }

//        [TargetRpc]
//        internal void OnSyncObserver(NetworkConnection connection, 
//            PlayerEntity player)
//        {
//            OnAssignedConnection(player);
//            //OnSetShipType();
//        }
//        #endregion

//        #region Upstream
//        [ServerRpc (RequireOwnership = false)]
//        internal void OnResyncRequest(NetworkConnection connection = null)
//        {
//            OnSyncObserver(connection, player);
//        }
//        #endregion

//        #region Server
//        public void UseSpawn()
//        {

//        }
//        #endregion
//    }
//}
