using BepInEx;
using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RandomSpawner
{
    [BepInPlugin(GUID,Name,Version)]
    [BepInProcess("valheim.exe")]
    public class RandomSpawner : BaseUnityPlugin
    {
        private const string GUID = "lazysenior.Valheim.RandomSpawner";
        private const string Name = "RandomSpawner";
        private const string Version = "1.0";
        private readonly Harmony harmony = new Harmony(GUID);

        public static List<GameObject> Charaters = new List<GameObject>();

        void Awake()
        {
            harmony.PatchAll();
        }
    }
}
