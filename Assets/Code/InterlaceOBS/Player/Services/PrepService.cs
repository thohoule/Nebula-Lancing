using UnityEngine;
using Assets.Code.Gameplay.GameSM;

namespace Assets.Code.Gameplay
{
    public class PrepService : MonoBehaviour
    {
        [SerializeField]
        private PreparingUI prepUI;
        [SerializeField]
        private PlayerPrepLane lane1;
        [SerializeField]
        private PlayerPrepLane lane2;
        [SerializeField]
        private PlayerPrepLane lane3;
        [SerializeField]
        private PlayerPrepLane lane4;

        private static PrepService instance;

        private void Awake()
        {
            instance = this;
        }

        public static void ShowUI()
        {
            instance.prepUI.Show();
        }

        public static void HideUI()
        {
            instance.prepUI.Hide();
        }

        public static void SetShipType(int typeValue)
        {
            PlayerService.OwnerHandler.SetShipType(typeValue);
        }

        public static void SetPrimaryWeapon(string weaponAsset)
        {
            PlayerService.OwnerHandler.SetPrimaryWeapon(weaponAsset);
        }

        public static void SetSecondaryWeapon(string weaponAsset)
        {
            PlayerService.OwnerHandler.SetSecondaryWeapon(weaponAsset);
        }

        public static void ToggleReady()
        {
            //PlayerService.Owner.ToggleReadyState();
            PlayerService.OwnerHandler.ToggleReadyState();
        }

        public static PlayerPrepLane GetPrepLane(int laneNumber)
        {
            switch (laneNumber)
            {
                case 1:
                    return instance.lane1;
                case 2:
                    return instance.lane2;
                case 3:
                    return instance.lane3;
                default:
                    return instance.lane4;
            }
        }
    }
}
