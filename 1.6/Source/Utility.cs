using RimWorld;
using RimWorld.Planet;
using System.Collections.Generic;
using Verse;

namespace SmartWorkMode
{
    public static class Utility
    {
        public static readonly Dictionary<MechanitorControlGroup, Dictionary<Map, Area>> shutdownAreas = new Dictionary<MechanitorControlGroup, Dictionary<Map, Area>>();
        public static readonly Dictionary<MechanitorControlGroup, MoveableArea> shutdownAreasGravship = new Dictionary<MechanitorControlGroup, MoveableArea>();

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

        public static MoveableArea GetSmartShutdownAreaGravship(this MechanitorControlGroup group)
        {
            if (shutdownAreasGravship.ContainsKey(group))
            {
                return shutdownAreasGravship[group];
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

        public static void SetSmartShutdownAreaGravship(this MechanitorControlGroup group, MoveableArea area)
        {
            shutdownAreasGravship[group] = area;
        }

        public static void CopySmartShutdownAreaToMap(Area area, MoveableArea_Allowed areaGravship)
        {
            foreach (MechanitorControlGroup group in shutdownAreasGravship.Keys)
            {
                if (!group.GetSmartShutdownAreas().ContainsKey(area.Map) && group.GetSmartShutdownAreaGravship() == areaGravship)
                {
                    group.SetSmartShutdownArea(area.Map, area);
                }
            }
        }
    }
}
