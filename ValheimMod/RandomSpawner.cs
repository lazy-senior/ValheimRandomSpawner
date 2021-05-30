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
        private const string GUID = "lazysenior.ValheimMod";
        private const string Name = "ValheimMod by Lazy-Senior";
        private const string Version = "0.0.0.2";
        private readonly Harmony harmony = new Harmony(GUID);

        public static List<GameObject> Charaters = new List<GameObject>();

        void Awake()
        {
            harmony.PatchAll();
        }
    }
}
