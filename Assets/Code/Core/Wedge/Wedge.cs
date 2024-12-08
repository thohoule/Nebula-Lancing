using UnityEngine;

namespace Assets.Code
{
    public struct Wedge
    {
        public Vector2 Forward { get; private set; }
        public float OriginAngle { get; private set; }
        public float StartAngle { get; private set; }
        public float EndAngle { get; private set; }

        public Wedge(Vector3 forward, float startAngle, float endAngle) :
            this(new Vector2(forward.x, forward.z), startAngle, endAngle)
        { }

        public Wedge(float startAngle, float endAngle) :
            this(Vector2.up, startAngle, endAngle)
        { }

        public Wedge(Vector2 forward, float startAngle, float endAngle)
        {
            Forward = forward.normalized;
            OriginAngle = Vector2.SignedAngle(Vector2.up, forward);
            StartAngle = OriginAngle + startAngle;
            EndAngle = OriginAngle + endAngle;
        }

        public bool IsWithin(Vector3 direction)
        {
            return IsWithin(new Vector2(direction.x, direction.z));
        }

        public bool IsWithin(Vector2 direction)
        {
            var angle = Vector2.SignedAngle(Vector2.up, direction);

            return angle >= StartAngle && angle <= EndAngle;
        }

        public bool IsWithin(Vector3 direction, out float relevantAngle)
        {
            return IsWithin(new Vector2(direction.x, direction.z),
                out relevantAngle);
        }

        public bool IsWithin(Vector2 direction, out float relevantAngle)
        {
            var angle = Vector2.SignedAngle(Vector2.up, direction);
            relevantAngle = angle - OriginAngle;

            return angle >= StartAngle && angle <= EndAngle;
        }

        public Vector2 GetPoint(float minRange, float maxRange)
        {
            return GetPoint(Random.Range(minRange, maxRange));
        }

        public Vector2 GetPoint(float range)
        {
            var selection = Random.Range(StartAngle, EndAngle);
            var forwardPosition = Forward * range;

            return Quaternion.Euler(0, 0, selection) * forwardPosition;
        }

        /// <summary>
        /// Will find how off a direction is and return the difference in Signed Angle for
        /// the direction to be corrected for this wedge; will return 0 if direction is 
        /// within.
        /// </summary>
        /// <returns></returns>
        public float CorrectiveAngle(Vector2 direction)
        {
            var angle = Vector2.SignedAngle(Vector2.up, direction);

            if (angle < StartAngle)
                return StartAngle - angle;
            else if (angle > EndAngle)
                return EndAngle - angle;

            return 0;
        }

        public Wedge Inverse()
        {
            return new Wedge(Forward * -1, StartAngle, EndAngle);
        }
    }
}
