using UnityEngine;
using TeaSteep;

namespace Assets.Code
{
    /// <summary>
    /// A world relevant wedge with a position and rotation.
    /// </summary>
    public struct Cone
    {
        private Wedge wedge;

        //public Transform Transform { get; set; }
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }
        public float Range { get; set; }

        public Cone(Vector3 position, Quaternion rotation, float range,
            Vector2 wedgeDirectionOffset, float startAngle, float endAngle) :
            this(position, rotation, range, new Wedge(wedgeDirectionOffset, startAngle, endAngle))
        {

        }

        //public Cone(Vector3 position, float range, Wedge wedge) :
        //    this(Transform.)

        public Cone(Vector3 position, Quaternion rotation, Wedge wedge) :
            this(position, rotation, float.PositiveInfinity, wedge)
        { }

        public Cone(Vector3 position, Quaternion rotation, float range, Wedge wedge)
        {
            Position = position;
            Rotation = rotation;
            Range = range;
            this.wedge = wedge;
        }

        public bool PointIsWithin(Vector3 position)
        {
            var worldDirection = (position - Position).normalized;
            var localDirection = (Rotation * worldDirection).normalized;
            var distance = Vector3.Distance(Position, position);

            return wedge.IsWithin(localDirection) && distance <= Range;
        }
    }
}
