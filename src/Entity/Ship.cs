using Godot;
using ProjectAzura.src.Character;
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
        InitiativeSystem initiativeSystem;

        /// <summary>
        /// Called when our turn starts.
        /// </summary>
        public Action<Ship> TurnStart = GlobalEventSystem.DoNothing;

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

        public Ship(ShipStatController statController, CrewMember[] crew, int teamID) : base(statController)
        {
            Crew = crew;
            this.teamID = teamID;
        }

        public void Attack(EntityBase target, CrewMember crewMember)
        {
            Attack(target);

            UseCrewTurn(crewMember);
        }

        public override void Attack(EntityBase target)
        {
            throw new NotImplementedException();
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

        void ExecuteFoeTurn()
        {
            // Foes always move first.

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
        }

        void UseCrewTurn(CrewMember crewMember)
        {
            throw new NotImplementedException();
        }
    }
}
