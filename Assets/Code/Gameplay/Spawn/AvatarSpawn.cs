using UnityEngine;
using Assets.Code.Characters;
using TeaSteep;
using FishNet.Connection;
using FishNet.Object;
using Assets.Code.Gameplay.Network;
using Assets.Code.Characters.Weapons.Primary;
using Assets.Code.Characters.Weapons.Secondary;
using Unity.VisualScripting;

namespace Assets.Code.Gameplay
{
    public class AvatarSpawn : NetworkBehaviour
    {
        [SerializeField]
        private FocusCameraController cameraController;
        [SerializeField]
        private NetActor actorPrefab;

        public void UseSpawn(RegisteredClient client)
        {
            var connection = client.Connection;
            var prep = client.Player.Prep;

            //use prep to load prefab
            //var avatarPrefab = PrefabAsset.GetPrefab<AvatarAsset>();
            //var shipPrefab = getShipPrefab(prep.SelectedShip);
            var avatar = Instantiate(actorPrefab);
            //var ship = Instantiate(shipPrefab);

            //avatar.DamagedParticles = ship.DamagedParticles;
            //avatar.DeathEffect = ship.DeathEffect;
            //ship.gameObject.SetParentAndLocals(avatar.gameObject);
            avatar.transform.position = transform.position;
            //avatar.owner = client.Player;
            //avatar.Health = 60;
            //avatar.MaxHealth = 60;
            //avatar.MaxShield = 100;
            //avatar.Shield = 100;
            //avatar.ShieldRefreshDelay = 1.5f;
            //avatar.ShieldRefreshRate = 10;

            //LoadWeaponModel(ship.gameObject, "PlasmaGun",
            //    prep.GetPrimaryAttachPoint());
            //LoadWeaponModel(ship.gameObject, "HarpoonGrabber",
            //    prep.GetSecondaryPoint());

            //avatar.PrimaryWeapon = new TestShooterWeapon(avatar);
            //avatar.SecondaryWeapon = new TestGrabberWeapon(avatar);

            Spawn(avatar.gameObject, connection);
            LoadShipModel(avatar, prep);
            var handler = avatar.GetComponent<NetAvatarHandler>();
            Debug.Log(string.Format("Handler Get {0}", handler != null));
            setAvatar(connection, handler);

            //handler.OnSetHandler(connection, 0);

            //var control = avatar.AddComponent<NetActorControl>();
            var control = NetActorControl.CreateControl(handler);
            control.PrimaryWeapon = new TestShooterWeapon(control);
            control.SecondaryWeapon = new TestGrabberWeapon(control);

            handler.Control = control;
        }

        //private void oldSpawnCode(RegisteredClient client)
        //{
        //    var connection = client.Connection;
        //    var prep = client.Player.Prep;

        //    //use prep to load prefab
        //    var avatarPrefab = PrefabAsset.GetPrefab<AvatarAsset>();
        //    var shipPrefab = getShipPrefab(prep.SelectedShip);
        //    var avatar = Instantiate(avatarPrefab);
        //    var ship = Instantiate(shipPrefab);

        //    avatar.DamagedParticles = ship.DamagedParticles;
        //    avatar.DeathEffect = ship.DeathEffect;
        //    ship.gameObject.SetParentAndLocals(avatar.gameObject);
        //    avatar.transform.position = transform.position;
        //    avatar.owner = client.Player;
        //    avatar.Health = 60;
        //    avatar.MaxHealth = 60;
        //    avatar.MaxShield = 100;
        //    avatar.Shield = 100;
        //    avatar.ShieldRefreshDelay = 1.5f;
        //    avatar.ShieldRefreshRate = 10;

        //    LoadWeaponModel(ship.gameObject, "PlasmaGun",
        //        prep.GetPrimaryAttachPoint());
        //    LoadWeaponModel(ship.gameObject, "HarpoonGrabber",
        //        prep.GetSecondaryPoint());

        //    avatar.PrimaryWeapon = new TestShooterWeapon(avatar);
        //    avatar.SecondaryWeapon = new TestGrabberWeapon(avatar);

        //    Spawn(avatar.gameObject, connection);
        //    setAvatar(connection, avatar);
        //}

        [ObserversRpc]
        public void LoadShipModel(NetActor avatar, ShipPrep prep)
        {
            var shipPrefab = getShipPrefab(prep.SelectedShip);
            var ship = Instantiate(shipPrefab);

            ship.gameObject.SetParentAndLocals(avatar.gameObject);
            avatar.DamagedParticles = ship.DamagedParticles;
            avatar.DeathEffect = ship.DeathEffect;

            LoadWeaponModel(ship.gameObject, "PlasmaGun",
                prep.GetPrimaryAttachPoint());
            LoadWeaponModel(ship.gameObject, "HarpoonGrabber",
                prep.GetSecondaryPoint());


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

        public void UseSpawn(NetworkConnection owner, NetActor avatar)
        {
            //var prefab = PrefabAsset.GetPrefab<NetActor>();
            //var avatar = Instantiate(prefab);

            Spawn(avatar.gameObject, owner);
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

        [TargetRpc]
        private void setAvatar(NetworkConnection connection, NetAvatarHandler handler)
        {
            Debug.Log("set ava message");
            var avatar = handler.Actor;
            var target = avatar.GetComponent<CameraTarget>();

            ActorManager.Avatar = avatar;
            PlayerService.MainAvatarHandler = handler;
            cameraController.Target = target;

            handler.SetUIHandler(0);
        }
    }
}
