using Godot;
using RPGSystem.Util;
using System.Linq;

namespace ProjectAzura.src.EngineObjects.Resources
{
    /// <summary>
    /// A table detailing what tile IDs should convert to what hazards, and performs that conversion. Note that an unlisted id is considered impassible.
    /// </summary>
    [GlobalClass]
    internal partial class HazardTable:Resource
    {
        [Export] int[] normal;
        [Export] int[] difficult;
        [Export] int[] impassible;
        [Export] int[] damaging;

        public Tile IDToTile(int tileID)
        {
            HazardType hazard=HazardType.impassible;
            if (normal.ToList().Contains(tileID)) { hazard = HazardType.none; }
            if (difficult.ToList().Contains(tileID)) { hazard = HazardType.difficult; }
            // This is redundant, but just for clarity's sake, keep this around.
            // if (impassible.ToList().Contains(tileID)) { hazard = HazardType.impassible; }
            if (damaging.ToList().Contains(tileID)) { hazard = HazardType.damaging; }
            return new() { hazard = hazard };
        }
    }
}