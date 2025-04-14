using Godot;
using RPGSystem.Util;
using System;
using System.Linq;

namespace ProjectAzura.src.EngineObjects.Resources
{
    /// <summary>
    /// A table detailing what tile IDs should convert to what hazards, and performs that conversion. Note that an unlisted id is considered impassible.
    /// </summary>
    [GlobalClass]
    internal partial class HazardTable:Resource
    {
        [Export] Vector2[] normal;
        [Export] Vector2[] difficult;
        [Export] Vector2[] impassible;
        [Export] Vector2[] damaging;

        Vector2I[] Normal { get => Cast(normal); }
        Vector2I[] Difficult { get => Cast(difficult); }
        Vector2I[] Impassible { get => Cast(impassible); }
        Vector2I[] Damaging { get => Cast(damaging); }

        private Vector2I[] Cast(Vector2[] old)
        {
            Vector2I[] newArray = new Vector2I[old.Length];
            for (int i = 0; i < old.Length; i++) {
                Vector2 data = old[i];
                newArray[i] = new((int)data.X, (int)data.Y);
            }
            return newArray;
        }

        public Tile IDToTile(Vector2I tileID)
        {
            HazardType hazard=HazardType.impassible;
            if (Normal.ToList().Contains(tileID)) { hazard = HazardType.none; }
            if (Difficult.ToList().Contains(tileID)) { hazard = HazardType.difficult; }
            // This is redundant, but just for clarity's sake, keep this around.
            // if (Impassible.ToList().Contains(tileID)) { hazard = HazardType.impassible; }
            if (Damaging.ToList().Contains(tileID)) { hazard = HazardType.damaging; }
            return new() { hazard = hazard };
        }
    }
}