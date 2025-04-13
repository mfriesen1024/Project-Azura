using Godot;
using RPGSystem.Stats;

namespace ProjectAzura.src.EngineObjects.Resources
{
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
}