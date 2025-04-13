using Godot;
using RPGSystem.Stats;

namespace ProjectAzura.src.EngineObjects.Resources
{
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
}