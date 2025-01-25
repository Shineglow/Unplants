using UnityEngine;

namespace Unplants.General.Systems.DragDropSystem
{
    public interface IDragDropItem
    {
        public Vector3 Position { get; set; }
        public Vector3 Scale { get; set; }
    }
}
