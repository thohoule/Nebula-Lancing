//using System;
//using System.Collections.Generic;
//using FishNet.Object;
//using FishNet.Connection;
//using Assets.Code.Gameplay;

//namespace Assets.Code.Interlace.Actor
//{
//    public class MainAvatar : NetworkBehaviour
//    {
//        private NetActorHandler handler;

//        #region Upstream
//        [ServerRpc]
//        public void FirePrimary()
//        {
//            /*possible bug, server side of OwnerPlayer's handler was never set
//             Likewise, this class's handler won'y be set server side*/
//            handler.Actor.PrimaryWeapon.Fire();
//        }
//        #endregion
//    }
//}
