using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Verse;

namespace SmartWorkMode
{
    [HarmonyPatch(typeof(MoveableArea_Allowed))]
    [HarmonyPatch(nameof(MoveableArea_Allowed.TryCreateArea))]
    public static class Patch_MoveableArea_Allowed
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> instructionsList = instructions.ToList();
            instructionsList.InsertRange(instructionsList.Count - 1, new[]
            {
                new CodeInstruction(OpCodes.Ldloc_0),
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Call, typeof(PatchUtility_MoveableArea_Allowed).Method(nameof(PatchUtility_MoveableArea_Allowed.CopySmartShutdownAreaToMap)))
            });
            return instructionsList;
        }
    }

    public static class PatchUtility_MoveableArea_Allowed
    {
        public static void CopySmartShutdownAreaToMap(Area area, MoveableArea_Allowed areaGravship)
        {
            foreach (MechanitorControlGroup group in Utility.shutdownAreasGravship.Keys)
            {
                if (group.GetSmartShutdownAreaGravship() == areaGravship)
                {
                    group.SetSmartShutdownArea(area.Map, area);
                }
            }
        }
    }
}
