using UnityEngine;

namespace Assets.Code
{
    public abstract partial class PrefabAsset : MonoBehaviour, IPrefabAsset
    {
        public virtual string PrefabName { get => gameObject.name; }
        public virtual bool NameSearchEnabled { get => true; }
        public virtual bool TypeSearchEnabled { get => false; }
    }
}
