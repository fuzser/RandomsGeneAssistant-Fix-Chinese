using System;
using System.Collections.Generic;
using Verse;
using HarmonyLib;
using RimWorld;
using UnityEngine;

namespace RandomsGeneAssistant
{
    [StaticConstructorOnStartup]
    public static class HarmonyPatcher
    {
        static HarmonyPatcher()
        {
            Harmony harmony = new Harmony("worldleader.randomsgeneassistant.fixchinese");
            harmony.PatchAll(typeof(HarmonyPatcher).Assembly);
            PatchTradeUIRevisited.HandlePatch(harmony);
            PatchGeneripperUI.HandlePatch(harmony);
            PatchCharacterEditor.HandlePatch(harmony);
        }
    }
}
