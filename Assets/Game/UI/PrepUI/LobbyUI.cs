using TMPro;
using UnityEngine;

namespace Assets.Game.UI
{
    public class LobbyUI : LobbyUIBase
    {
        [SerializeField]
        private GameObject countdownObject;
        [SerializeField]
        private TextMeshProUGUI countdownText;

        protected override void onCountdownStart(float value)
        {
            countdownObject.SetActive(true);
            onCountdownTick(value);
        }

        protected override void onCountdownStop()
        {
            countdownObject.SetActive(false);
        }

        protected override void onCountdownTick(float value)
        {
            countdownText.text = value.ToString();
        }

        protected override void onElapsed()
        {
            onCountdownStop();
        }
    }
}
