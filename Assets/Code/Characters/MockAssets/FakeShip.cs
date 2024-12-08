using UnityEngine;

namespace Assets.Code.Characters.MockAssets
{
    public class FakeShip : PrefabAsset
    {
        [SerializeField]
        private string assetName;

        public override bool NameSearchEnabled => true;
        public override bool TypeSearchEnabled => false;
        public override string PrefabName => assetName;
    }
}
