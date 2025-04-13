using Godot;
using RPGSystem.Stats;

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

        public static implicit operator ShipStatController(ShipConstructionData res)
        {
            return new(res.stats, [], res.typeData);
        }
    }
}
