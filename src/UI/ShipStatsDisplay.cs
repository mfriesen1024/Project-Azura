using Godot;
using ProjectAzura.src.Entity;
using System;

namespace ProjectAzura.src.UI
{
    /// <summary>
    /// Will display stats of ships it highligts.
    /// </summary>
    internal partial class ShipStatsDisplay:Control
    {
        public void Enable(Ship ship, Node parent)
        {
            parent.AddChild(this);
            throw new NotImplementedException();
        }

        public void Disable()
        {
            GetParent().RemoveChild(this);
        }
    }
}