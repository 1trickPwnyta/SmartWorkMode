using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Verse;

namespace SmartWorkMode
{
    [HarmonyPatch(typeof(Gravship))]
    [HarmonyPatch("CopyAreas")]
    public static class Patch_Gravship
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> instructionsList = instructions.ToList();
            object areaField = instructionsList.First(i => i.opcode == OpCodes.Stfld).operand;

            int firstIndex = instructionsList.FindIndex(i => i.Calls(typeof(List<Pawn>).Method(nameof(List<Pawn>.AddRange))));
            instructionsList.InsertRange(firstIndex + 1, new[]
            {
                new CodeInstruction(OpCodes.Ldloc_1),
                new CodeInstruction(OpCodes.Ldfld, areaField),
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldfld, typeof(Gravship).Field(nameof(Gravship.areas))),
                new CodeInstruction(OpCodes.Ldfld, typeof(MoveableAreas).Field(nameof(MoveableAreas.homeArea))),
                new CodeInstruction(OpCodes.Call, typeof(PatchUtility_Gravship).Method(nameof(PatchUtility_Gravship.CopySmartShutdownAreaToGravship)))
            });

            int lastIndex = instructionsList.FindLastIndex(i => i.Calls(typeof(List<Pawn>).Method(nameof(List<Pawn>.AddRange))));
            instructionsList.InsertRange(lastIndex + 1, new[]
            {
                new CodeInstruction(OpCodes.Ldloc_1),
                new CodeInstruction(OpCodes.Ldfld, areaField),
                new CodeInstruction(OpCodes.Ldloc_S, 8),
                new CodeInstruction(OpCodes.Call, typeof(PatchUtility_Gravship).Method(nameof(PatchUtility_Gravship.CopySmartShutdownAreaToGravship)))
            });
            
            return instructionsList;
        }
    }

    public static class PatchUtility_Gravship
    {
        public static void CopySmartShutdownAreaToGravship(Area area, MoveableArea_Allowed areaGravship)
        {
            foreach (MechanitorControlGroup group in Utility.shutdownAreas.Keys)
            {
                if (group.GetSmartShutdownArea(area.Map) == area)
                {
                    group.SetSmartShutdownAreaGravship(areaGravship);
                }
            }
        }
    }
}
