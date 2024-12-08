using UnityEngine;

namespace Assets.Code
{
    public abstract class ScriptAsset : ScriptableObject, IPrefabAsset
    {
        public virtual string PrefabName { get => name; }
        public virtual bool NameSearchEnabled { get => true; }
        public virtual bool TypeSearchEnabled { get => false; }
    }
}
