using Godot;
using RPGSystem.Encounter;

namespace ProjectAzura.src.EngineObjects
{
    partial class Map : TileMapLayer
    {
        // The upper and lower bounds
        [Export(PropertyHint.None, "The -x -y corner of the map.")] Vector2I lbound;
        [Export(PropertyHint.None, "The -x -y corner of the map.")] Vector2I ubound;
        [Export(PropertyHint.ResourceType, "HazardTable")] HazardTable hazardTable;
        public Area InternalMapData { get; protected set; }


        public override void _Ready()
        {
            base._Ready();
        }
    }
}
