using UnityEngine;

namespace Unplants.General.Systems.DragDropSystem
{
    public struct PointerData : IPointerData
    {
        public Vector2 PointerPos { get; set; }
        public int DisplayIndex { get; set; }
    }
}
