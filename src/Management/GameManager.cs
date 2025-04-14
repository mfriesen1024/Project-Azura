using Godot;
using ProjectAzura.src.EngineObjects.Resources;
using ProjectAzura.src.Entity;
using ProjectAzura.src.UI;
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
        [Export] ShipConstructionData[] partyData;
        #region UIStuff
        [Export] PackedScene gameplayUI, loading, menus;
        public HUD HUD { get; private set; }
        public PlayerPhaseUI PlayerPhaseUI { get; private set; }
        Control mainMenu;
        Control loadScreen;
        Control winScreen;
        Control failScreen;
        #endregion

        public override void _Ready()
        {
            // Double check that we only have one GM.
            if (Instance == null) { Instance = this; }
            else { Free(); }

            InitEvents();
            InitSystems();
            InitMiscObjects();
            InitUI();

            // Use this to activate things when everything is initialized.
            Launch();
        }

        private void InitSystems()
        {
            GD.PrintErr(new NotImplementedException());
        }

        private void InitMiscObjects()
        {
            // Initialize party.
            Party = new Ship[partyData.Length];
            for (int i = 0; i < partyData.Length; i++) { Party[i] = partyData[i]; }
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
