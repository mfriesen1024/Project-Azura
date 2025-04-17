using Godot;
using ProjectAzura.src.Character;
using ProjectAzura.src.Entity;
using System;

namespace ProjectAzura.src.UI
{
    public partial class PlayerPhaseUI : Control
    {
        internal Ship FocusedShip { get; set; }
        [Export] Camera2D camera;
        [Export] Sprite2D cursorMovableElement;
        [Export] ShipStatsDisplay currentShip, targetShip;
        [Export] Button move, attack, repair, brace, pass;

        MoveMode moveMode;

        private const string up = "ui_up";
        private const string down = "ui_down";
        private const string left = "ui_left";
        private const string right = "ui_right";

        public override void _EnterTree()
        {
            base._EnterTree();
            FocusCamera();
            UpdateAvailableActions();
        }

        private void FocusCamera(MoveMode mode = MoveMode.view)
        {
            camera.Position = FocusedShip.Sprite.Position;
            moveMode = mode;
        }

        public override void _UnhandledKeyInput(InputEvent @event)
        {
            base._UnhandledKeyInput(@event);

            // TODO: this needs replacing with MoveCamera().
            if (Input.IsActionJustPressed(up)) { camera.Position += new Vector2(0, -32); }
            if (Input.IsActionJustPressed(down)) { camera.Position += new Vector2(0, 32); }
            if (Input.IsActionJustPressed(left)) { camera.Position += new Vector2(-32, 0); }
            if (Input.IsActionJustPressed(right)) { camera.Position += new Vector2(32, 0); }
        }

        // Implement better movement logic.
        void MoveCamera(Vector2 direction)
        {
            throw new NotImplementedException();
        }

        void UpdateAvailableActions()
        {
            // First disable everything.
            move.Disabled = true;
            attack.Disabled = true;
            brace.Disabled = true;
            repair.Disabled = true;

            // Check every crewmember that hasn't acted.
            foreach (CrewMember cm in FocusedShip.Crew)
            {
                if (!cm.HasActed)
                {
                    // Any action those crew members can take should be enabled.
                    foreach (ActionType at in cm.AvailableActions)
                    {
                        switch (at)
                        {
                            case ActionType.Move: move.Disabled = FocusedShip.hasMoved; break;
                            case ActionType.Attack: attack.Disabled = false; break;
                            case ActionType.Brace: brace.Disabled = false; break;
                            case ActionType.Repair: repair.Disabled = false; break;
                            default: GD.PrintErr(new NotImplementedException($"{at} is not implemented in playerphase ui")); break;
                        }
                    }
                }
            }

            move.GrabFocus();
        }
        enum MoveMode { view, move, attack}
    }
}