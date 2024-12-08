using UnityEngine;

namespace Assets.Code.Gameplay
{
    /// <summary>
    /// Will be the common base class for the prep and play lanes
    /// </summary>
    public abstract class BaseLane : MonoBehaviour
    {
        internal LaneControl control;

        public int Number { get => control.Number; }
    }
}
