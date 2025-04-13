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
        CrewMember[] crew { get { return ManualCast(); } }

        private CrewMember[] ManualCast()
        {
            CrewMember[] newArray = new CrewMember[crewData.Length];
            for (int i = 0; i < crewData.Length; i++) {
                newArray[i] = crewData[i];
            }
        }

        [Export] int teamID;

        public static implicit operator Ship(ShipConstructionData res)
        {
            return new Ship(new(res.stats, [], res.typeData), res.crew, res.teamID);
        }
    }
}
