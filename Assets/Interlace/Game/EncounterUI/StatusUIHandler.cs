using Assets.Code.Gameplay.PlayingSM.UI;
using UnityEngine;

namespace Assets.Game
{
    public class StatusUIHandler : MonoBehaviour
    {
        [SerializeField]
        private SliderControl healthSlider;
        [SerializeField]
        private SliderControl shieldSlider;

        public SliderControl HealthSlider { get => healthSlider; }
        public SliderControl ShieldSlider { get => shieldSlider; }

        public void SetShield(int value)
        {
            shieldSlider.Value = value;
        }

        public void SetMaxShield(int value)
        {
            shieldSlider.Setup(value);
        }

        public void SetHealth(int value)
        {
            healthSlider.Value = value;
        }

        public void SetMaxHealth(int value)
        {
            healthSlider.Setup(value);
        }
    }
}
