
using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RandomSpawner
{
    [HarmonyPatch]
    class ZNetScenePatches
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(ZNetScene), "Awake")]
        static void getNamedPrefabes(ref Dictionary<int, GameObject> ___m_namedPrefabs)
        {
            RandomSpawner.Charaters = ___m_namedPrefabs
                .Where(p => p.Value.GetComponent<Character>() != null && p.Value.GetComponent<Character>().m_boss == false)
                .Select(p => p.Value)
                .ToList();

            Debug.Log($"{RandomSpawner.Charaters.Count()} non-boss charaters found.");
        }
    }
}
