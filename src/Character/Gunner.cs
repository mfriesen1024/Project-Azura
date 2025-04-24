using System.Linq;

namespace ProjectAzura.src.Character
{
    /// <summary>
    /// A crew member capable of attacking, with a decent multiplier.
    /// </summary>
    class Gunner : CrewMember
    {
        public Gunner()
        {
            Init();
        }

        public Gunner(ActionType[] availableActions, float powerModifier) : base(availableActions, powerModifier)
        {
            Init();
        }

        private void Init()
        {
            var actions = AvailableActions.ToList();
            actions.Add(ActionType.Attack);
            AvailableActions = actions.ToArray();

            PowerModifier *= 1.2f;
        }
    }
}
