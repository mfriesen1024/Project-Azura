using Godot;
using ProjectAzura.src.EngineObjects;
using ProjectAzura.src.EngineObjects.Resources;
using ProjectAzura.src.Entity;
using ProjectAzura.src.UI;
using RPGSystem.Systems;
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
        public InitiativeSystem InitiativeSystem { get; private set; }
        public Ship[] Party { get; private set; }
        [Export] ShipConstructionData[] partyData;

        public Map DemoMap { get; private set; }
        [Export] PackedScene DemoScene;

        #region UIStuff
        [Export] PackedScene gameplayUI, loading, menus;
        public HUD HUD { get; private set; }
        public PlayerPhaseUI PlayerPhaseUI { get; private set; }
        Control GameplayUI; // This enables parent adjustment for hiding outside of levels.
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
            GameplayUI = gameplayUI.Instantiate() as Control;
            PlayerPhaseUI = GameplayUI.GetChild(0) as PlayerPhaseUI;
            HUD = GameplayUI.GetChild(1) as HUD;

            GD.PrintErr(new NotImplementedException("Only Gameplay UI is implemented."));
        }

        private void Launch()
        {
            throw new NotImplementedException();
        }

        private void InitEvents()
        {
            Ship.TurnStart = OnTurnStart;
            Ship.TurnEnd = OnTurnEnd;
        }

        private void OnTurnStart(Ship ship)
        {
            PlayerPhaseUI.FocusedShip = ship;
            AddChild(PlayerPhaseUI);
        }

        void OnTurnEnd()
        {
            RemoveChild(PlayerPhaseUI);
        }
    }
}
