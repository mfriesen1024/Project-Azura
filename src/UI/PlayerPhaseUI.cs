using Godot;
using ProjectAzura.src.Entity;

namespace ProjectAzura.src.UI
{
    public partial class PlayerPhaseUI:Control
    {
        internal Ship FocusedShip { get; set; }
        [Export] Camera2D camera;
        [Export] Sprite2D cursorMovableElement;

        private const string up="up";
        private const string down="down";
        private const string left="left";
        private const string right="right";

        public override void _EnterTree()
        {
            base._EnterTree();

            camera.Position = FocusedShip.Sprite.Position;
        }

        public override void _UnhandledKeyInput(InputEvent @event)
        {
            base._UnhandledKeyInput(@event);

            if (Input.IsActionJustPressed(up)) { camera.Position += new Vector2(0, -32); }
            if (Input.IsActionJustPressed(down)) { camera.Position += new Vector2(0, 32); }
            if (Input.IsActionJustPressed(left)) { camera.Position += new Vector2(-32, 0); }
            if (Input.IsActionJustPressed(right)) { camera.Position += new Vector2(32, 0); }
        }
    }
}