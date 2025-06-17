using RimWorld;
using System.Linq;
using UnityEngine;
using Verse;

namespace SmartWorkMode
{
    public class SmartWorkModeSettings : ModSettings
    {
        public static bool RequireDormantGroup = true;
        public static MechWorkModeDef FirstDefaultWorkMode = MechWorkModeDefOf.SmartWork;
        public static MechWorkModeDef SecondDefaultWorkMode = RimWorld.MechWorkModeDefOf.SelfShutdown;
        public static MechWorkModeDef AdditionalDefaultWorkMode = MechWorkModeDefOf.SmartWork;

        public static void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);

            listingStandard.CheckboxLabeled("SmartWorkMode_RequireDormantGroup".Translate(), ref RequireDormantGroup);

            Rect rect, buttonRect;

            rect = listingStandard.GetRect(24f);
            Widgets.Label(rect, "SmartWorkMode_FirstDefaultWorkMode".Translate());
            buttonRect = rect.RightPartPixels(24f);
            Widgets.Dropdown(buttonRect, FirstDefaultWorkMode, target => target, target => DefDatabase<MechWorkModeDef>.AllDefsListForReading.Select(d => new Widgets.DropdownMenuElement<MechWorkModeDef>() { option = new FloatMenuOption(d.LabelCap, () => FirstDefaultWorkMode = d, d.uiIcon, Color.white), payload = d }), null, FirstDefaultWorkMode.uiIcon);
            TooltipHandler.TipRegion(buttonRect, () => FirstDefaultWorkMode.LabelCap, 356893);

            rect = listingStandard.GetRect(24f);
            Widgets.Label(rect, "SmartWorkMode_SecondDefaultWorkMode".Translate());
            buttonRect = rect.RightPartPixels(24f);
            Widgets.Dropdown(buttonRect, SecondDefaultWorkMode, target => target, target => DefDatabase<MechWorkModeDef>.AllDefsListForReading.Select(d => new Widgets.DropdownMenuElement<MechWorkModeDef>() { option = new FloatMenuOption(d.LabelCap, () => SecondDefaultWorkMode = d, d.uiIcon, Color.white), payload = d }), null, SecondDefaultWorkMode.uiIcon);
            TooltipHandler.TipRegion(buttonRect, () => SecondDefaultWorkMode.LabelCap, 356894);

            rect = listingStandard.GetRect(24f);
            Widgets.Label(rect, "SmartWorkMode_AdditionalDefaultWorkMode".Translate());
            buttonRect = rect.RightPartPixels(24f);
            Widgets.Dropdown(buttonRect, AdditionalDefaultWorkMode, target => target, target => DefDatabase<MechWorkModeDef>.AllDefsListForReading.Select(d => new Widgets.DropdownMenuElement<MechWorkModeDef>() { option = new FloatMenuOption(d.LabelCap, () => AdditionalDefaultWorkMode = d, d.uiIcon, Color.white), payload = d }), null, AdditionalDefaultWorkMode.uiIcon);
            TooltipHandler.TipRegion(buttonRect, () => AdditionalDefaultWorkMode.LabelCap, 356895);

            listingStandard.End();
        }

        public override void ExposeData()
        {
            Scribe_Values.Look(ref RequireDormantGroup, "RequireDormantGroup", true);
            Scribe_Defs.Look(ref FirstDefaultWorkMode, "FirstDefaultWorkMode");
            Scribe_Defs.Look(ref SecondDefaultWorkMode, "SecondDefaultWorkMode");
            Scribe_Defs.Look(ref AdditionalDefaultWorkMode, "AdditionalDefaultWorkMode");
        }
    }
}
