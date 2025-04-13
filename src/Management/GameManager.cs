using Godot;
using ProjectAzura.src.Entity;
using System;

namespace ProjectAzura.src.Management
{
    /// <summary>
    /// Responsible for a lot of core functions including the UI.
    /// </summary>
    partial class GameManager : Node
    {
        public override void _Ready()
        {
            base._Ready();

            Ship.TurnStart = OnTurnStart;
        }

        private void OnTurnStart(Ship ship)
        {
            throw new NotImplementedException();
        }
    }
}
