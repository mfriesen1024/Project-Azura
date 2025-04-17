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

        private const string up = "ui_up";
        private const string down = "ui_down";
        private const string left = "ui_left";
        private const string right = "ui_right";

        public override void _EnterTree()
        {
            base._EnterTree();

            camera.Position = FocusedShip.Sprite.Position;
            UpdateAvailableActions();
        }

        public override void _UnhandledKeyInput(InputEvent @event)
        {
            base._UnhandledKeyInput(@event);

            if (Input.IsActionJustPressed(up)) { camera.Position += new Vector2(0, -32); }
            if (Input.IsActionJustPressed(down)) { camera.Position += new Vector2(0, 32); }
            if (Input.IsActionJustPressed(left)) { camera.Position += new Vector2(-32, 0); }
            if (Input.IsActionJustPressed(right)) { camera.Position += new Vector2(32, 0); }
        }

        void UpdateAvailableActions()
        {
            // First disable everything.
            move.Disabled = true;
            attack.Disabled = true;
            brace.Disabled = true;
            repair.Disabled = true;

            foreach (CrewMember cm in FocusedShip.Crew)
            {
                if (!cm.HasActed)
                {
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
        }
    }
}