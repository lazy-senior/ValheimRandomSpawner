using BepInEx;
using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace ExampleAxe
{
    [BepInPlugin("Cluestep.CustomItems", "CustomItems", "0.0.1")]
    [BepInProcess("valheim.exe")]
    public class Setup : BaseUnityPlugin
    {
        private readonly Harmony harmony = new Harmony("Cluestep.CustomItem");
        private static GameObject BestAxe;
        private static GameObject BestBow;

        void Awake()
        {
            var assetBundleAxe = GetAssetBundleFromResources("bestaxe");
            var assetBundleBow = GetAssetBundleFromResources("bestbow");
            BestAxe = assetBundleAxe.LoadAsset<GameObject>("Assets/CustomItems/BestAxe.prefab");
            BestBow = assetBundleBow.LoadAsset<GameObject>("Assets/CustomItems/BestBow.prefab");
            harmony.PatchAll();
        }

        void OnDestroy()
        {
            harmony.UnpatchSelf();
        }

        public static AssetBundle GetAssetBundleFromResources(string fileName)
        {
            var execAssembly = Assembly.GetExecutingAssembly();

            var resourceName = execAssembly.GetManifestResourceNames()
                .Single(str => str.EndsWith(fileName));

            using (var stream = execAssembly.GetManifestResourceStream(resourceName))
            {
                return AssetBundle.LoadFromStream(stream);
            }
        }

        [HarmonyPatch(typeof(ZNetScene), "Awake")]
        public static class ZNetScene_Awake_Patch
        {
            public static void Prefix(ZNetScene __instance)
            {
                if (__instance == null)
                {
                    return;
                }

                __instance.m_prefabs.Add(BestAxe);
                __instance.m_prefabs.Add(BestBow);
            }
        }

        [HarmonyPatch(typeof(ObjectDB), "CopyOtherDB")]
        public static class ObjectDB_CopyOtherDB_Patch
        {
            public static void Postfix()
            {
                AddCustomItems();
            }
        }

        [HarmonyPatch(typeof(ObjectDB), "Awake")]
        public static class ObjectDB_Awake_Patch
        {
            public static void Postfix()
            {
                AddCustomItems();
            }
        }

        private static void AddCustomItems()
        {
            if (ObjectDB.instance == null || ObjectDB.instance.m_items.Count == 0 || ObjectDB.instance.GetItemPrefab("Amber") == null)
            {
                return;
            }

            var itemDrop = BestAxe.GetComponent<ItemDrop>();
            if (itemDrop != null)
            {
                if (ObjectDB.instance.GetItemPrefab(BestAxe.name.GetStableHashCode()) == null)
                {
                    ObjectDB.instance.m_items.Add(BestAxe);
                    Dictionary<int, GameObject> m_itemsByHash = (Dictionary<int, GameObject>)typeof(ObjectDB).GetField("m_itemByHash", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ObjectDB.instance);
                    m_itemsByHash[BestAxe.name.GetStableHashCode()] = BestAxe;
                }
            }

            itemDrop = BestBow.GetComponent<ItemDrop>();
            if (itemDrop != null)
            {
                if (ObjectDB.instance.GetItemPrefab(BestBow.name.GetStableHashCode()) == null)
                {
                    ObjectDB.instance.m_items.Add(BestBow);
                    Dictionary<int, GameObject> m_itemsByHash = (Dictionary<int, GameObject>)typeof(ObjectDB).GetField("m_itemByHash", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ObjectDB.instance);
                    m_itemsByHash[BestBow.name.GetStableHashCode()] = BestBow;
                }
            }

        }
    }
}
