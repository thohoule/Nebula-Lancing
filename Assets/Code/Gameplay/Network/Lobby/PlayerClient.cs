using System;
using UnityEngine;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using FishNet.Connection;
using Assets.Code.Gameplay.GameSM;
using Assets.Code.Characters;
using FishNet.Object.Synchronizing.Internal;

namespace Assets.Code.Gameplay
{
    //public delegate void OnPrepPropertyChange(int number, string text);

    //public class PlayerClient : NetPrefabAsset
    //{
    //    [SerializeField, SyncVar]
    //    private string profileName;
    //    [SerializeField, SyncVar]//SyncObject(RequireReadOnly =false)]
    //    private ShipPrep prep;
    //    [SerializeField, SyncVar]
    //    private int tempShipType;

    //    public override bool NameSearchEnabled => false;
    //    public override bool TypeSearchEnabled => true;

    //    public string ProfileName { get => profileName; set => profileName = value; }
    //    public ShipPrep Prep { get => prep; }
    //    public PlayerPrepLane AssingedLane { get; private set; }
    //    public bool IsAssignedLane { get => AssingedLane != null; }

    //    //public event OnPrepPropertyChange OnPrepChange;
    //    public event OnPrepPropertyChange OnReadyChange;
    //    public event OnPrepPropertyChange OnShipTypeChange;
    //    public event OnPrepPropertyChange OnPrimaryWeaponChange;
    //    public event OnPrepPropertyChange OnSecondaryWeaponChange;

    //    public void SetupPrep(PlayerPrepLane assignedLane)//server side
    //    {
    //        AssingedLane = assignedLane;

    //        setupLane(assignedLane.Number);
    //    }

    //    [ServerRpc]
    //    public void SetReadyState(bool state, NetworkConnection connection = null)
    //    {
    //        if (state != prep.IsReady)
    //        {
    //            prep.IsReady = state;
    //            readyChanged(state ? 0 : 1);
    //        }
    //    }

    //    [ServerRpc]
    //    public void SetShipType(int typeValue, NetworkConnection connection = null)
    //    {
    //        if (prep.SelectedShip != typeValue)
    //        {
    //            prep.SelectedShip = typeValue;
    //            tempShipType = typeValue;
    //            shipTypeChanged(typeValue);
    //        }
    //    }

    //    [ServerRpc]
    //    public void SetPrimaryWeapon(string weaponAsset, NetworkConnection connection = null)
    //    {
    //        if (prep.GetCurrent().PrimaryWeapon != weaponAsset)
    //        {
    //            prep.SetPrimary(weaponAsset);
    //            primaryWeaponChanged(weaponAsset);
    //        }
    //    }

    //    [ServerRpc]
    //    public void SetSecondaryWeapon(string weaponAsset, NetworkConnection connection = null)
    //    {
    //        if (prep.GetCurrent().SecondaryWeapon != weaponAsset)
    //        {
    //            prep.SetSecondary(weaponAsset);
    //            secondaryWeaponChanged(weaponAsset);
    //        }
    //    }

    //    [ObserversRpc]
    //    private void setupLane(int lane)
    //    {
    //        switch (lane)
    //        {
    //            case 1:
    //                GameLoopControl.PreparingUI.Lane1.AssignOwner(this, IsOwner);
    //                break;
    //            case 2:
    //                GameLoopControl.PreparingUI.Lane2.AssignOwner(this, IsOwner);
    //                break;
    //            case 3:
    //                GameLoopControl.PreparingUI.Lane3.AssignOwner(this, IsOwner);
    //                break;
    //            default:
    //                GameLoopControl.PreparingUI.Lane4.AssignOwner(this, IsOwner);
    //                break;
    //        }
    //    }

    //    //private void onPrepChange(ShipPrep prev, ShipPrep next, bool asServer)
    //    //{
    //    //    OnPrepChange?.Invoke();
    //    //}

    //    [ObserversRpc]
    //    private void readyChanged(int state)
    //    {
    //        OnReadyChange?.Invoke(state, null);
    //    }

    //    [ObserversRpc]
    //    private void shipTypeChanged(int type)
    //    {
    //        //OnPrepChange?.Invoke();
    //        OnShipTypeChange?.Invoke(type, null);
    //    }

    //    [ObserversRpc]
    //    private void primaryWeaponChanged(string text)
    //    {
    //        OnPrimaryWeaponChange?.Invoke(0, text);
    //    }

    //    [ObserversRpc]
    //    private void secondaryWeaponChanged(string text)
    //    {
    //        OnSecondaryWeaponChange?.Invoke(0, text);
    //    }
    //}

    [Serializable]
    public class ShipPrep// : SyncBase, ICustomSync
    {
        public const string Small_Settings = "SmallShipSettings";
        public const string Medium_Settings = "MediumShipSettings";
        public const string Large_Settings = "LargeShipSettings";

        public bool IsReady;
        public int SelectedShip;
        public ShipInfo SmallShip;
        public ShipInfo MediumShip;
        public ShipInfo LargeShip;

        public void SetPrimary(string weaponAsset)
        {
            GetCurrent().PrimaryWeapon = weaponAsset;
        }

        public void SetSecondary(string weaponAsset)
        {
            GetCurrent().SecondaryWeapon = weaponAsset;
        }

        public ShipInfo GetCurrent()
        {
            switch (SelectedShip)
            {
                case 0:
                    return SmallShip;
                case 1:
                    return MediumShip;
                default:
                    return LargeShip;
            }
        }

        public Vector3 GetPrimaryAttachPoint()
        {
            var settings = getShipSettings();

            return settings.PrimaryAttachPoint; //+ offset
        }

        public Vector3 GetSecondaryPoint()
        {
            var settings = getShipSettings();

            return settings.SecondaryAttachPoint;
        }

        public string GetPrimaryWeapon()
        {
            var ship = GetCurrent();
            return ship.PrimaryWeapon;
        }

        public string GetSecondaryWeapon()
        {
            var ship = GetCurrent();
            return ship.SecondaryWeapon;
        }

        private ShipSettings getShipSettings()
        {
            switch (SelectedShip)
            {
                case 0:
                    return PrefabAsset.GetPrefab<ShipSettings>(Small_Settings);
                case 1:
                    return PrefabAsset.GetPrefab<ShipSettings>(Medium_Settings);
                default:
                    return PrefabAsset.GetPrefab<ShipSettings>(Large_Settings);
            }
        }

        public object GetSerializedType()
        {
            return null;
        }

        //[SerializeField, SyncVar]
        //private bool isReady;
        //[SerializeField, SyncVar(OnChange = nameof(onShipTypeChange))]
        //private int selectedShip;
        //[SerializeField, SyncVar]
        //private ShipInfo smallShip;
        //[SerializeField, SyncVar]
        //private ShipInfo mediumShip;
        //[SerializeField, SyncVar]
        //private ShipInfo largeShip;

        //public bool IsReady { get => isReady; set => isReady = value; }
        //public int SelectedShip { get => selectedShip; set => selectedShip = value; }
        //public ShipInfo SmallShip { get => smallShip; set => smallShip = value; }
        //public ShipInfo MediumShip { get => mediumShip; set => mediumShip = value; }
        //public ShipInfo LargeShip { get => largeShip; set => largeShip = value; }

        //public event OnPrepPropertyChange OnShipChange;
        //public event OnPrepPropertyChange OnReadyChange;

        //private void onShipTypeChange(int prev, int next, bool asServer)
        //{
        //    OnShipChange?.Invoke();
        //}

        //private void onReadyStateChange(bool prev, bool next, bool asServer)
        //{
        //    OnReadyChange?.Invoke();
        //}
    }

    [Serializable]
    public class ShipInfo// : ICustomSync
    {
        public string PrimaryWeapon;
        public string SecondaryWeapon;

        public object GetSerializedType()
        {
            return null;
        }

        //[SerializeField, SyncVar(OnChange = nameof(onWeaponChange))]
        //private string primaryWeapon;
        //[SerializeField, SyncVar(OnChange = nameof(onWeaponChange))]
        //private string secondaryWeapon;

        //public string PrimaryWeapon { get => primaryWeapon; set => primaryWeapon = value; }
        //public string SecondaryWeapon { get => secondaryWeapon; set => secondaryWeapon = value; }

        //public event OnPrepPropertyChange OnWeaponChange;

        //private void onWeaponChange(string prev, string next, bool asServer)
        //{
        //    OnWeaponChange?.Invoke();
        //}
    }
}
