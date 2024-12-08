using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Assets.Code.Gameplay.PlayingSM.UI
{
    [Serializable]
    public class SliderControl
    {
        private const string readout_Format = "{0}/{1}";

        private int value;
        private int maxValue = 1;
        //private float

        [SerializeField]
        private Slider slider;
        [SerializeField]
        private TextMeshProUGUI text;

        public int Value { get => value; set => setValue(value); }
        public int MaxValue { get => maxValue; set => setMaxValue(value); }

        public void Setup(int maxValue)
        {
            Setup(maxValue, maxValue);
        }

        public void Setup(int value, int maxValue)
        {
            this.value = Math.Min(value, maxValue);
            this.maxValue = Math.Max(maxValue, 1);
        }

        private void setValue(int value)
        {
            value = Math.Max(value, 0);
            this.value = Math.Min(value, maxValue);
            refresh();
        }

        private void setMaxValue(int maxValue)
        {
            this.maxValue = Math.Max(maxValue, 1);
            refresh();
        }

        private void refresh()
        {
            var slideValue = Mathf.Clamp01((float)value / maxValue);
            slider.value = slideValue;

            var readout = string.Format(readout_Format, value, maxValue);
            text.text = readout;
        }
    }
}
