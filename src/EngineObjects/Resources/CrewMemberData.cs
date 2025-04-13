using Godot;
using ProjectAzura.src.Character;
using System.Linq;

namespace ProjectAzura.src.EngineObjects.Resources
{
    [GlobalClass]
    internal partial class CrewMemberData:CharacterDataBase
    {
        [Export] CrewMemberType type;
        [Export] float power = 1;
        [Export] int[] actions = [];

        public static implicit operator CrewMember(CrewMemberData res)
        {
            return new CrewMember((ActionType[])res.actions.Cast<ActionType>(), res.power);
        }

        public enum CrewMemberType
        {
            Gunner,
            HelmsMan,
            Officer,
        }
    }
}