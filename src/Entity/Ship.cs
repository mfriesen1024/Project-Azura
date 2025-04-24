using Godot;
using KeystoneUtils.Logging;
using ProjectAzura.src.Character;
using ProjectAzura.src.Management;
using RPGSystem.Entity;
using RPGSystem.Stats;
using RPGSystem.Systems;
using RPGSystem.Util;
using System;
using System.Linq;

namespace ProjectAzura.src.Entity
{
    internal class Ship : EntityBase
    {
        // We somehow need a reference to the initiative system. How we get it? Not sure.
        InitiativeSystem initiativeSystem { get => GameManager.Instance.InitiativeSystem; }

        /// <summary>
        /// Called when this ship's turn starts, if its under player control.
        /// </summary>
        public static Action<Ship> TurnStart = GlobalEventSystem.DoNothing;

        /// <summary>
        /// Called when a player ship ends their turn.
        /// </summary>
        public static Action TurnEnd = GlobalEventSystem.DoNothing;

        /// <summary>
        /// A reference to our sprite engine side.
        /// </summary>
        public Sprite2D Sprite { get; protected set; }

        /// <summary>
        /// Array of crew members on board the ship.
        /// </summary>
        public CrewMember[] Crew { get; protected set; }

        /// <summary>
        /// 0 is player team, anything else is its own team.
        /// </summary>
        public int teamID { get; protected set; }

        public int defaultMovement;
        public bool hasMoved;

        public Ship(ShipStatController statController, CrewMember[] crew, int teamID, Vector2I location, PackedScene spritePrefab) : base(statController)
        {
            Crew = crew;
            this.teamID = teamID;

            short x = (short)location.X;
            short y = (short)location.Y;
            Location = new(x, y);

            Sprite = spritePrefab.Instantiate() as Sprite2D;
            UpdateSpriteLocation(); // This needs to be instant as we're instantiating things.
        }

        public void UpdateSpriteLocation()
        {
            Sprite.Position = new(Location.x * 32, Location.y * 32);
            GD.PushWarning("Used an instant location setter! Make sure you know what you're doing!");
        }

        public void Attack(EntityBase target, CrewMember crewMember)
        {
            Attack(target);

            UseCrewTurn(crewMember);
        }

        public override void Attack(EntityBase target)
        {
            GameManager.MainLog.WriteAll($"{new NotImplementedException()}", LogLevel.error);
        }

        public void Move(Vector2S newLoc, CrewMember crewMember)
        {
            // Check that the location is valid, then move the ship there.
            Vector2S[] instructions = NavigationSystem.Instance.TryFindPath(Location, newLoc);
            if (instructions.Length > 0 && instructions.Length < defaultMovement)
            {
                foreach (Vector2S instruction in instructions)
                {
                    // put animation code here.
                    GD.PrintErr(new NotImplementedException("Animation not implemented."));
                }
                hasMoved = true;

                UseCrewTurn(crewMember);
            }
        }

        public void Repair(CrewMember crewMember)
        {
            UseCrewTurn(crewMember);
        }

        public override void BeginTurn(out bool shouldHalt)
        {
            // We should not pause for player input if the ship isn't under player control.
            shouldHalt = teamID != 0;

            // Handle turn deferring.
            if (shouldHalt) { TurnStart(this); }
            else { ExecuteFoeTurn(); }
        }

        public void EndTurn()
        {
            TurnEnd();
            initiativeSystem.ResumeIteration();
        }

        void ExecuteFoeTurn()
        {
            // Foes always move before taking other actions.

            // Get a target and path.
            EntityBase target = NavigationSystem.Instance.FindNearestFoe(false, Location);
            Vector2S[] path = NavigationSystem.Instance.TryFindPath(Location, target.Location);
            // Ensure we don't try to move onto the target by giving an idle instruction. This should be revised later.
            if (path.Length > 0) { path[path.Length - 1] = new(0, 0); }

            // Get new location.
            Vector2S newLoc = Location;
            foreach (Vector2S instruction in path) { newLoc += instruction; }

            // If we dont have a suitable helmsman, we dont end up doing anything.
            foreach (CrewMember crewMember in Crew)
            {
                if (crewMember.AvailableActions.ToList().Contains(ActionType.Move))
                {
                    Move(newLoc, crewMember);
                }
            }

            // If our locations match, we're next to our target, open fire!
            if (newLoc == Location)
            {
                foreach (CrewMember crewMember in Crew)
                {
                    // If a crewmember can attack, do it.
                    if (crewMember.AvailableActions.ToList().Contains(ActionType.Attack) && !crewMember.HasActed)
                    {
                        Attack(target, crewMember);
                    }
                }
            }

            // Everyone else should play defensively.
            foreach (CrewMember crewMember in Crew)
            {
                if (!crewMember.HasActed) { Repair(crewMember); }
            }
        }

        void UseCrewTurn(CrewMember crewMember)
        {
            crewMember.HasActed = true;
        }
    }
}
