﻿using HarmonyLib;
using RimWorld;

namespace SmartWorkMode
{
    [HarmonyPatch(typeof(Pawn_MechanitorTracker))]
    [HarmonyPatch("Notify_ControlGroupAmountMayChanged")]
    public static class Patch_Pawn_MechanitorTracker
    {
        public static void Prefix(Pawn_MechanitorTracker __instance)
        {
            if (__instance.controlGroups.Count == 0 && __instance.TotalAvailableControlGroups > 1)
            {
                MechanitorControlGroup group1 = new MechanitorControlGroup(__instance);
                group1.SetWorkMode(SmartWorkModeSettings.FirstDefaultWorkMode);
                __instance.controlGroups.Add(group1);
                MechanitorControlGroup group2 = new MechanitorControlGroup(__instance);
                group2.SetWorkMode(SmartWorkModeSettings.SecondDefaultWorkMode);
                __instance.controlGroups.Add(group2);
            }
        }
    }
}
