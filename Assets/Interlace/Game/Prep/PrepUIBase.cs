using UnityEngine;

namespace Assets.Game
{
    public abstract class PrepUIBase : MonoBehaviour
    {
        public virtual void SetSelectShipText(string text)
        { }

        public virtual void SetReadyState(bool readyState)
        { }

        public virtual void EnableOwnerEdit()
        { }
    }
}
