using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using Unplants.General.Systems.DragDropSystem;

namespace Unplants.General.Systems.EventSystemAbstraction
{
    public sealed class RaycasterAbstractionBase : IRaycasterAbstraction<GameObject>
    {
        private Physics2DRaycaster _physics2DRaycaster;
        private PointerEventData _pointerEventData;

        public RaycasterAbstractionBase(EventSystem eventSystem, Physics2DRaycaster physics2DRaycaster)
        {
            _pointerEventData = new PointerEventData(eventSystem);
            _physics2DRaycaster = physics2DRaycaster;
        }

        public IEnumerable<GameObject> GetObjectUnderMouse(IPointerData data)
        {
            _pointerEventData.position = data.PointerPos;
            _pointerEventData.displayIndex = data.DisplayIndex;

            List<RaycastResult> raycastResults = new();
            _physics2DRaycaster.Raycast(_pointerEventData, raycastResults);

            return raycastResults.Select(i => i.gameObject);
        }
    }
}
