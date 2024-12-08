using Assets.Code;
using Assets.Code.Characters;
using Assets.Code.Characters.Weapons.Primary;
using Assets.Code.Gameplay;
using FishNet.Object;
using Interlace;
using TeaSteep;
using UnityEngine;
using Assets.Entities;

namespace Assets.Game.Character
{
    public class AvatarSpawn : AvatarSpawnBase
    {
        [SerializeField]
        private FocusCameraController cameraController;
        [SerializeField]
        private AvatarHandler puppetPrefab;

        //public override void UseSpawn(PlayerEntity entity)
        //{
        //    var connection = entity.Connection;
        //    var prep = entity.Prep;

        //    var avatar = Instantiate(actorPrefab);

        //    avatar.transform.position = transform.position;

        //    Spawn(avatar.gameObject, connection);
        //    LoadShipModel(avatar, prep);
        //}

        protected override AvatarHandler onUsed(PlayerEntity entity)
        {
            var connection = entity.Connection;
            var prep = entity.Prep;

            //var avatar = Instantiate(actorPrefab);
            var puppet = Instantiate(puppetPrefab);

            //avatar.transform.position = transform.position;
            puppet.transform.position = transform.position;

            Spawn(puppet.gameObject, connection);
            LoadShipModel(puppet, prep);

            AvatarService.Coord.PrimaryWeapon = new PlasmaGunWeapon(
                AvatarService.Coord);

            return puppet;
        }

        [ObserversRpc]
        public void LoadShipModel(AvatarHandler puppet, PrepEntity prep)
        {
            //Debug.Log("Jimmy newt " + bobby.Blanch + bobby.Steave.Hammer);
            var shipPrefab = getShipPrefab(prep.SelectedShip);
            var ship = Instantiate(shipPrefab);

            ship.gameObject.SetParentAndLocals(puppet.gameObject);
            puppet.DamagedParticles = ship.DamagedParticles;
            puppet.DeathEffect = ship.DeathEffect;

            LoadWeaponModel(ship.gameObject, "PlasmaGun",
                ship.PrimaryAttachPoint.transform.localPosition);
            LoadWeaponModel(ship.gameObject, "HarpoonGrabber",
                ship.SecondaryAttachPoint.transform.localPosition);
        }

        public void LoadWeaponModel(GameObject modelObject, string assetName,
            Vector3 attachPoint)
        {
            if (assetName == "")
                return;

            if (PrefabAsset.TryGetPrefab(assetName, out WeaponPrefab prefab))
            {
                var instance = Instantiate(prefab);
                var attach = attachPoint + prefab.AttachOffset;
                instance.gameObject.SetParentAndLocals(modelObject, attach);

                //setPrimaryWeapon(instance.gameObject);
            }
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
    }
}
