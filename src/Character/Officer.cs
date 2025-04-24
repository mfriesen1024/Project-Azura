using System.Linq;

namespace ProjectAzura.src.Character
{
    /// <summary>
    /// Character that can do most things, but has a low power multiplier.
    /// </summary>
    class Officer : CrewMember
    {
        public Officer()
        {
            Init();
        }

        public Officer(ActionType[] availableActions, float powerModifier) : base(availableActions, powerModifier)
        {
            Init();
        }

        private void Init()
        {
            var actions = AvailableActions.ToList();
            actions.Add(ActionType.Attack);
            actions.Add(ActionType.Move);
            AvailableActions = actions.ToArray();
        }
    }
}
