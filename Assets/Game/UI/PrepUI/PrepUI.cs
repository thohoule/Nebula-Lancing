using UnityEngine;
using Assets.Code.Gameplay.GameSM.UI;
using TMPro;
using Interlace;

namespace Assets.Game.UI.Prep
{
    public class PrepUI : PrepUIBase
    {
        [SerializeField]
        private TextMeshProUGUI playerText;
        [SerializeField]
        private ShipPrepUI shipUI;
        [SerializeField]
        private ReadyUpUI readyUI;
        [SerializeField]
        private WeaponPrepUI primaryWeapon;
        [SerializeField]
        private WeaponPrepUI secondaryWeapon;
        [SerializeField]
        private GameObject changeWeaponUIObject;

        public bool OwnershipEnabled { get; private set; }

        public override void SetSelectShipText(string text)
        {
            shipUI.ShipNameText.text = text;
        }

        public override void SetReadyState(bool readyState)
        {
            readyUI.SetReadyState(readyState);
        }

        public override void EnableOwnerEdit()
        {
            shipUI.ShowShipSelect();
            primaryWeapon.EnableChangeButton();
            secondaryWeapon.EnableChangeButton();
            OwnershipEnabled = true;
        }

        #region Button Handles
        public void CycleShipSelectLeftClick()
        {
            if (OwnershipEnabled)
                OwnerOrator.CycleShipSelectionLeft();
        }

        public void CycleShipSelectRightClick()
        {
            if (OwnershipEnabled)
                OwnerOrator.CycleShipSelectionRight();
        }

        public void ToggleReadyClick()
        {
            if (OwnershipEnabled)
                OwnerOrator.ToggleReady();
        }
        #endregion
    }
}
