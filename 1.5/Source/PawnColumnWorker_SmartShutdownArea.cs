using RimWorld;
using System.Linq;
using UnityEngine;
using Verse;

namespace SmartWorkMode
{
    public class PawnColumnWorker_SmartShutdownArea : PawnColumnWorker
    {
        public override void DoCell(Rect rect, Pawn pawn, PawnTable table)
        {
            MechanitorControlGroup group = pawn.GetMechControlGroup();
            if (group != null)
            {
                Text.Anchor = TextAnchor.MiddleCenter;
                Rect rect2 = rect;
                rect2.xMin += 3f;
                if (group.WorkMode == MechWorkModeDefOf.SmartWork)
                {
                    Area area = group.GetSmartShutdownArea(Find.CurrentMap);
                    Widgets.DrawRectFast(rect, area?.Color ?? Color.grey);
                    Widgets.Label(rect2, AreaUtility.AreaAllowedLabel_Area(area));
                    Text.Anchor = TextAnchor.UpperLeft;
                    if (Mouse.IsOver(rect))
                    {
                        AcceptanceReport canControlMechs = group.Tracker.CanControlMechs;
                        TipSignal tooltip = pawn.GetTooltip();
                        tooltip.text = "SmartWorkMode_ClickToChangeSmartShutdownArea".Translate();
                        if (!canControlMechs && !canControlMechs.Reason.NullOrEmpty())
                        {
                            tooltip.text += "\n\n" + ("DisabledCommand".Translate() + ": " + canControlMechs.Reason).Colorize(ColorLibrary.RedReadable);
                        }
                        TooltipHandler.TipRegion(rect, tooltip);
                        if (canControlMechs && Widgets.ButtonInvisible(rect))
                        {
                            Find.WindowStack.Add(new FloatMenu(Find.CurrentMap.areaManager.AllAreas.Where(a => a.AssignableAsAllowed()).Prepend(null).Select(a => new FloatMenuOption(AreaUtility.AreaAllowedLabel_Area(a), () =>
                            {
                                group.SetSmartShutdownArea(Find.CurrentMap, a);
                            }, a?.ColorTexture ?? BaseContent.GreyTex, Color.white)).ToList()));
                        }
                        Widgets.DrawHighlight(rect);
                    }
                }
                else
                {
                    Widgets.Label(rect2, "N/A");
                    TipSignal tooltip = pawn.GetTooltip();
                    tooltip.text = "SmartWorkMode_SetToSmartWorkModeToChange".Translate();
                    TooltipHandler.TipRegion(rect, tooltip);
                }
            }
        }

        public override bool CanGroupWith(Pawn pawn, Pawn other)
        {
            MechanitorControlGroup group = pawn.GetMechControlGroup();
            return group != null && other.GetMechControlGroup() == group;
        }
    }
}
