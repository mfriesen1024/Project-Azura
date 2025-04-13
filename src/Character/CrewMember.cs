namespace ProjectAzura.src.Character
{
    /// <summary>
    /// Represents a crew member character, and has references to any sprites needed for it.
    /// </summary>
    public class CrewMember : CharacterBase
    {
        /// <summary>
        /// Determines what this CrewMember can do.
        /// </summary>
        // Specialized classes will be needed to do more.
        // This is not static because we want to need a class instance to determine actions.
        public ActionType[] AvailableActions { get; protected set; } = [ActionType.Repair, ActionType.Brace];

        public bool HasActed;

        public CrewMember()
        {
        }

        public CrewMember(ActionType[] availableActions, float powerModifier)
        {
            if (availableActions != null && availableActions.Length > 0) { AvailableActions = availableActions; }
            PowerModifier = powerModifier;
        }

        /// <summary>
        /// How strong should this crew member's actions be? Impacts all actions but move.
        /// </summary>
        public float PowerModifier { get; protected set; } = 1;
    }
}