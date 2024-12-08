using UnityEngine;

namespace Assets.Code.Characters
{
    public class WeaponPrefab : PrefabAsset
    {
        [SerializeField]
        private string weaponName;
        [SerializeField]
        private Vector3 attachOffset;

        public override string PrefabName => weaponName;
        public Vector3 AttachOffset { get => attachOffset; }
    }
}
