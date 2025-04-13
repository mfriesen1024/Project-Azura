using Godot;
using ProjectAzura.src.Character;

namespace ProjectAzura.src.EngineObjects.Resources
{
    [GlobalClass]
    internal partial class CrewMemberData:CharacterDataBase
    {
        [Export] float power = 1;
        [Export] ActionType[] actions;

        public static implicit operator CrewMember(CrewMemberData res)
        {
            return new CrewMember(res.actions, res.power);
        }
    }
}