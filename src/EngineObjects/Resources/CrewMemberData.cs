using Godot;
using KeystoneUtils.Logging;
using ProjectAzura.src.Character;
using ProjectAzura.src.Management;
using System;
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
            ActionType[] availableActions = (ActionType[])res.actions.Cast<ActionType>();
            CrewMember cm;
            switch (res.type)
            {
                case CrewMemberType.Gunner: cm = new Gunner(availableActions,res.power); break;
                case CrewMemberType.HelmsMan: cm = new HelmsMan(availableActions, res.power);break;
                case CrewMemberType.Officer: cm = new Officer(availableActions, res.power); break;
                default: GameManager.MainLog.WriteAll($"{new NotImplementedException($"Type {res.type} is not implemented in crew generator.")}", LogLevel.warn);
                    cm = new CrewMember(availableActions, res.power);break;
            }
            return cm;
        }

        public enum CrewMemberType
        {
            Gunner,
            HelmsMan,
            Officer,
            Other,
        }
    }
}