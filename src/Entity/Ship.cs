using Godot;
using RPGSystem.Entity;
using RPGSystem.Stats;
using RPGSystem.Systems;
using RPGSystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAzura.src.Entity
{
    internal class Ship : EntityBase
    {
        /// <summary>
        /// Called when our turn starts.
        /// </summary>
        public Action<Ship> TurnStart = GlobalEventSystem.DoNothing;

        public Ship(ShipStatController statController) : base(statController)
        {
        }

        public Ship(ShipStatController statController, CrewMember[] crew, int teamID) : base(statController)
        {
            Crew = crew;
            this.teamID = teamID;
        }

        public CrewMember[] Crew { get; protected set; }

        /// <summary>
        /// 0 is player team, anything else is its own team.
        /// </summary>
        public int teamID { get; protected set; }
        public Vector2S Location = new(0,0);
        public int defaultMovement;
        public bool hasMoved;

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
            Vector2S[] instructions = NavigationSystem.Instance.TryFindPath(Location,newLoc);
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
            throw new NotImplementedException();
        }

        void UseCrewTurn(CrewMember crewMember)
        {
            throw new NotImplementedException();
        }
    }
}
