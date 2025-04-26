using Godot;
using ProjectAzura.src.Character;
using ProjectAzura.src.Entity;

namespace ProjectAzura.src.EngineObjects.Resources
{
    /// <summary>
    /// A data converter class to allow storage as .tres files and conversion to RPGSystem objects.
    /// </summary>
    [GlobalClass]
    partial class ShipConstructionData : Resource
    {
        [Export] PackedScene spritePrefab;
        [Export] TypeModifierConstructor typeData;
        [Export] BaseStatsConstructor stats;
        [Export] CrewMemberData[] crewData;
        [Export] Vector2I startLoc;
        CrewMember[] crew { get { return CastDataToFormattedArray(); } }

        private CrewMember[] CastDataToFormattedArray()
        {
            CrewMember[] newArray = new CrewMember[crewData.Length];
            for (int i = 0; i < crewData.Length; i++)
            {
                newArray[i] = crewData[i];
            }
            return newArray;
        }

        [Export] int teamID;

        public static implicit operator Ship(ShipConstructionData res)
        {
            return new Ship(new(res.stats, [], res.typeData), res.stats.Movement, res.crew, res.teamID, res.startLoc, res.spritePrefab);
        }
    }
}
