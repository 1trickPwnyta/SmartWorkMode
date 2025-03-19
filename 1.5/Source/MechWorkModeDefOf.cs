using RimWorld;
using Verse;

namespace SmartWorkMode
{
    [DefOf]
    public static class MechWorkModeDefOf
    {
        static MechWorkModeDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(MechWorkModeDefOf));
        }

        public static MechWorkModeDef SmartWork;
    }
}
