using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Gameplay.PlayingSM.UI
{
    public delegate void TimerEventHandler();

    public class CooldownSlider : MonoBehaviour
    {
        public const float Max_Cooldown = 999;

        private TimerEventHandler expireHandle;

        //[SerializeField]
        //private GameObject cooldownObject;
        [SerializeField]
        private Slider slider;

        public bool IsPaused { get; set; }
        public float Current { get; private set; }
        public float TotalSeconds { get; private set; }
        public float RatePerSecond { get; private set; }

        public void StartCooldown(float seconds)
        {
            StartCooldown(seconds, null);
        }

        public void StartCooldown(float seconds, TimerEventHandler expireHandler)
        {
            StartCooldown(seconds, 1, expireHandler);
        }

        public void StartCooldown(float seconds, float ratePerSecond,
            TimerEventHandler expireHandler)
        {
            Current = 0;
            TotalSeconds = Mathf.Clamp(seconds, 0.0001f, Max_Cooldown);
            RatePerSecond = Mathf.Clamp(ratePerSecond, 0.0001f, Max_Cooldown);

            expireHandle = expireHandler;
            gameObject.SetActive(true);
        }

        public void Cancel()
        {
            end();
        }

        private void Update()
        {
            if (IsPaused)
                return;

            Current += Time.deltaTime * RatePerSecond;
            var percent = Current / TotalSeconds;
            slider.value = percent;

            if (Current >= TotalSeconds)
                fireExpire();
        }

        private void fireExpire()
        {
            expireHandle?.Invoke();
            end();
        }

        private void end()
        {
            gameObject.SetActive(false);
        }
    }
}
