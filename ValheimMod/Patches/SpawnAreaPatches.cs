using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace RandomSpawner
{
    [HarmonyPatch]
    class SpawnAreaPatches
    {
        private static readonly System.Random Random = new System.Random();
        
        [HarmonyPrefix]
        [HarmonyPatch(typeof(SpawnArea), "Awake")]
        static void setSpawnAreaPatch(ref float ___m_levelupChance, ref float ___m_spawnIntervalSec, ref float ___m_triggerDistance,
           ref float ___m_spawnRadius, ref float ___m_nearRadius, ref float ___m_farRadius, ref int ___m_maxNear, ref int ___m_maxTotal,
           ref ZNetView ___m_nview, ref List<SpawnArea.SpawnData> ___m_prefabs)
        {
            //public float m_levelupChance = 15f;
            ___m_levelupChance = 100f;
            //public float m_spawnIntervalSec = 30f;
            ___m_spawnIntervalSec = 5f;
            //public float m_triggerDistance = 256f;
            ___m_triggerDistance = 10240f;
            //public float m_spawnRadius = 2f;
            ___m_spawnRadius = 10f;
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
        static void addRandomPrefabs(ref ZNetView ___m_nview, ref List<SpawnArea.SpawnData> ___m_prefabs)
        {
            ___m_prefabs = new List<SpawnArea.SpawnData>();
            
            for (var i = 0; i < 5; i++) {
                var character = RandomSpawner.Charaters[Random.Next(RandomSpawner.Charaters.Count)].GetComponent<Character>();
                Debug.Log($"Adding {character.m_name} to spawner");
                ___m_prefabs.Add(new SpawnArea.SpawnData()
                {
                    m_prefab = character.gameObject,
                    m_weight = 1,
                    m_minLevel = 1,
                    m_maxLevel = 3
                });
            }
        }
    }
}
