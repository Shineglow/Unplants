using UnityEngine;

namespace Unplants.General.Systems.DragDropSystem
{
    public interface IPointerData
    {
        Vector2 PointerScreenPos { get; }
        Vector2 PointerDelta { get; }
        int DisplayIndex { get; }
    }
}
