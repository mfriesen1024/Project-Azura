using Godot;
using ProjectAzura.src.Character;
using ProjectAzura.src.Entity;
using ProjectAzura.src.Management;
using RPGSystem.Systems;
using RPGSystem.Util;
using System;
using System.Linq;

namespace ProjectAzura.src.UI
{
    public partial class PlayerPhaseUI : Control
    {
        Action<int> CrewSelected = GlobalEventSystem.DoNothing;

        internal Ship FocusedShip { get; set; }
        [ExportCategory("PrimaryNodes")]
        [Export] Camera2D camera;
        [Export] Sprite2D cursorMovableElement;
        [Export] ShipStatsDisplay currentShipDisplay, targetShipDisplay;
        [Export] Control actionButtonsParent, crewButtonsParent;
        [ExportCategory("ButtonNodes")]
        [Export] Button moveButton;
        [Export] Button attackButton;
        [Export] Button repairButton;
        [Export] Button braceButton;
        [Export] Button passButton;
        [Export] Button gunnerButton;
        [Export] Button helmsmanButton;
        [Export] Button officerButton;


        bool canMove;

        private const string up = "ui_up";
        private const string down = "ui_down";
        private const string left = "ui_left";
        private const string right = "ui_right";

        public override void _Ready()
        {
            base._Ready();
            moveButton.Pressed += Move;
            moveButton.Pressed += FocusCamera;
            attackButton.Pressed += Attack;
            attackButton.Pressed += FocusCamera;
            repairButton.Pressed += Repair;
            repairButton.Pressed += FocusCamera;
            braceButton.Pressed += Brace;
            braceButton.Pressed += FocusCamera;
            passButton.Pressed += Pass;

            gunnerButton.Pressed += delegate { CrewSelected(0); };
            helmsmanButton.Pressed += delegate { CrewSelected(1); };
            officerButton.Pressed += delegate { CrewSelected(2); };

            // These 2 need to be reparented frequently.
            RemoveChild(actionButtonsParent);
            RemoveChild(crewButtonsParent);

            // These need to not move within canvas layer.
            camera.Reparent(GameManager.Instance);
            cursorMovableElement.Reparent(GameManager.Instance);            
        }

        private void Pass()
        {
            FocusedShip.EndTurn();
        }

        private void Brace()
        {
            CrewSelected += InternalBrace;
            void InternalBrace(int t)
            {
                CrewMember cm = GetBestCrewmember(t);
                GD.PrintErr(new NotImplementedException("brace is not implemented."));
                CrewSelected -= InternalBrace;
            }
            RequestCrew(ActionType.Brace);
        }

        private void Repair()
        {
            CrewSelected += InternalRepair;
            void InternalRepair(int t)
            {
                CrewMember cm = GetBestCrewmember(t);
                GD.PrintErr(new NotImplementedException("repair is not implemented."));
                CrewSelected -= InternalRepair;
            }
            RequestCrew(ActionType.Repair);
        }

        private void Attack()
        {
            CrewSelected += InternalAttack;
            void InternalAttack(int t)
            {
                CrewMember cm = GetBestCrewmember(t);
                Ship target = (Ship)NavigationSystem.Instance.FindNearestFoe(false, ScaledV2ToV2S(Position));
                float dist = target.Sprite.Position.DistanceTo(Position);
                if (dist == 0)
                {
                    FocusedShip.Attack(target, cm);
                }
                else
                {
                    GD.Print($"Attempted to attack a target at {target.Sprite.Position}, we're at {Position}");
                }
                CrewSelected -= InternalAttack;
            }
            RequestCrew(ActionType.Attack);
        }

        private void Move()
        {
            Vector2S targetPos = ScaledV2ToV2S(cursorMovableElement.Position);
            CrewSelected += InternalMove;
            void InternalMove(int t)
            {
                CrewMember cm = GetBestCrewmember(t);
                FocusedShip.Move(targetPos, cm);
                CrewSelected -= InternalMove;
            }
            RequestCrew(ActionType.Move);
        }

        private void RequestCrew(ActionType at)
        {
            GD.PushWarning($"Very bad crew request performed. This should be fixed!!!");
            gunnerButton.Disabled = !new Gunner().AvailableActions.ToList().Contains(at);
            helmsmanButton.Disabled = !new HelmsMan().AvailableActions.ToList().Contains(at);
            officerButton.Disabled = !new Officer().AvailableActions.ToList().Contains(at);
            RemoveChild(actionButtonsParent);
            AddChild(crewButtonsParent);
        }

        static Vector2S ScaledV2ToV2S(Vector2 position)
        {
            return new((short)(position.X / 32), (short)(position.Y / 32));
        }

        static Vector2 V2SToScaledV2F(Vector2S vector)
        {
            return new Vector2(vector.x * 32, vector.y * 32);
        }

        CrewMember GetBestCrewmember(int type)
        {
            GD.PushWarning("GetBestCrewmember was called, currently this is going to get the first one. Please fix!!!");
            RemoveChild(crewButtonsParent);
            foreach (CrewMember cm in FocusedShip.Crew)
            {
                switch (type)
                {
                    case 0: if (cm is Gunner) { return cm; } break;
                    case 1: if (cm is HelmsMan) { return cm; } break;
                    case 2: if (cm is Officer) { return cm; } break;
                    default: throw new ArgumentException($"{type} is not a valid crewmember type index for GetBestCrewmember.");
                }
            }
            throw new InvalidOperationException();
        }

        public override void _EnterTree()
        {
            base._EnterTree();
            FocusCamera();
            UpdateAvailableActions();
        }

        // Focus camera when preparing a move or attack action.
        private void FocusCamera()
        {
            Vector2 focusedPosition = FocusedShip.Sprite.Position;
            camera.Position = focusedPosition;
            cursorMovableElement.Position = focusedPosition;
            canMove = true;
        }

        public override void _UnhandledKeyInput(InputEvent @event)
        {
            base._UnhandledKeyInput(@event);
            if (canMove)
            {
                if (Input.IsActionJustPressed(up)) { camera.Position += new Vector2(0, -32); }
                else if (Input.IsActionJustPressed(down)) { camera.Position += new Vector2(0, 32); }
                else if (Input.IsActionJustPressed(left)) { camera.Position += new Vector2(-32, 0); }
                else if (Input.IsActionJustPressed(right)) { camera.Position += new Vector2(32, 0); }
                else if (@event is InputEventKey iek)
                {
                    if (iek.Keycode == Key.Space || iek.Keycode == Key.Enter)
                    {
                        AddChild(actionButtonsParent);
                        canMove = false;
                    }
                }
                cursorMovableElement.Position = camera.Position;
            }
        }

        void UpdateAvailableActions()
        {
            // First disable everything.
            moveButton.Disabled = true;
            attackButton.Disabled = true;
            braceButton.Disabled = true;
            repairButton.Disabled = true;

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
                            case ActionType.Move: moveButton.Disabled = FocusedShip.hasMoved; break;
                            case ActionType.Attack: attackButton.Disabled = false; break;
                            case ActionType.Brace: braceButton.Disabled = false; break;
                            case ActionType.Repair: repairButton.Disabled = false; break;
                            default: GD.PrintErr(new NotImplementedException($"{at} is not implemented in playerphase ui")); break;
                        }
                    }
                }
            }

            //moveButton.GrabFocus();
        }
    }
}