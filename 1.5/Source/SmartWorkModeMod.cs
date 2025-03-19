using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace SmartWorkMode
{
    [StaticConstructorOnStartup]
    public static class SmartWorkModeInitializer
    {
        static SmartWorkModeInitializer()
        {
            SmartWorkModeMod.Settings = SmartWorkModeMod.Mod.GetSettings<SmartWorkModeSettings>();
        }
    }

    public class SmartWorkModeMod : Mod
    {
        public const string PACKAGE_ID = "smartworkmode.1trickPwnyta";
        public const string PACKAGE_NAME = "Smart Work Mode";

        public static SmartWorkModeMod Mod;
        public static SmartWorkModeSettings Settings;

        public SmartWorkModeMod(ModContentPack content) : base(content)
        {
            var harmony = new Harmony(PACKAGE_ID);
            harmony.PatchAll();
            harmony.Patch(typeof(MechanitorControlGroup).Constructor(new[] { typeof(Pawn_MechanitorTracker) }), null, typeof(Patch_MechanitorControlGroup_ctor).Method(nameof(Patch_MechanitorControlGroup_ctor.Postfix)));

            Mod = this;

            Log.Message($"[{PACKAGE_NAME}] Loaded.");
        }

        public override string SettingsCategory() => PACKAGE_NAME;

        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);
            SmartWorkModeSettings.DoSettingsWindowContents(inRect);
        }
    }
}
