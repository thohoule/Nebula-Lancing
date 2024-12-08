using Interlace;
using UnityEngine;

namespace Assets.Game
{
    public abstract class LobbyUIBase : MonoBehaviour
    {
        private bool isCountingDown;
        private float countdownValue;
        private float readyTimer;
        private bool isDone;

        //public virtual void SetReadyTimerText(string text)
        //{ }

        public void StartCountdown(float value)
        {
            isCountingDown = true;
            countdownValue = value;
            readyTimer = 0;
            isDone = false;
            onCountdownStart(countdownValue);
        }

        public void HideCountdown()
        {
            isCountingDown = false;
            isDone = false;
            onCountdownStop();
        }

        private void Update()
        {
            if (!isCountingDown || isDone)
                return;

            if (readyTimer >= countdownValue)
            {
                isDone = true;
                onElapsed();
            }
            else
            {
                onCountdownTick(countdownValue - readyTimer);
                readyTimer += Time.deltaTime;
            }
        }

        protected virtual void onCountdownStart(float value)
        { }

        protected virtual void onCountdownStop()
        { }

        protected virtual void onCountdownTick(float value)
        { }

        protected virtual void onElapsed()
        { }
    }
}
