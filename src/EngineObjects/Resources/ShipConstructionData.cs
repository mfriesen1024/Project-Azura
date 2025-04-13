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

        [GlobalClass]
        public partial class TypeModifierConstructor : Resource
        {
            [Export] float trueDamage;
            [Export] float explosive;
            [Export] float piercing;
            [Export] float laser;

            public static implicit operator TypeModifierData(TypeModifierConstructor res)
            {
                return new TypeModifierData(res.laser, res.piercing, res.explosive, res.trueDamage);
            }
        }

        [GlobalClass]
        public partial class BaseStatsConstructor : Resource
        {
            [Export] short hull;
            [Export] short wepPower;
            [Export] short speed;

            public static implicit operator BaseStats(BaseStatsConstructor res)
            {
                return new BaseStats(res.hull, res.wepPower, res.speed);
            }
        }

        public static implicit operator ShipStatController(ShipConstructionData res)
        {
            return new(res.stats, [], res.typeData);
        }
    }
}
