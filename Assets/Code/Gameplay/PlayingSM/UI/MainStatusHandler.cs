using UnityEngine;
using Assets.Code.Characters;
using Assets.Code.Gameplay.PlayingSM.UI;

namespace Assets.Code.Gameplay.PlayingSM
{
    [RequireComponent(typeof(PlayerStatUI))]
    public class MainStatusHandler : MonoBehaviour, IAvatarUIHandler
    {
        private PlayerStatUI ui;

        private void Awake()
        {
            ui = GetComponent<PlayerStatUI>();
        }

        public void SetHealth(int value)
        {
            ui.HealthSlider.Value = value;
        }

        public void SetMaxHealth(int value)
        {
            ui.HealthSlider.MaxValue = value;
        }

        public void SetShield(int value)
        {
            ui.ShieldSlider.Value = value;
        }

        public void SetMaxShield(int value)
        {
            ui.ShieldSlider.MaxValue = value;
        }

        public void SetPrimaryCooldown(float seconds)
        {
            ui.PrimaryCooldown.StartCooldown(seconds);
        }

        public void SetSecondaryCooldown(float seconds)
        {
            ui.SecondaryCooldown.StartCooldown(seconds);
        }

        public void SetSheildCooldown(float seconds)
        {
            ui.ShieldCooldown.StartCooldown(seconds);
        }
    }
}
