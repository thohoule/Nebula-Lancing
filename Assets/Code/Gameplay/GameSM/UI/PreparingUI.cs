using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Assets.Code.Gameplay.GameSM
{
    public class PreparingUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject countdownObject;
        [SerializeField]
        private TextMeshProUGUI countdownText;
        [SerializeField]
        private PlayerPrepLane lane1;
        [SerializeField]
        private PlayerPrepLane lane2;
        [SerializeField]
        private PlayerPrepLane lane3;
        [SerializeField]
        private PlayerPrepLane lane4;

        public PlayerPrepLane Lane1 { get => lane1; }
        public PlayerPrepLane Lane2 { get => lane2; }
        public PlayerPrepLane Lane3 { get => lane3; }
        public PlayerPrepLane Lane4 { get => lane4; }

        public void Show()
        {
            //if (Lane1.IsAssinged)
            //    Lane1.LaneUI.Show();
            //if (Lane2.IsAssinged)
            //    Lane2.LaneUI.Show();
            //if (Lane3.IsAssinged)
            //    Lane3.LaneUI.Show();
            //if (Lane4.IsAssinged)
            //    Lane4.LaneUI.Show();
        }

        public void Hide()
        {
            Lane1.LaneUI.Hide();
            Lane2.LaneUI.Hide();
            Lane3.LaneUI.Hide();
            Lane4.LaneUI.Hide();
            HideCountdown();
        }

        public void ShowCoundown()
        {
            countdownObject.SetActive(true);
        }

        public void HideCountdown()
        {
            countdownObject.SetActive(false);
        }

        public void SetReadyTime(float remainingTime)
        {
            countdownText.text = remainingTime.ToString();
        }

        //public void StartLaneGameplay()
        //{
        //    Lane1.StartGameplay();
        //    Lane2.StartGameplay();
        //    Lane3.StartGameplay();
        //    Lane4.StartGameplay();
        //}
    }
}
