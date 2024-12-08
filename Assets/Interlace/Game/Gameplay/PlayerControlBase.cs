using UnityEngine;

namespace Assets.Game
{
    public abstract class PlayerControlBase : MonoBehaviour
    {
        protected static PlayerControlBase instance { get; set; }

        private void Awake()
        {
            instance = this;
            onAwake();
        }

        protected virtual void onAwake()
        { }

        protected virtual void onEnableControl()
        { }

        public static void EnableControl()
        {
            instance.onEnableControl();
        }
    }
}
