

using Assets.Game;
using UnityEngine;

namespace Assets.Services.Player
{
    public class StatusService : MonoBehaviour
    {
        private int otherSelector;
        private static StatusService instance;

        [SerializeField]
        private GameObject uiObject;
        [SerializeField]
        private StatusUIHandler primaryHandler;
        [SerializeField]
        private StatusUIHandler topOtherHandler;
        [SerializeField]
        private StatusUIHandler middleOtherHandler;
        [SerializeField]
        private StatusUIHandler bottomOtherHandler;

        private void Awake()
        {
            instance = this;
        }

        public static GameObject GetUIObject()
        {
            return instance.uiObject;
        }

        public static StatusUIHandler GetPrimary()
        {
            return instance.primaryHandler;
        }

        public static StatusUIHandler GetNextOther()
        {
            switch (instance.otherSelector)
            {
                case 0:
                    instance.otherSelector++;
                    return instance.topOtherHandler;
                case 1:
                    instance.otherSelector++;
                    return instance.middleOtherHandler;
                default:
                    return instance.bottomOtherHandler;
            }
        }
    }
}
