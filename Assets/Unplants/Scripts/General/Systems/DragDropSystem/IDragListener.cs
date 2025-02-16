using System;

namespace Unplants.General.Systems.DragDropSystem
{
    public interface IDragListener<T>
    {
        event Action<T, IPointerData> PointerDown;
        event Action<T, IPointerData> PointerUp;
        event Action<T, IPointerData> DragBegin;
        event Action<T, IPointerData> Drag;
        event Action<T, IPointerData> DragEnd;
    }
}
