using FishNet.Object;

namespace Assets.Code
{
    public abstract class NetPrefabAsset : NetworkBehaviour, IPrefabAsset
    {
        public virtual string PrefabName { get => gameObject.name; }
        public virtual bool NameSearchEnabled { get => true; }
        public virtual bool TypeSearchEnabled { get => false; }
    }
}
