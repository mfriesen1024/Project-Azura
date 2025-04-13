using Godot;
using ProjectAzura.src.Character;
using ProjectAzura.src.Entity;
using System.Linq;

namespace ProjectAzura.src.EngineObjects.Resources
{
    /// <summary>
    /// A data converter class to allow storage as .tres files and conversion to RPGSystem objects.
    /// </summary>
    [GlobalClass]
    partial class ShipConstructionData : Resource
    {
        [Export] TypeModifierConstructor typeData;
        [Export] BaseStatsConstructor stats;
        [Export] CrewMemberData[] crewData;
#pragma warning disable CA2021 // Types are confirmed compatible.
        CrewMember[] crew { get { return (CrewMember[])crewData.Cast<CrewMember>(); } }
#pragma warning restore CA2021
        [Export] int teamID;

        public static implicit operator Ship(ShipConstructionData res)
        {
            return new Ship(new(res.stats, [], res.typeData), res.crew, res.teamID);
        }
    }
}
