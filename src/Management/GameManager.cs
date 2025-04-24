using Godot;
using KeystoneUtils.Logging;
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
        public static Logger MainLog = new Logger(true,true,"logs\\","main","log");
        public static GameManager Instance { get; private set; }
        public InitiativeSystem InitiativeSystem { get => CurrentMap.InitiativeSystem; }
        public Ship[] Party { get; private set; }
        [Export] ShipConstructionData[] partyData;

        public Map CurrentMap { get; private set; }
        [Export] PackedScene DemoScene;

        #region UIStuff
        CanvasLayer UIParent;
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
            if (Instance == null) { Instance = this; MainLog.WriteAll("Created GM."); }
            else { Free(); }

            InitEvents();
            InitMiscObjects();
            InitSystems();
            InitUI();

            // Use this to activate things when everything is initialized.
            Launch();
        }

        private void InitSystems()
        {
            MainLog.WriteAll("Initializing Systems.");
            CurrentMap = DemoScene.Instantiate() as Map;
            NavigationSystem.Instance = new();
            MainLog.WriteAll("Systems Initialized.");
        }

        private void InitMiscObjects()
        {
            // Initialize party.
            MainLog.WriteAll("Initializing Party.");
            Party = new Ship[partyData.Length];
            for (int i = 0; i < partyData.Length; i++) { Party[i] = partyData[i]; }
            MainLog.WriteAll("Party Initialized.");

            //AddChild(new InputDebugger());
        }

        private void InitUI()
        {
            MainLog.WriteAll("Initializing UI.");
            UIParent = new(); AddChild(UIParent);
            GameplayUI = gameplayUI.Instantiate() as Control;
            PlayerPhaseUI = GameplayUI.GetChild(0) as PlayerPhaseUI;
            HUD = GameplayUI.GetChild(1) as HUD;
            GameplayUI.RemoveChild(PlayerPhaseUI);

            GD.PrintErr(new NotImplementedException("Only Gameplay UI is implemented."));
            MainLog.WriteAll("UI Initialized.");
        }

        private void Launch()
        {
            MainLog.WriteAll("Launching.");
            AddChild(CurrentMap);
            UIParent.AddChild(GameplayUI);
            MainLog.WriteAll("Launch Complete.");
        }

        private void InitEvents()
        {
            Ship.TurnStart = OnTurnStart;
            Ship.TurnEnd = OnTurnEnd;
        }

        private void OnTurnStart(Ship ship)
        {
            MainLog.WriteAll("TurnStart Called.");
            PlayerPhaseUI.FocusedShip = ship;
            UIParent.AddChild(PlayerPhaseUI);
        }

        void OnTurnEnd()
        {
            MainLog.WriteAll("TurnEndCalled.");
            UIParent.RemoveChild(PlayerPhaseUI);
        }
    }
}
