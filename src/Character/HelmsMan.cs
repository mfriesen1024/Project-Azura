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
            var actions = AvailableActions.ToList();
            actions.Add(ActionType.Attack);
            AvailableActions = actions.ToArray();
        }
    }
}
