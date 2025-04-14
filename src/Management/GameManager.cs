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
        /// <summary>
        /// The singleton instance of our GM.
        /// </summary>
        public static GameManager Instance { get; private set; }
        public Ship[] Party { get; private set; }

        public override void _Ready()
        {
            // Double check that we only have one GM.
            if (Instance == null) { Instance = this; }
            else { Free(); }

            InitEvents();
            InitUI();

            // Use this to activate things when everything is initialized.
            Launch();
        }

        private void InitUI()
        {
            throw new NotImplementedException();
        }

        private void Launch()
        {
            throw new NotImplementedException();
        }

        private void InitEvents()
        {
            Ship.TurnStart = OnTurnStart;
        }

        private void OnTurnStart(Ship ship)
        {
            throw new NotImplementedException();
        }
    }
}
