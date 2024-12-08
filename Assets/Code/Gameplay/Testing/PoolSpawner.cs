using System;
using UnityEngine;
using FishNet.Object;
using Assets.Code.Characters.Weapons.Common;

namespace Assets.Code.Gameplay.Testing
{
    public class PoolSpawner : NetworkBehaviour
    {
        private void Awake()
        {
            instance = this;
        }

        public override void OnStartServer()
        {
            base.OnStartServer();

            var prefab = PrefabAsset.GetPrefab<BallProjectileAsset>();
            NetworkManager.CacheObjects(prefab.NetworkObject, 30, IsServer);

            var grabberPrefab = PrefabAsset.GetPrefab<GrabberProjectileAsset>();
            NetworkManager.CacheObjects(grabberPrefab.NetworkObject, 4, IsServer);
        }

        private T create<T>()
            where T : NetworkBehaviour, IPrefabAsset
        {
            var prefab = PrefabAsset.GetPrefab<T>();
            //var asset = Instantiate(prefab);

            var asset = NetworkManager.GetPooledInstantiated(prefab.NetworkObject, true);
            Debug.Log("Projectile Spawned");
            Spawn(asset);
            return asset.GetComponent<T>();
        }

        private static PoolSpawner instance;

        public static T Create<T>()
            where T : NetworkBehaviour, IPrefabAsset
        {
            return instance.create<T>();
        }
    }
}
