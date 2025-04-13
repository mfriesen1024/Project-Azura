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
            var actions = AvailableActions.ToList();
            actions.Add(ActionType.Attack);
            AvailableActions = actions.ToArray();
        }
    }
}
