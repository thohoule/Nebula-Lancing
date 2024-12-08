using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Characters
{
    [CreateAssetMenu(fileName = "NewShipSettings", menuName = "Data/Ship/Ship Settings")]
    public class ShipSettings : ScriptAsset
    {
        [SerializeField]
        private Vector3 primaryAttachPoint;
        [SerializeField]
        private Vector3 secondaryAttachPoint;

        public Vector3 PrimaryAttachPoint { get => primaryAttachPoint; }
        public Vector3 SecondaryAttachPoint { get => secondaryAttachPoint; }
    }
}
