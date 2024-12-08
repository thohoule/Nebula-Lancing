using System;
using UnityEngine;

namespace Assets.Entities
{
    [Serializable]
    public class ShipConfigEntity
    {
        public string primaryWeapon;
        public string secondaryWeapon;

        public string PrimaryWeapon { get => primaryWeapon; set => primaryWeapon = value; }
        public string SecondaryWeapon { get => secondaryWeapon; set => secondaryWeapon = value; }
    }
}
