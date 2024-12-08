using System;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.Characters.MockAssets;
using Assets.Code.Gameplay.GameSM.UI;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using Interlace;

namespace Assets.Code.Gameplay.GameSM
{
    public class PlayerPrepLane : NetworkBehaviour
    {
        public const string Small_Ship_Text = "Small Ship";
        public const string Medium_Ship_Text = "Medium Ship";
        public const string Large_Ship_Text = "Large Ship";

        //private PlayerClient assignedClient;
        private bool canAssign;
        private int weaponBeingChanged;

        //private FakeShip smallMockShip;
        //private FakeShip mediumMockShip;
        //private FakeShip largeMockShip;

        [SerializeField]
        private int laneNumber;
        [SerializeField]
        private PlayerPrepUI prepUI;
        [SerializeField]
        private MockPrepShip mockShip;
        //[SerializeField, SyncVar(OnChange = nameof(onShipChange))]
        //private int selectedShip;
        //[SerializeField, SyncVar]
        //private string smallPrimaryWeapon;
        //[SerializeField, SyncVar]
        //private string smallSecondaryWeapon;
        //[SerializeField, SyncVar]
        //private string mediumPrimaryWeapon;
        //[SerializeField, SyncVar]
        //private string mediumSecondaryWeapon;
        //[SerializeField, SyncVar]
        //private string largePrimaryWeapon;
        //[SerializeField, SyncVar]
        //private string largeSecondaryWeapon;

        public int Number { get => laneNumber; }
        public PlayerPrepUI LaneUI { get => prepUI; }
        //public bool IsAssinged { get => assignedClient != null; }

        //private void Awake()
        //{
        //    smallMockShip = PrefabAsset.GetPrefab<FakeShip>("SmallFakeShip");
        //    mediumMockShip = PrefabAsset.GetPrefab<FakeShip>("MediumFakeShip");
        //    largeMockShip = PrefabAsset.GetPrefab<FakeShip>("LargeFakeShip");
        //}

        //public void AssignOwner(PlayerClient assigned, bool isOwner)
        //{
        //    assignedClient = assigned;
        //    canAssign = isOwner;

        //    prepUI.PlayerText.text = assigned.ProfileName;
        //    prepUI.Show();
        //    mockShip.SetSmallShip();
        //    mockShip.PlayEnterAnimation();

        //    if (canAssign)
        //    {
        //        prepUI.PrimaryWeapon.EnableChangeButton();
        //        prepUI.SecondaryWeapon.EnableChangeButton();
        //        prepUI.ShipSelectUI.ShowShipSelect();
        //    }
        //    else
        //    {
        //        prepUI.PrimaryWeapon.DisableChangeButton();
        //        prepUI.SecondaryWeapon.DisableChangeButton();
        //        prepUI.ShipSelectUI.HideShipSelect();
        //    }

        //    //assignedClient.OnPrepChange += onPrepChange;
        //    //assignedClient.OnReadyChange += onReadyChanged;
        //    //assignedClient.OnShipTypeChange += onShipTypeChange;
        //    //assignedClient.OnPrimaryWeaponChange += onPrimayChanged;
        //    //assignedClient.OnSecondaryWeaponChange += onSecondaryChanged;
        //}

        public void EnterSetup(string profileName)
        {
            prepUI.PlayerText.text = profileName;
            prepUI.Show();
            mockShip.PlayEnterAnimation();
        }

        public void ExitSetup()
        {

        }

        public void EnableSelection()
        {
            LaneUI.ShipSelectUI.ShowShipSelect();
            canAssign = true;
        }

        public void TransitionSetup()
        {
            mockShip.PlayTransitionAnimation();
            prepUI.Hide();
        }

        public void SetMockShip(ShipPrep prep)
        {
            mockShip.SetShipByPrep(prep);
        }

        public void SetMockPrimary(ShipPrep prep)
        {
            mockShip.LoadPrimaryWeapon(prep.GetPrimaryWeapon(),
                prep.GetPrimaryAttachPoint());
        }

        public void SetMockPrimary(string weaponAsset, Vector3 attachPoint)
        {
            mockShip.LoadPrimaryWeapon(weaponAsset, attachPoint);
        }

        public void SetMockSecondary(ShipPrep prep)
        {
            SetMockSecondary(prep.GetSecondaryWeapon(), prep.GetSecondaryPoint());
        }

        public void SetMockSecondary(string weaponAsset, Vector3 attachPoint)
        {
            mockShip.LoadPrimaryWeapon(weaponAsset, attachPoint);
        }

        public void SetShipTypeName(string shipName)
        {
            prepUI.ShipSelectUI.ShipNameText.text = shipName;
        }

        public void SetReadyState(bool readyState)
        {
            prepUI.ReadyUI.NotReadyObject.SetActive(!readyState);
            prepUI.ReadyUI.ReadyObject.SetActive(readyState);
        }

        public string GetShipNameByType(ShipPrep prep)
        {
            return GetShipNameByType(prep.SelectedShip);
        }

        public string GetShipNameByType(int typeValue)
        {
            switch (typeValue)
            {
                case 0:
                    return Small_Ship_Text;
                case 1:
                    return Medium_Ship_Text;
                default:
                    return Large_Ship_Text;
            }
        }

        //public void Unassign()
        //{

        //}

        //public void StartGameplay()
        //{
        //    if (IsAssinged)
        //    {
        //        mockShip.PlayTransitionAnimation();
        //    }
        //}

        //private void onReadyChanged(int number, string text)
        //{
        //    var value = number == 0 ? false : true;

        //    prepUI.ReadyUI.NotReadyObject.SetActive(!value);
        //    prepUI.ReadyUI.ReadyObject.SetActive(value);
        //}

        //private void onShipTypeChange(int number, string text)
        //{
        //    var shipPrep = new ShipPrep()
        //    {
        //        SelectedShip = number,
        //        SmallShip = assignedClient.Prep.SmallShip,
        //        MediumShip = assignedClient.Prep.MediumShip,
        //        LargeShip = assignedClient.Prep.LargeShip,
        //    };
        //    mockShip.SetShipByPrep(shipPrep);
        //    prepUI.ShipSelectUI.ShipNameText.text = getShipTypeText();
        //}

        //private void onPrimayChanged(int number, string text)
        //{
        //    mockShip.LoadPrimaryByPrep(assignedClient.Prep);
        //}

        //private void onSecondaryChanged(int number, string text)
        //{
        //    mockShip.LoadSecondaryByPrep(assignedClient.Prep);
        //}

        //private void onPrepChange()
        //{
        //    var value = assignedClient.Prep.IsReady;

        //    prepUI.ReadyUI.NotReadyObject.SetActive(!value);
        //    prepUI.ReadyUI.ReadyObject.SetActive(value);

        //    //reload ship
        //    int selectedShip = assignedClient.Prep.SelectedShip;
        //    switch (selectedShip)
        //    {
        //        case 0:
        //            mockShip.SetSmallShip(assignedClient.Prep.SmallShip);
        //            break;
        //        case 1:
        //            mockShip.SetMediumShip(assignedClient.Prep.MediumShip);
        //            break;
        //        default:
        //            mockShip.SetLargeShip(assignedClient.Prep.LargeShip);
        //            break;
        //    }
        //}

        //private string getShipTypeText()
        //{
        //    switch (assignedClient.Prep.SelectedShip)
        //    {
        //        case 0:
        //            return Small_Ship_Text;
        //        case 1:
        //            return Medium_Ship_Text;
        //        default:
        //            return Large_Ship_Text;
        //    }
        //}

        //public void PlayerEntered(bool isOwner)
        //{
        //    canAssign = isOwner;

        //    prepUI.Show();
        //    selectedShip = 0;

        //    if (canAssign)
        //    {
        //        prepUI.PrimaryWeapon.EnableChangeButton();
        //        prepUI.SecondaryWeapon.EnableChangeButton();
        //        prepUI.ShipSelectUI.ShowShipSelect();
        //    }
        //    else
        //    {
        //        prepUI.PrimaryWeapon.DisableChangeButton();
        //        prepUI.SecondaryWeapon.DisableChangeButton();
        //        prepUI.ShipSelectUI.HideShipSelect();
        //    }
        //}

        //public void PlayerExit()
        //{

        //}

        #region Set Ship Size
        public void CycleShipSelectLeft()
        {
            if (!canAssign)
                return;

            var current = PlayerService.OwnerHandler.Player.Prep.SelectedShip;

            if (--current < 0)
                current = 2;

            PrepService.SetShipType(current);
            //assignedClient.SetShipType(current);
            //assignedClient.Prep.SelectedShip = current;
        }

        public void CycleShipSelectRight()
        {
            if (!canAssign)
                return;

            var current = PlayerService.OwnerHandler.Player.Prep.SelectedShip;
            //assignedClient.Prep.SelectedShip;

            if (++current > 2)
                current = 0;

            PrepService.SetShipType(current);
            //assignedClient.SetShipType(current);
            //assignedClient.Prep.SelectedShip = current;
        }

        //private void onShipChange(int prev, int next, bool asServer)
        //{
        //    toggleShip(prev);
        //    toggleShip(next);
        //}

        //private void toggleShip(int shipType)
        //{
        //    var ship = shipType == 0 ? smallMockShip : shipType == 1 ?
        //        mediumMockShip : largeMockShip;
        //    var currentState = ship.gameObject.activeInHierarchy;

        //    ship.gameObject.SetActive(!currentState);
        //}
        #endregion

        #region Change Ship Weapon
        public void ChangePrimaryWeapon()
        {
            if (!canAssign)
                return;

            //open Change UI
            weaponBeingChanged = 1;
            prepUI.ShowChangeWeaponUI();
        }

        public void ChangeSecondaryWeapon()
        {
            if (!canAssign)
                return;

            //open Change UI
            weaponBeingChanged = 2;
            prepUI.ShowChangeWeaponUI();
        }

        public void SetWeaponAsset(string weaponAsset)
        {
            if (!canAssign)
                return;

            if (weaponBeingChanged == 1)
                PrepService.SetPrimaryWeapon(weaponAsset);
            //assignedClient.SetPrimaryWeapon(weaponAsset);
            else if (weaponBeingChanged == 2)
                PrepService.SetSecondaryWeapon(weaponAsset);
                //assignedClient.SetSecondaryWeapon(weaponAsset);

            weaponBeingChanged = 0;
            prepUI.HideChangeWeaponUI();
        }
        #endregion

        #region Ready
        public void ToggleReadyClick()
        {
            if (!canAssign)
                return;

            PrepService.ToggleReady();
        }

        public void IsReadyClick()
        {
            //if (!canAssign)
            //    return;

            //assignedClient.SetReadyState(true);
            //assignedClient.Prep.IsReady = true;

            //Set Ready state
            //prepUI.ReadyUI.NotReadyObject.SetActive(false);
            //prepUI.ReadyUI.ReadyObject.SetActive(true);
        }

        public void IsNotReadyClick()
        {
            //if (!canAssign)
            //    return;

            //assignedClient.SetReadyState(false);
            //assignedClient.Prep.IsReady = false;

            //prepUI.ReadyUI.NotReadyObject.SetActive(true);
            //prepUI.ReadyUI.ReadyObject.SetActive(false);
        }

        //private void onReadyStateChange()
        //{
        //    var value = assignedClient.Prep.IsReady;

        //    prepUI.ReadyUI.NotReadyObject.SetActive(!value);
        //    prepUI.ReadyUI.ReadyObject.SetActive(value);
        //}
        #endregion
    }
}
