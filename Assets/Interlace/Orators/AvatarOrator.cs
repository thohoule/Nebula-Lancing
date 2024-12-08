using UnityEngine;
using FishNet.Connection;
using FishNet.Object;
using Assets.Services;

namespace Interlace
{
    public class AvatarOrator : NetworkBehaviour
    {
        internal AvatarCoord Coord { get; set; }

        private static AvatarOrator instance;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
                return;
            }

            instance = this;
        }

        #region Upstream
        [ServerRpc(RequireOwnership = false)]
        private void firePrimary(int charge, NetworkConnection connection = null)
        {
            var playerCoord = LobbyAuthority.GetCoordByConnection(connection);
            playerCoord.OnPrimaryFire(charge);

            /*Charge is a simple example of how a control can tell the server,
             how much energy and time was spent charging up a shot without the 
            server needing those details.*/
            //Coord.OnPrimaryFire(charge);
        }

        [ServerRpc(RequireOwnership = false)]
        private void fireSecondary(int charge, NetworkConnection connection = null)
        {
            Coord?.OnSecondaryFire(charge);
        }

        public static void EchoTest()
        {
            Debug.Log("Test Dummy");
            instance.PositionEcho();
        }

        [ServerRpc (RequireOwnership = false)]
        public void PositionEcho(NetworkConnection connection = null)
        {
            var member = LobbyService2.GetMemberByConnection(connection);
            Debug.Log(string.Format("Server Pos: {0}", 
                member.ActorHandler.transform.position));

            EchoReturn(connection);
        }

        [TargetRpc]
        public void EchoReturn(NetworkConnection connection)
        {
            var member = LobbyService2.GetMemberByConnection(connection);
            Debug.Log(string.Format("Client Pos: {0}",
                member.ActorHandler.transform.position));
        }

        //[ServerRpc (RequireOwnership = false)]
        //private void onAvatarMovement(Vector2 movement, NetworkConnection connection = null)
        //{
        //    var coord = LobbyAuthority.GetCoordByConnection(connection);
        //    coord?.OnMove(movement);
        //}

        //[ServerRpc(RequireOwnership = false)]
        //public void SetHealth(int value, NetworkConnection connection = null)
        //{
        //    var player = LobbyService.GetMemberByConnection(connection);
        //    player.ActorCoord.Health = value;
        //}

        //[ServerRpc(RequireOwnership = false)]
        //internal void Set

        //[ServerRpc(RequireOwnership = false)]
        //internal void Move(Vector2 direction, NetworkConnection connection = null)
        //{

        //}
        #endregion

        #region Static
        public static void FirePrimary(int charge)
        {
            instance.firePrimary(charge);
        }

        public static void FireSecondary(int charge)
        {
            instance.fireSecondary(charge);
        }
        #endregion
    }
}
