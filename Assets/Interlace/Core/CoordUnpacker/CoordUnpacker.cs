using UnityEngine;
using FishNet.Object;
using Unity.VisualScripting;
using FishNet.Connection;

namespace Interlace
{
    public class CoordUnpacker<THandler, TCoord> : MonoBehaviour
        where THandler : NetworkBehaviour
        where TCoord : MonoBehaviour, ICoordinator<THandler>
    {
        [SerializeField]
        private GameObject directoryParent;
        [SerializeField]
        private THandler handlerPrefab;
        //[SerializeField]
        //private TCoord coordPrefab;

        public TCoord Instantiate()
        {
            return Instantiate(out THandler handler);
        }

        public TCoord Instantiate(out THandler handler, NetworkConnection owner = null)
        {
            handler = Instantiate(handlerPrefab);
            setParent(handler);
            FishNet.InstanceFinder.ServerManager.Spawn(handler.gameObject,
                owner);

            var coordInstance = handler.AddComponent<TCoord>();
            coordInstance.SetHandler(handler);
            coordInstance.Initialize();
            return coordInstance;
        }

        private void setParent(THandler handler)
        {
            if (directoryParent != null)
                handler.transform.SetParent(directoryParent.transform, true);
        }
    }
}
