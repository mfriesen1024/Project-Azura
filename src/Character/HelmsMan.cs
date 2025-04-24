using System.Linq;

namespace ProjectAzura.src.Character
{
    /// <summary>
    /// A crew member capable of moving the ship with a decent multiplier.
    /// </summary>
    class HelmsMan : CrewMember
    {
        public HelmsMan()
        {
            Init();
        }

        public HelmsMan(ActionType[] availableActions, float powerModifier) : base(availableActions, powerModifier)
        {
            Init();
        }

        private void Init()
        {
            var actions = AvailableActions.ToList();
            actions.Add(ActionType.Move);
            AvailableActions = actions.ToArray();

            PowerModifier *= 1.2f;
        }
    }
}
