using System;
using System.Collections.Generic;

namespace Assets.Code
{
    public class Threshold
    {
        protected Threshold child;
        protected Threshold leaf;
        protected int level;

        public float Origin { get; private set; }
        public float UpperBounds { get; private set; }
        public float LowerBounds { get; private set; }

        protected Threshold(float origin, float upperBounds, float lowerBounds, int level) :
            this(origin, upperBounds, lowerBounds)
        {
            this.level = level;
        }

        public Threshold(float origin, float uniformBounds)
        { }

        public Threshold(float origin, float upperBounds, float lowerBounds)
        {
            Origin = origin;
            UpperBounds = upperBounds;
            LowerBounds = lowerBounds;
        }

        public virtual void ExpandLevel(float upperExtend, float lowerExtend)
        {
            Threshold nextLeaf = new Threshold(Origin,
                leaf.UpperBounds + upperExtend, leaf.LowerBounds + lowerExtend, leaf.level + 1);

            leaf.child = nextLeaf;
            leaf = nextLeaf;
        }

        public bool IsWithin(float value)
        {
            return IsWithin(value, out int level);
        }

        public bool IsWithin(float value, out int level)
        {
            level = -1;
            if (!leaf.rangeCheck(value))
                return false;

            Threshold current = this;
            while (current.child != null)
            {
                if (rangeCheck(value))
                {
                    level = current.level;
                    return true;
                }    

                current = current.child;
            }

            return false;
        }

        protected bool rangeCheck(float value)
        {
            var placement = value - Origin;

            return placement <= UpperBounds &&
                placement >= -LowerBounds;
        }

        public enum ThresholdState
        {
            Under,
            Over,
            /// <summary>
            /// Within the threshold, but above the Origin
            /// </summary>
            Above,
            /// <summary>
            /// Within the threshold, but below the Origin
            /// </summary>
            Below,
            At
        }
    }

    public class ThresholdList<T> : Threshold
    {
        private List<T> inValues;

        public T LowerOutValue { get; private set; }
        public T UpperOutValue { get; private set; }
        public T InValue { get => inValues[0]; }

        public ThresholdList(float origin, float upperBounds, float lowerBounds,
            T innerValue, T lowerOutValue, T upperOutValue) :
            base(origin, upperBounds, lowerBounds)
        {
            inValues = new List<T>() { innerValue };
            LowerOutValue = lowerOutValue;
            UpperOutValue = upperOutValue;
        }

        public T GetValue(float point)
        {
            return GetValue(point, out ThresholdState state);
        }

        public T GetValue(float point, out ThresholdState state)
        {
            if (IsWithin(point, out int level))
            {
                state = point > Origin ? ThresholdState.Above :
                    point == Origin ? ThresholdState.At : ThresholdState.Below;
                return inValues[level];
            }
            else if (point - Origin > UpperBounds)
            {
                state = ThresholdState.Over;
                return UpperOutValue;
            }
            else
            {
                state = ThresholdState.Under;
                return LowerOutValue;
            }
        }

        public void AddLevel(T value, float upperExtend, float lowerExtend)
        {
            inValues.Add(value);
            ExpandLevel(upperExtend, lowerExtend);
        }
    }

    //public class Threshold
    //{
    //    private int level;
    //    private Threshold segment;

    //    public float Origin { get; private set; }
    //    public float InnerBound { get; private set; }
    //    public float OuterBound { get; private set; }

    //    public Threshold(float origin, float radiousBound) :
    //        this(origin, -radiousBound, radiousBound)
    //    { }

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="value">The origin value of the threshold.</param>
    //    /// <param name="innerBound">Offset from value to still be within threshold, should be a negative number.</param>
    //    /// <param name="outerbound">Distance from value to still be within threshold, should be a positive number.</param>
    //    public Threshold(float origin, float innerBound, float outerbound)
    //    {
    //        Origin = origin;
    //        InnerBound = innerBound;
    //        OuterBound = outerbound;
    //    }

    //    public void SetSegment(float innerBound, float outerbound)
    //    {
    //        if (segment != null)
    //            throw new InvalidOperationException("Threshold: Each threashold can only contain one child segment.");
    //        else if (Mathf.Abs(innerBound) > Mathf.Abs(InnerBound) ||
    //            Mathf.Abs(outerbound) > Math.Abs(OuterBound))
    //            throw new ArgumentOutOfRangeException("Threshold: Child segment cannot have bounds exceeding its parent threshold.");

    //        //level
    //    }

    //    public bool IsWithin(float value)
    //    {

    //    }

    //    public bool IsWithin(float value, out int level)
    //    {

    //    }
    //}
}
