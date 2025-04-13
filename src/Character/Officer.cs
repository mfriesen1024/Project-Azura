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
            var actions = AvailableActions.ToList();
            actions.Add(ActionType.Attack);
            actions.Add(ActionType.Move);
            AvailableActions = actions.ToArray();
        }
    }
}
