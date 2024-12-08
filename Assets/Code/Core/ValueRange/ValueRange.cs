using System;
using UnityEngine;

namespace Assets.Code
{
    [Serializable]
    public struct ValueRange
    {
        [SerializeField]
        private float value;
        [SerializeField]
        private float min;
        [SerializeField]
        private float max;

        public float Value { get => value; set => this.value = Mathf.Clamp(value, min, max); }
        public float Min { get => min; set => min = value; }
        public float Max { get => max; set => max = value; }

        public ValueRange(float min, float max, float value)
        {
            if (min > max)
                min = max;

            this.min = min;
            this.max = max;
            this.value = Mathf.Clamp(value, min, max);
        }

        public ValueRange(ValueRange range, float value)
        {
            min = range.Min;
            max = range.Max;
            this.value = Mathf.Clamp(value, min, max);
        }
    }
}
