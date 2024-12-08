using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Assets.Code.Gameplay.GameSM.UI
{
    public class ShipPrepUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI shipNameText;
        [SerializeField]
        private GameObject shipSelectPanelObject;

        public TextMeshProUGUI ShipNameText { get => shipNameText; set => shipNameText = value; }

        public void ShowShipSelect()
        {
            shipSelectPanelObject.SetActive(true);
        }

        public void HideShipSelect()
        {
            shipSelectPanelObject.SetActive(false);
        }
    }
}
