using UnityEngine;
using Assets.Code.Gameplay;
using FishNet.Object;
using FishNet.Connection;
using Assets.Code.Gameplay.GameSM;

namespace Assets.Code.Gameplay
{
    public class PlayerClientHandler : NetworkBehaviour
    {
        protected PlayerClient player;

        [SerializeField]
        private int seatNumber;
        [SerializeField]
        private PlayerPrepLane prepLane;
        [SerializeField]
        private AvatarSpawn spawn;

        internal PlayerPrepLane PrepLane { get => prepLane; }
        internal AvatarSpawn SpawnPoint { get => spawn; }
        public PlayerClient Player { get => player; }
        public bool IsAssigned { get => player != null; }
        public int Seat { get => seatNumber; }

        #region Downstream
        [ObserversRpc]
        internal void OnSetShipType(int shipType)
        {
            player.Prep.SelectedShip = shipType;
            //set model and UI
            PrepLane.SetMockShip(player.Prep);
            prepLane.SetShipTypeName(prepLane.GetShipNameByType(player.Prep));
            //var lane = PrepService.GetPrepLane(player.SeatNumber);
            //lane.SetMockShip(player.Prep);
            //lane.SetShipTypeName(lane.GetShipNameByType(player.Prep));
        }

        [ObserversRpc]
        internal void OnSetPrimaryWeapon(string weaponName)
        {
            player.Prep.SetPrimary(weaponName);
            //set model and UI
            prepLane.SetMockPrimary(player.Prep);
            //var lane = PrepService.GetPrepLane(player.SeatNumber);
            //lane.SetMockPrimary(player.Prep);
        }

        [ObserversRpc]
        internal void OnSetSecondaryWeapon(string weaponName)
        {
            player.Prep.SetSecondary(weaponName);
            //set model and UI
            prepLane.SetMockSecondary(player.Prep);
            //var lane = PrepService.GetPrepLane(player.SeatNumber);
            //lane.SetMockSecondary(player.Prep);
        }

        //[ObserversRpc]
        //internal void OnAssignSeat(int seat)
        //{
        //    player.SeatNumber = seat;
        //    //PlayerService.SetPlayer(player);
        //}

        [ObserversRpc]
        internal void OnReadyChange(bool readyState)
        {
            player.Prep.IsReady = readyState;
            //changeUI
            prepLane.SetReadyState(readyState);
            //var lane = PrepService.GetPrepLane(player.SeatNumber);
            //lane.SetReadyState(readyState);
        }

        //[ObserversRpc]
        //internal void OnTransition() //instead use Gameplay
        //{
        //    prepLane.LaneUI.Hide();
        //}

        [ObserversRpc]
        [TargetRpc]
        internal void OnAssginPlayer(NetworkConnection connection, 
            PlayerClient player)
        {
            this.player = player;
            prepLane.SetMockShip(player.Prep);
            prepLane.EnterSetup(player.ProfileName);
            prepLane.EnableSelection();
        }

        [TargetRpc]
        internal void OnSyncPrep(NetworkConnection connection, ShipPrep prep)
        {
            player.Prep = prep;
            prepLane.SetMockShip(prep);
            prepLane.SetReadyState(prep.IsReady);
        }

        [TargetRpc]
        internal void OnAssignOwner(NetworkConnection connection)
        {
            //PlayerService.Owner.SetHandler(this);
            PlayerService.SetOwnerHandler(this);
        }
        #endregion

        #region Upstream
        [ServerRpc(RequireOwnership = false)]
        public void SetShipType(int typeValue,
            NetworkConnection connection = null)
        {
            OnSetShipType(typeValue);
        }

        [ServerRpc(RequireOwnership = false)]
        public void SetPrimaryWeapon(string weaponAsset,
            NetworkConnection connection = null)
        {
            OnSetPrimaryWeapon(weaponAsset);
        }

        [ServerRpc(RequireOwnership = false)]
        public void SetSecondaryWeapon(string weaponAsset,
            NetworkConnection connection = null)
        {
            OnSetSecondaryWeapon(weaponAsset);
        }

        [ServerRpc (RequireOwnership = false)]
        public void ToggleReadyState(NetworkConnection connection = null)
        {
            OnReadyChange(!Player.Prep.IsReady);
        }
        #endregion
    }
}
