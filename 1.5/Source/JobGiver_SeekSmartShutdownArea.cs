using RimWorld;
using Verse;
using Verse.AI;

namespace SmartWorkMode
{
    public class JobGiver_SeekSmartShutdownArea : ThinkNode_JobGiver
    {
        protected override Job TryGiveJob(Pawn pawn)
        {
            MechanitorControlGroup group = pawn.GetMechControlGroup();
            if (group != null)
            {
                Area area = group.GetSmartShutdownArea(pawn.Map);
                if (area != null && area.TrueCount > 0 && !area[pawn.Position])
                {
                    Region region = null;
                    RegionTraverser.BreadthFirstTraverse(pawn.GetRegion(RegionType.Set_Passable), (Region from, Region r) => r.Allows(TraverseParms.For(pawn), false), r => 
                    {
                        if (r.IsDoorway)
                        {
                            return false;
                        }
                        if (!r.IsForbiddenEntirely(pawn) && r.OverlapWith(area) != AreaOverlap.None)
                        {
                            region = r;
                            return true;
                        }
                        return false;
                    }, 9999);
                    if (region != null)
                    {
                        if (region.TryFindRandomCellInRegionUnforbidden(pawn, cell => area[cell] && pawn.CanReserve(cell), out IntVec3 c))
                        {
                            return JobMaker.MakeJob(JobDefOf.Goto, c);
                        }
                    }
                }
            }

            return null;
        }
    }
}
