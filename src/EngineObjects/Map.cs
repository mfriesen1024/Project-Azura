using Godot;
using ProjectAzura.src.EngineObjects.Resources;
using ProjectAzura.src.Entity;
using ProjectAzura.src.Management;
using RPGSystem.Encounter;
using RPGSystem.Util;
using System;

namespace ProjectAzura.src.EngineObjects
{
    partial class Map : TileMapLayer
    {
        // The upper bounds. We should only use positive coordinates so things don't break.
        [Export(PropertyHint.None, "The +x +y corner of the map.")] Vector2I ubound;
        [Export(PropertyHint.ResourceType, "HazardTable")] HazardTable hazardTable;
        public Area InternalMapData { get; protected set; }
        [Export] ShipConstructionData[] FoeData;

        public override void _Ready()
        {
            base._Ready();

            // Offset ubound by +1 to avoid offbyone errors
            ubound += Vector2I.One;

            Tile[,] tiles = new Tile[ubound.X, ubound.Y];

            for (int x = 0; x < ubound.X; x++)
            {
                for (int y = 0; y < ubound.Y; y++)
                {
                    try { tiles[x, y] = hazardTable.IDToTile(GetCellAlternativeTile(new(x, y))); }
                    // oob tiles are considered impassible, they can be ignored, but log it anyway.
                    catch (IndexOutOfRangeException ior) { GD.PushWarning(ior); }
                }
            }

            Ship[] foes = new Ship[FoeData.Length];
            for (int i = 0; i < FoeData.Length; i++)
            {
                foes[i] = FoeData[i];
            }

            InternalMapData = new() { Map = tiles, Party = GameManager.Party, FoeList = foes };
        }
    }
}
