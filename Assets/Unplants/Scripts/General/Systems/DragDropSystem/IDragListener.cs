using System;

namespace Unplants.General.Systems.DragDropSystem
{
    public interface IDragListener
    {
        event Action<IDragDropItem, IPointerData> PointerDown;
        event Action<IDragDropItem, IPointerData> PointerUp;
        event Action<IDragDropItem, IPointerData> DragBegin;
        event Action<IDragDropItem, IPointerData> Drag;
        event Action<IDragDropItem, IPointerData> DragEnd;
    }
}
