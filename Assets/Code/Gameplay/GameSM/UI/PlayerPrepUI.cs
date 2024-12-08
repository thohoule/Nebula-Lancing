using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Assets.Code.Gameplay.GameSM.UI
{
    public class PlayerPrepUI : MonoBehaviour
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

        public TextMeshProUGUI PlayerText { get => playerText; }
        public ShipPrepUI ShipSelectUI { get => shipUI; }
        public ReadyUpUI ReadyUI { get => readyUI; }
        public WeaponPrepUI PrimaryWeapon { get => primaryWeapon; set => primaryWeapon = value; }
        public WeaponPrepUI SecondaryWeapon { get => secondaryWeapon; set => secondaryWeapon = value; }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void ShowChangeWeaponUI()
        {
            changeWeaponUIObject.SetActive(true);
        }

        public void HideChangeWeaponUI()
        {
            changeWeaponUIObject.SetActive(false);
        }
    }
}
