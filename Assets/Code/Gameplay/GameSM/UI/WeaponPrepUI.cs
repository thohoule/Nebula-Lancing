using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Assets.Code.Gameplay.GameSM.UI
{
    public class WeaponPrepUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI nameText;
        [SerializeField]
        private TextMeshProUGUI damageText;
        [SerializeField]
        private TextMeshProUGUI speedText;
        [SerializeField]
        private Image pictureImage;
        [SerializeField]
        private TextMeshProUGUI upgradeText;
        [SerializeField]
        private TextMeshProUGUI costText;
        [SerializeField]
        private TextMeshProUGUI mockText;
        [SerializeField]
        private Button changeButton;

        public TextMeshProUGUI NameText { get => nameText; set => nameText = value; }
        public TextMeshProUGUI DamageText { get => damageText; set => damageText = value; }
        public TextMeshProUGUI SpeedText { get => speedText; set => speedText = value; }
        public Image PictureImage { get => pictureImage; set => pictureImage = value; }
        public TextMeshProUGUI UpgradeText { get => upgradeText; set => upgradeText = value; }
        public TextMeshProUGUI CostText { get => costText; set => costText = value; }
        public TextMeshProUGUI MockText { get => mockText; set => mockText = value; }

        public void EnableChangeButton()
        {
            changeButton.enabled = true;
        }

        public void DisableChangeButton()
        {
            changeButton.enabled = false;
        }
    }
}
