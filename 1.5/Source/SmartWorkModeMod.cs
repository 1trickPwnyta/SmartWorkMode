using HarmonyLib;
using Verse;

namespace SmartWorkMode
{
    public class SmartWorkModeMod : Mod
    {
        public const string PACKAGE_ID = "smartworkmode.1trickPwnyta";
        public const string PACKAGE_NAME = "Smart Work Mode";

        public SmartWorkModeMod(ModContentPack content) : base(content)
        {
            var harmony = new Harmony(PACKAGE_ID);
            harmony.PatchAll();

            Log.Message($"[{PACKAGE_NAME}] Loaded.");
        }
    }
}
