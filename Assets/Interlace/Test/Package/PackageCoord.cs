using UnityEngine;
using FishNet.Object;
using Unity.VisualScripting;

namespace Interlace.Test
{
    public abstract class PackageCoord<THandler, TCoord> : MonoBehaviour
        where THandler : NetworkBehaviour
        where TCoord : MonoBehaviour, ICoordinator<THandler>
    {
        [SerializeField]
        private THandler handlerPrefab;
        [SerializeField]
        private TCoord coordPrefab;

        public TCoord Instantiate()
        {
            //var puppetInstance = Instantiate(handlerPrefab);
            //FishNet.InstanceFinder.ServerManager.Spawn(puppetInstance.gameObject);

            ////var coordInstance = Instantiate(coordPrefab);
            //var coordInstance = puppetInstance.AddComponent<TCoord>();
            //coordInstance.SetHandler(puppetInstance);
            //return coordInstance;

            return Instantiate(out THandler handler);
        }

        public TCoord Instantiate(out THandler handler)
        {
            handler = Instantiate(handlerPrefab);
            FishNet.InstanceFinder.ServerManager.Spawn(handler.gameObject);

            //var coordInstance = Instantiate(coordPrefab);
            var coordInstance = handler.AddComponent<TCoord>();
            coordInstance.SetHandler(handler);
            return coordInstance;
        }
    }
}
