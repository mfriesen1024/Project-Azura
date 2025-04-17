using Godot;
using ProjectAzura.src.Entity;

namespace ProjectAzura.src.UI
{
    public partial class PlayerPhaseUI:Control
    {
        internal Ship FocusedShip { get; set; }
        [Export] Camera2D camera;

        public override void _EnterTree()
        {
            base._EnterTree();

            camera.Position = FocusedShip.Sprite.Position;
        }
    }
}