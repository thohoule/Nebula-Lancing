using UnityEngine;
using FishNet.Connection;
using FishNet.Object;
using Assets.Game;
using Assets.Services;

namespace Interlace
{
    public class OwnerOrator : NetworkBehaviour
    {
        private static OwnerOrator instance;

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
        private void setShipType(int typeValue, NetworkConnection connection = null)
        {
            //var player = LobbyService.GetMemberByConnection(connection);
            //player.OnSetShipType(typeValue);
            var profile = LobbyService2.GetProfileByConnection(connection);
            profile.OnSetShipType(typeValue);
        }

        [ServerRpc (RequireOwnership = false)]
        private void cycleShipSelectionLeft(NetworkConnection connection = null)
        {
            //var player = LobbyService.GetMemberByConnection(connection);

            //var current = player.Entity.Prep.SelectedShip;

            //if (--current < 0)
            //    current = 2;

            //player.OnSetShipType(current);

            var profile = LobbyService2.GetProfileByConnection(connection);
            var current = profile.Entity.Prep.SelectedShip;

            if (--current < 0)
                current = 2;

            profile.OnSetShipType(current);
        }

        [ServerRpc(RequireOwnership = false)]
        private void cycleShipSelectionRight(NetworkConnection connection = null)
        {
            //var player = LobbyService.GetMemberByConnection(connection);

            //var current = player.Entity.Prep.SelectedShip;

            //if (++current > 2)
            //    current = 0;

            //player.OnSetShipType(current);

            var profile = LobbyService2.GetProfileByConnection(connection);
            var current = profile.Entity.Prep.SelectedShip;

            if (++current > 2)
                current = 0;

            profile.OnSetShipType(current);
        }

        [ServerRpc(RequireOwnership = false)]
        private void toggleReadyState(NetworkConnection connection = null)
        {
            //var player = LobbyService.GetMemberByConnection(connection);

            //var readyState = !player.Entity.Prep.IsReady;

            //player.OnReadyChange(readyState);

            ////send that a ready state has changed to state control
            //GameplayControl.LobbyPhase.Content.OnReadyStateChange();

            var profile = LobbyService2.GetProfileByConnection(connection);
            var readyState = !profile.Entity.Prep.IsReady;

            profile.OnReadyChange(readyState);

            //send that a ready state has changed to state control
            GameplayControl.LobbyContent.OnReadyStateChange();
            //GameplayControl.LobbyPhase.Content.OnReadyStateChange();
        }

        [ServerRpc(RequireOwnership = false)]
        private void requestResync(NetworkConnection connection = null)
        {
            //var player = LobbyService.GetMemberByConnection(connection);
            //player.OnSyncObserver(connection, player.Entity);
        }
        #endregion

        #region Static
        public static void SetShipType(int typeValue)
        {
            instance.setShipType(typeValue);
        }

        public static void CycleShipSelectionLeft()
        {
            instance.cycleShipSelectionLeft();
        }

        public static void CycleShipSelectionRight()
        {
            instance.cycleShipSelectionRight();
        }

        public static void ToggleReady()
        {
            instance.toggleReadyState();
        }

        public static void RequestResync()
        {
            instance.requestResync();
        }
        #endregion
    }
}
