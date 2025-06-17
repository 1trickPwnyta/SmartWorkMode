using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using Verse;

namespace SmartWorkMode
{
    // Patched manually in mod constructor
    public static class Patch_MechanitorControlGroup_ctor
    {
        public static void Postfix(ref MechWorkModeDef ___workMode)
        {
            ___workMode = SmartWorkModeSettings.AdditionalDefaultWorkMode;
        }
    }

    [HarmonyPatch(typeof(MechanitorControlGroup))]
    [HarmonyPatch(nameof(MechanitorControlGroup.ExposeData))]
    public static class Patch_MechanitorControlGroup_ExposeData
    {
        public static void Postfix(MechanitorControlGroup __instance)
        {
            Dictionary<Map, Area> shutdownAreas = __instance.GetSmartShutdownAreas();
            Scribe_Collections.Look(ref shutdownAreas, "shutdownAreas", LookMode.Reference, LookMode.Reference);
            __instance.SetSmartShutdownAreas(shutdownAreas);
        }
    }
}
