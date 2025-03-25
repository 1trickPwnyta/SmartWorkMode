using RimWorld;
using Verse;
using Verse.AI;

namespace SmartWorkMode
{
    public class JobGiver_SmartShutdown : ThinkNode_JobGiver
    {
        protected override Job TryGiveJob(Pawn pawn)
        {
            Pawn_MechanitorTracker mechanitor = pawn.GetOverseer()?.mechanitor;
            if (mechanitor != null)
            {
                if (!SmartWorkModeSettings.RequireDormantGroup || mechanitor.controlGroups.Any(g => g.WorkMode == RimWorld.MechWorkModeDefOf.SelfShutdown))
                {
                    MechanitorControlGroup group = pawn.GetMechControlGroup();
                    if (group != null)
                    {
                        Area area = group.GetSmartShutdownArea(pawn.Map);
                        IntVec3 c = pawn.Position;
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
                                region.TryFindRandomCellInRegionUnforbidden(pawn, cell => area[cell], out c);
                            }
                        }
                        if (RCellFinder.TryFindNearbyMechSelfShutdownSpot(c, pawn, pawn.Map, out IntVec3 target, true))
                        {
                            Job job = JobMaker.MakeJob(JobDefOf.SelfShutdown, target);
                            job.forceSleep = true;
                            job.expiryInterval = 300;
                            job.checkOverrideOnExpire = true;
                            return job;
                        }
                    }
                }
            }

            return null;
        }
    }
}
