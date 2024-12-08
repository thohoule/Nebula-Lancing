using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code
{
    public abstract partial class PrefabAsset : MonoBehaviour, IPrefabAsset
    {
        private const string Prefab_Dir = "Prefabs";

        private static List<IPrefabAsset> prefabAssets;
        private static Dictionary<string, int> nameLookup;
        private static Dictionary<Type, int> typeLookup;

        #region Get Asset
        public static IPrefabAsset GetPrefab(string name)
        {
            TryGetPrefab(name, out PrefabAsset prefabAsset);
            return prefabAsset;
        }

        public static T GetPrefab<T>(string name)
            where T : class, IPrefabAsset
        {
            TryGetPrefab<T>(name, out T prefabAsset);
            return prefabAsset;
        }

        public static T GetPrefab<T>()
            where T : class, IPrefabAsset
        {
            TryGetPrefab(out T prefabAsset);
            return prefabAsset;
        }

        public static IPrefabAsset GetPrefab(Type type)
        {
            TryGetPrefab(type, out IPrefabAsset prefabAsset);
            return prefabAsset;
        }

        public static bool TryGetPrefab(string name, out IPrefabAsset prefab)
        {
            if (prefabAssets == null)
                loadCatelog();

            if (nameLookup.TryGetValue(name, out int index))
            {
                prefab = prefabAssets[index];
                return true;
            }

            prefab = null;
            return false;
        }

        public static bool TryGetPrefab<T>(string name, out T prefab)
            where T : class, IPrefabAsset
        {
            if (prefabAssets == null)
                loadCatelog();

            if (nameLookup.TryGetValue(name, out int index))
            {
                prefab = prefabAssets[index] as T;
                return prefab != null;
            }

            prefab = null;
            return false;
        }

        public static bool TryGetPrefab<T>(out T prefab)
            where T : class, IPrefabAsset
        {
            if (TryGetPrefab(typeof(T), out IPrefabAsset basePrefab))
            {
                prefab = basePrefab as T;
                return prefab != null;
            }

            prefab = null;
            return false;
        }

        public static bool TryGetPrefab(Type type, out IPrefabAsset prefab)
        {
            if (prefabAssets == null)
                loadCatelog();

            if (typeLookup.TryGetValue(type, out int index))
            {
                prefab = prefabAssets[index];
                return true;
            }

            prefab = null;
            return false;
        }
        #endregion

        #region Loading Catelog
        private static void loadCatelog()
        {
            prefabAssets = new List<IPrefabAsset>();
            nameLookup = new Dictionary<string, int>();
            typeLookup = new Dictionary<Type, int>();

            foreach (var prefab in getAllAssets())
            {
                var index = prefabAssets.Count;
                prefabAssets.Add(prefab);

                if (prefab.NameSearchEnabled)
                {
                    try
                    {
                        nameLookup.Add(prefab.PrefabName, index);
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError(string.Format("Error adding named prefab: {0}", ex.Message));
                    }
                }

                if (prefab.TypeSearchEnabled)
                {
                    try
                    {
                        typeLookup.Add(prefab.GetType(), index);
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError(string.Format("Error adding typed prefab: {0}", ex.Message));
                    }
                }
            }
        }

        private static IEnumerable<IPrefabAsset> getAllAssets()
        {
            //Resources.LoadAll<PrefabAsset>("");
            //Resources.LoadAll<NetPrefabAsset>("");

            foreach (var asset in Resources.LoadAll<PrefabAsset>(Prefab_Dir))
            {
                yield return asset;
            }

            foreach (var netAsset in Resources.LoadAll<NetPrefabAsset>(Prefab_Dir))
            {
                yield return netAsset;
            }

            foreach (var script in Resources.LoadAll<ScriptAsset>(Prefab_Dir))
            {
                yield return script;
            }
        }
        #endregion
    }
}
