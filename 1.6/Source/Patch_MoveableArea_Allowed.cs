using HarmonyLib;
using RimWorld.Planet;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;

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
                new CodeInstruction(OpCodes.Call, typeof(Utility).Method(nameof(Utility.CopySmartShutdownAreaToMap)))
            });
            return instructionsList;
        }
    }
}
