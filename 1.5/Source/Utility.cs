using RimWorld;
using System.Collections.Generic;
using Verse;

namespace SmartWorkMode
{
    public static class Utility
    {
        private static Dictionary<MechanitorControlGroup, Dictionary<Map, Area>> shutdownAreas = new Dictionary<MechanitorControlGroup, Dictionary<Map, Area>>();

        public static Dictionary<Map, Area> GetSmartShutdownAreas(this MechanitorControlGroup group)
        {
            if (shutdownAreas.ContainsKey(group))
            {
                return shutdownAreas[group];
            }

            return new Dictionary<Map, Area>();
        }

        public static Area GetSmartShutdownArea(this MechanitorControlGroup group, Map map)
        {
            if (shutdownAreas.ContainsKey(group))
            {
                if (shutdownAreas[group] != null && shutdownAreas[group].ContainsKey(map))
                {
                    return shutdownAreas[group][map];
                }
            }
            return null;
        }

        public static void SetSmartShutdownAreas(this MechanitorControlGroup group, Dictionary<Map, Area> areas)
        {
            shutdownAreas[group] = areas;
        }

        public static void SetSmartShutdownArea(this MechanitorControlGroup group, Map map, Area area)
        {
            if (!shutdownAreas.ContainsKey(group) || shutdownAreas[group] == null)
            {
                shutdownAreas[group] = new Dictionary<Map, Area>();
            }
            shutdownAreas[group][map] = area;
        }
    }
}
