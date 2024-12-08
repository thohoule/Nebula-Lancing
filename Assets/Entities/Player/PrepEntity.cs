using System;
using UnityEngine;

namespace Assets.Entities
{
    [Serializable]
    public class PrepEntity
    {
        public ShipConfigEntity smallShipConfig;
        public ShipConfigEntity mediumShipConfig;
        public ShipConfigEntity largeShipConfig;
        public bool IsReady;
        public int SelectedShip;

        //public bool IsReady { get; internal set; }
        //public int SelectedShip { get; internal set; }

        public PrepEntity()
        {
            smallShipConfig = new ShipConfigEntity();
            mediumShipConfig = new ShipConfigEntity();
            largeShipConfig = new ShipConfigEntity();
        }

        public void SetPrimary(string weaponAsset)
        {
            GetCurrentConfig().PrimaryWeapon = weaponAsset;
        }

        public void SetSecondary(string weaponAsset)
        {
            GetCurrentConfig().SecondaryWeapon = weaponAsset;
        }

        public ShipConfigEntity GetCurrentConfig()
        {
            switch (SelectedShip)
            {
                case 0:
                    return smallShipConfig;
                case 1:
                    return mediumShipConfig;
                default:
                    return largeShipConfig;
            }
        }

        //public Vector3 GetPrimaryAttachPoint()
        //{
        //    var settings = getShipSettings();

        //    return settings.PrimaryAttachPoint; //+ offset
        //}

        //public Vector3 GetSecondaryPoint()
        //{
        //    var settings = getShipSettings();

        //    return settings.SecondaryAttachPoint;
        //}

        public string GetPrimaryWeapon()
        {
            var ship = GetCurrentConfig();
            return ship.PrimaryWeapon;
        }

        public string GetSecondaryWeapon()
        {
            var ship = GetCurrentConfig();
            return ship.SecondaryWeapon;
        }

        public static PrepEntity DefualtEntity()
        {
            return new PrepEntity()
            {
                smallShipConfig = new ShipConfigEntity()
                {
                    PrimaryWeapon = "Plasma"
                },
                mediumShipConfig = new ShipConfigEntity()
                {
                    PrimaryWeapon = "Plasma"
                },
                largeShipConfig = new ShipConfigEntity()
                {
                    PrimaryWeapon = "Plasma"
                }
            };
        }

        //private ShipSettings getShipSettings()
        //{
        //    switch (SelectedShip)
        //    {
        //        case 0:
        //            return PrefabAsset.GetPrefab<ShipSettings>(Small_Settings);
        //        case 1:
        //            return PrefabAsset.GetPrefab<ShipSettings>(Medium_Settings);
        //        default:
        //            return PrefabAsset.GetPrefab<ShipSettings>(Large_Settings);
        //    }
        //}
    }
}
