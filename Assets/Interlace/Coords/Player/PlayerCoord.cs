using UnityEngine;
using Assets.Game;
using Assets.Entities;
using TeaSteep.Character.Status.Effect;
using TeaSteep.Character.Status;
using Assets.Game.Character;
using Unity.VisualScripting;
using Assets.Code.Characters;
using Assets.Code;
using TeaSteep;
using IGameEntity = Assets.Game.IGameEntity;
using FishNet;
using Assets.Services;
using System.Globalization;
using FishNet.Component.Transforming;

namespace Interlace
{
    /// <summary>
    /// This is used to keep track of the player's assets and is used to find that
    /// 'player'.
    /// </summary>
    public class PlayerCoord : MonoBehaviour, ICoordinator<PlayerHandler2>, 
        IGameEntity
    {
        private const float Shield_Tick = 1;

        private PlayerHandler2 handler;
        private AvatarCoord avatar;
        private ImmediateResolver resolver;
        private float shieldRefreshTime;
        private float shieldTickTime;
        private bool shieldIsBroken;

        protected StatusManager status;
        protected EffectManager effects;
        protected ActorHandler actorHandler { get => handler.ActorHandler; }
        protected ActorEntity entity { get => actorHandler.Entity; }

        public AvatarCoord AvatarCoord { get; internal set; }
        public StatusManager Status { get { return status ?? (status = new StatusManager()); } }
        public EffectManager Effects { get { return effects ?? (effects = new EffectManager()); } }
        public int Health { get => entity.Health; set => actorHandler.OnHealthChange(value); }
        public int MaxHealth { get => entity.MaxHealth; set => actorHandler.OnMaxHealthChange(value); }
        public int Shield { get => entity.Shield; set => actorHandler.OnShieldChange(value); }
        public int MaxShield { get => entity.MaxShield; set => actorHandler.OnShieldChange(value); }
        public int ShieldRefreshRate { get => entity.ShieldRefreshRate; set => actorHandler.Entity.ShieldRefreshRate = value; }
        public float ShieldRefreshDelay { get => entity.ShieldRefreshDelay; }
        public bool IsDead { get => entity.IsDead; set => actorHandler.OnDeathChange(value); }
        public Weapon PrimaryWeapon { get; set; }
        public Weapon SecondaryWeapon { get; set; }

        public Vector3 Position { get => transform.position; }
        public Vector3 Forward { get { return transform.position + transform.forward; } }

        private void Awake()
        {
            resolver = new ImmediateResolver(this);
        }

        public void Initialize()
        {
        }

        public void SetHandler(PlayerHandler2 handler)
        {
            this.handler = handler;
        }

        private void Update()
        {
            if (transform.position.y != 0)
                transform.position = new Vector3(transform.position.x,
                    0, transform.position.z);

            if (!IsDead && Health <= 0)
            {
                IsDead = true;
            }

            if (IsDead)
                return;


        }

        #region Damage and Refresh
        public void InflictDamage(int amount)
        {
            shieldRefreshTime = 0;

            var block = Shield - amount;

            if (block <= 0)//goes to health
            {
                if (Shield != 0)
                {
                    Shield = 0;
                }

                shieldIsBroken = true;
                actorHandler.OnShieldCooldown();
                //PlayingControl.PlayingUI.ShieldCooldown.StartCooldown(blankHanlder,
                //    ShieldRefreshDelay * 3);
                shieldRefreshTime = 0;
                shieldTickTime = Shield_Tick;
                Health = Mathf.Clamp(Health + block, 0, 999);
            }
            else
            {
                shieldTickTime = Shield_Tick;
                Shield = block;
            }
        }

        private void refreshShield()
        {
            if (Shield != MaxShield)
            {
                bool canRecover = shieldIsBroken ?
                    shieldRefreshTime >= (ShieldRefreshDelay * 3) :
                    shieldRefreshTime >= ShieldRefreshDelay;

                if (canRecover)
                {
                    shieldIsBroken = false;

                    if (shieldTickTime >= Shield_Tick)
                    {
                        Shield = Mathf.Clamp(Shield + ShieldRefreshRate, 0, MaxShield);
                        shieldTickTime = 0;
                    }
                    else
                        shieldTickTime += Time.deltaTime;
                }
                else
                    shieldRefreshTime += Time.deltaTime;
            }
        }
        #endregion

        #region Command Handling
        internal void OnPrimaryFire(int charge)
        {
            if (PrimaryWeapon.CanFire(charge))
                PrimaryWeapon.Fire(charge);
        }

        internal void OnSecondaryFire(int charge)
        {
            if (SecondaryWeapon.CanFire(charge))
                SecondaryWeapon.Fire(charge);
        }
        #endregion

        public void Enable()
        {
            gameObject.SetActive(true);
            handler.gameObject.SetActive(true);
        }

        #region Spawn
        public void SpawnPuppet()
        {
            //transform.position = PrepService.GetSpawnPosition(
            //    handler.ProfileHandler.Entity.SeatNumber);
            handler.OnLoadPuppetModel();
            handler.GiveOwnership(handler.ProfileHandler.Entity.Connection);
            handler.UseSpawnPosition(handler.ProfileHandler.Entity.Connection);

            //var shipPrefab = getShipPrefab(handler.ProfileHandler.Entity.Prep.SelectedShip);
            //actorHandler.Puppet = Instantiate(shipPrefab);

            //transform.position = PrepService.GetSpawnPosition(
            //    handler.ProfileHandler.Entity.SeatNumber);

            //actorHandler.Puppet.gameObject.SetParentAndLocals(gameObject);
            //actorHandler.DamagedParticles = actorHandler.Puppet.DamagedParticles;
            //actorHandler.DeathEffect = actorHandler.Puppet.DeathEffect;

            //actorHandler.CameraTarget = actorHandler.GetComponent<CameraTarget>();

            //loadWeaponModel(actorHandler.Puppet.gameObject, "PlasmaGun",
            //    actorHandler.Puppet.PrimaryAttachPoint.transform.position);
            //loadWeaponModel(actorHandler.Puppet.gameObject, "HarpoonGrabber",
            //    actorHandler.Puppet.SecondaryAttachPoint.transform.position);

            //InstanceFinder.ServerManager.Spawn(actorHandler.Puppet.gameObject);
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

                //setPrimaryWeapon(instance.gameObject);
            }
        }
        #endregion
    }

    //public class AvatarCoordUnpacker : CoordUnpacker<AvatarHandler, AvatarCoord>
    //{

    //}
}
