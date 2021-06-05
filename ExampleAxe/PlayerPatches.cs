using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RandomSpawner
{
    [HarmonyPatch]
    class PlayerPatches
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(Player), "Awake")]
        static void setGodMode(ref bool ___m_godMode, ref float ___m_runStaminaDrain, ref bool ___m_debugFly)
        {
            ___m_godMode = true;
            ___m_runStaminaDrain = 0f;
            //___m_debugFly = true;

            Debug.Log($"GodMode = {___m_godMode}, runStaminaDrain = {___m_runStaminaDrain} ");
        }

    }
    
}
