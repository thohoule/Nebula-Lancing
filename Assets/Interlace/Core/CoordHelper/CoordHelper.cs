using FishNet.Connection;
using FishNet.Object;
using Interlace;
using UnityEngine;

namespace Assets.Interlace.Core.CoordHelper
{
    public static class CoordHelper
    {
        public static TCoord UnpackCoord<THandler, TCoord>(THandler handlerPrefab,
            GameObject directoryParent, NetworkConnection owner = null)
            where THandler : NetworkBehaviour
            where TCoord : MonoBehaviour, ICoordinator<THandler>
        {
            var handler = GameObject.Instantiate(handlerPrefab);
            setParentCheck(directoryParent, handler.gameObject);
            FishNet.InstanceFinder.ServerManager.Spawn(handler.gameObject, owner);

            var coordInstance = handler.gameObject.AddComponent<TCoord>();
            coordInstance.SetHandler(handlerPrefab);

            coordInstance.Initialize();

            return coordInstance;
        }

        private static void setParentCheck(GameObject directoryParent,
            GameObject handlerObject)
        {
            if (directoryParent != null)
                handlerObject.transform.SetParent(directoryParent.transform, true);
        }
    }
}
