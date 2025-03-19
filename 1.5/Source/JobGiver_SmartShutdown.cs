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
                    if (RCellFinder.TryFindNearbyMechSelfShutdownSpot(pawn.Position, pawn, pawn.Map, out IntVec3 c, true))
                    {
                        Job job = JobMaker.MakeJob(JobDefOf.SelfShutdown, c);
                        job.forceSleep = true;
                        job.expiryInterval = 300;
                        job.checkOverrideOnExpire = true;
                        return job;
                    }
                }
            }

            return null;
        }
    }
}
