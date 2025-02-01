using System.Collections.Generic;
using Unplants.General.Systems.DragDropSystem;

namespace Unplants.General.Systems.EventSystemAbstraction
{
    public interface IRaycasterAbstraction<T>
    {
        IEnumerable<T> GetObjectUnderMouse(IPointerData pointerData);
    }
}
