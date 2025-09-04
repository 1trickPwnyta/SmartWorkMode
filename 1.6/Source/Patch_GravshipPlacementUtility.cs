using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Verse;

namespace SmartWorkMode
{
    [HarmonyPatch(typeof(GravshipPlacementUtility))]
    [HarmonyPatch("CopyAreasIntoMap")]
    public static class Patch_GravshipPlacementUtility
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> instructionsList = instructions.ToList();
            int index = instructionsList.IndexOf(instructionsList.FindAll(i => i.opcode == OpCodes.Ldfld && i.operand is FieldInfo f && f == typeof(MoveableAreas).Field(nameof(MoveableAreas.homeArea)))[1]);
            instructionsList.InsertRange(index, new[]
            {
                new CodeInstruction(OpCodes.Ldarg_1),
                new CodeInstruction(OpCodes.Ldfld, typeof(Map).Field(nameof(Map.areaManager))),
                new CodeInstruction(OpCodes.Call, typeof(AreaManager).PropertyGetter(nameof(AreaManager.Home))),
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldfld, typeof(Gravship).Field(nameof(Gravship.areas))),
                new CodeInstruction(OpCodes.Ldfld, typeof(MoveableAreas).Field(nameof(MoveableAreas.homeArea))),
                new CodeInstruction(OpCodes.Call, typeof(Utility).Method(nameof(Utility.CopySmartShutdownAreaToMap)))
            });
            return instructionsList;
        }
    }
}
