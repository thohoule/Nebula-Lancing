using System;
using UnityEngine;
using FishNet.Object;
using FishNet.Connection;
using Assets.Game;
using Assets.Game.Character;
using Interlace.Sync;
using Assets.Entities;
using Assets.Services;
using Assets.Code.Characters;
using Assets.Code;
using TeaSteep;

namespace Interlace
{
    public class PlayerHandler2 : NetworkBehaviour
    {
        //Profile handler
        [SerializeField]
        private ProfileHandler profileHandler;
        [SerializeField]
        private PrepHandler prepHandler;
        [SerializeField]
        private ActorHandler actorHandler;

        public ProfileHandler ProfileHandler { get => profileHandler; }
        public PrepHandler PrepHandler { get => prepHandler; }
        public ActorHandler ActorHandler { get => actorHandler; }
        //public ShipAsset PuppetShip { get; private set; }

        #region Downstream
        #region Load Puppet
        [ObserversRpc]
        internal void OnLoadPuppetModel()
        {
            var shipPrefab = getShipPrefab(profileHandler.Entity.Prep.SelectedShip);
            actorHandler.Puppet = Instantiate(shipPrefab);

            actorHandler.Puppet.gameObject.SetParentAndLocals(gameObject);
            actorHandler.DamagedParticles = actorHandler.Puppet.DamagedParticles;
            actorHandler.DeathEffect = actorHandler.Puppet.DeathEffect;

            actorHandler.CameraTarget = actorHandler.GetComponent<CameraTarget>();

            loadWeaponModel(actorHandler.Puppet.gameObject, "PlasmaGun",
                actorHandler.Puppet.PrimaryAttachPoint.transform.position);
            loadWeaponModel(actorHandler.Puppet.gameObject, "HarpoonGrabber",
                actorHandler.Puppet.SecondaryAttachPoint.transform.position);
        }

        private ShipAsset getShipPrefab(int selectedShip)
        {
            //use prep to load prefab
            switch (selectedShip)
            {
                case 0:
                    return PrefabAsset.GetPrefab<ShipAsset>(ShipAsset.Small_Ship);
                case 1:
                    return PrefabAsset.GetPrefab<ShipAsset>(ShipAsset.Medium_Ship);
                default:
                    return PrefabAsset.GetPrefab<ShipAsset>(ShipAsset.Large_Ship);
            }
        }

        private void loadWeaponModel(GameObject modelObject, string assetName,
            Vector3 attachPoint)
        {
            if (assetName == "")
                return;

            if (PrefabAsset.TryGetPrefab(assetName, out WeaponPrefab prefab))
            {
                var instance = Instantiate(prefab);
                var attach = attachPoint + prefab.AttachOffset;
                instance.gameObject.SetParentAndLocals(modelObject, attach);

                //temp line fixing bug
                instance.transform.position = attachPoint;

                //setPrimaryWeapon(instance.gameObject);
            }
        }
        #endregion

        [TargetRpc]
        internal void UseSpawnPosition(NetworkConnection connection)
        {
            transform.position = PrepService.GetSpawnPosition(
                ProfileHandler.Entity.SeatNumber);
        }
        #endregion
    }
}
