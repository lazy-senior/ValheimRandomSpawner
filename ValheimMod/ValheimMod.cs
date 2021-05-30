using BepInEx;
using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace ValheimMod
{
    [BepInPlugin(GUID,Name,Version)]
    [BepInProcess("valheim.exe")]
    public class ValheimMod : BaseUnityPlugin
    {
        private const string GUID = "lazysenior.ValheimMod";
        private const string Name = "ValheimMod by LazySenior";
        private const string Version = "0.0.0.1";
        private readonly Harmony harmony = new Harmony(GUID);

        public Dictionary<int, GameObject> m_namedPrefabs = new Dictionary<int, GameObject>();

        void Awake()
        {
            harmony.PatchAll();
        }

        [HarmonyPatch]
        class SpawnArea_Patches
        {
            [HarmonyPrefix]
            [HarmonyPatch(typeof(SpawnArea), "Awake")]
            static void setSpawnAreaPatch(ref float ___m_levelupChance, ref float ___m_spawnIntervalSec, ref float ___m_triggerDistance,
                ref float ___m_spawnRadius, ref float ___m_nearRadius, ref float ___m_farRadius, ref int ___m_maxNear, ref int ___m_maxTotal)
            {
                //public float m_levelupChance = 15f;
                ___m_levelupChance = 50f;
                //public float m_spawnIntervalSec = 30f;ww
                ___m_spawnIntervalSec = 2f;
                //public float m_triggerDistance = 256f;
                ___m_triggerDistance = 1024f;
                //public float m_spawnRadius = 2f;
                ___m_spawnRadius = 30f;
                //public float m_nearRadius = 10f; 
                ___m_nearRadius = 1f;
                //public float m_farRadius = 1000f;
                ___m_farRadius = 1f;
                //public int m_maxNear = 3;
                ___m_maxNear = 999;
                //public int m_maxTotal = 20;
                ___m_maxTotal = 999;
            }

            [HarmonyPostfix]
            [HarmonyPatch(typeof(SpawnArea), "Awake")]
            static void addTrollPrefab(ref ZNetView ___m_nview, ref List<SpawnArea.SpawnData> ___m_prefabs)
            {
                var trollSpawnData = new SpawnArea.SpawnData()
                {
                    m_prefab = ZNetScene.instance.GetPrefab("Troll"),
                    m_weight = 2,
                    m_minLevel = 1,
                    m_maxLevel = 2
                };

                ___m_prefabs.Add(trollSpawnData);
            }

            [HarmonyPrefix]
            [HarmonyPatch(typeof(SpawnArea), "SelectWeightedPrefab")]
            static void logCurrentSpawnData(ref ZNetView ___m_nview, ref List<SpawnArea.SpawnData> ___m_prefabs)
            {
                foreach (SpawnArea.SpawnData spawnData in ___m_prefabs)
                {
                    Debug.Log($"{spawnData.m_prefab.name}:{spawnData.m_weight},{spawnData.m_minLevel},{spawnData.m_maxLevel}");
                }
            }
        }
    }
}
