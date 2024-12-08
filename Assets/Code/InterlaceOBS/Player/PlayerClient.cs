using UnityEngine;
using TeaSteep;
using FishNet.Object;
using FishNet.Connection;

namespace Assets.Code.Gameplay
{
    public class PlayerClient : NetPrefabAsset
    {
        [SerializeField]
        private ShipPrep prep;

        public override bool NameSearchEnabled => false;
        public override bool TypeSearchEnabled => true;

        public string ProfileName { get; private set; }
        public ShipPrep Prep { get => prep; internal set => prep = value; }
        public int SeatNumber { get; internal set; }

        //#region Internals
        //[ObserversRpc]
        //internal void SetShipType(int shipType)
        //{
        //    Prep.SelectedShip = shipType;
        //    handler.OnPrepChange(Prep);
        //}

        //[ObserversRpc]
        //internal void SetPrimaryWeapon(string weaponName)
        //{
        //    Prep.SetPrimary(weaponName);
        //    handler.OnPrepChange(Prep);
        //}

        //[ObserversRpc]
        //internal void SetSecondaryWeapon(string weaponName)
        //{
        //    Prep.SetSecondary(weaponName);
        //    handler.OnPrepChange(Prep);
        //}

        //[ObserversRpc]
        //internal void AssignSeat(int seat)
        //{
        //    SeatNumber = seat;
        //    handler.OnSeatAssign(this);
        //}

        //[TargetRpc]
        //internal void SetAsOwner(NetworkConnection connection)
        //{
        //    handler.OnOwnershipAssign(this);
        //}
        //#endregion
    }
}
