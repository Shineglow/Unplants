using System;

namespace Unplants.General.Systems.DragDropSystem
{
    public interface IDragDropListener
    {
        event Action<IDragDropItem, IPointerData> PointerDown;
        event Action<IDragDropItem, IPointerData> PointerUp;
        event Action<IDragDropItem, IPointerData> DragBegin;
        event Action<IDragDropItem, IPointerData> Drag;
        event Action<IDragDropItem, IPointerData> DragEnd;
    }
}
