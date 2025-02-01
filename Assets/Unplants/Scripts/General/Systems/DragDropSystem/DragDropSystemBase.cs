using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Unplants.General.Systems.DragDropSystem
{
    public sealed class DragDropSystemBase : IDragDropSystem<IDragListener>
    {
        private Physics2DRaycaster _physics2DRaycaster;

        private HashSet<IDragListener> _items;
        private Vector2 _pointerDownPos;

        public DragDropSystemBase() 
        {
            _items = new HashSet<IDragListener>();
        }

        public void Add(IDragListener item)
        {
            _items.Add(item);
            item.PointerDown += OnPointerDown;
            item.PointerUp += OnPointerUp;
            item.DragBegin += OnDragBegin;
            item.Drag += OnDrag;
            item.DragEnd += OnDragEnd;
        }

        public void Remove(IDragListener item)
        {
            _items.Remove(item);
            item.PointerDown -= OnPointerDown;
            item.PointerUp -= OnPointerUp;
            item.DragBegin -= OnDragBegin;
            item.Drag -= OnDrag;
            item.DragEnd -= OnDragEnd;
        }

        private void OnDragEnd(IDragDropItem item, IPointerData data)
        {
            PointerEventData eventData = new(EventSystem.current);
            eventData.position = data.PointerPos;
            eventData.displayIndex = data.DisplayIndex;

            List<RaycastResult> raycastResults = new();
            _physics2DRaycaster.Raycast(eventData, raycastResults);

            if (raycastResults.Count == 0)
                return;

            for (int i = 0; i < raycastResults.Count; i++)
            {
                if(raycastResults[i].gameObject.TryGetComponent<IDropListener<IDragDropItem>>(out var listener))
                {
                    if (listener.Drop(item))
                    {
                        break;
                    }
                }
            }
        }

        private void OnDrag(IDragDropItem item, IPointerData data)
        {
            item.TransformAbstraction.position = data.PointerPos - _pointerDownPos;
        }

        private void OnDragBegin(IDragDropItem item, IPointerData data)
        {
            throw new NotImplementedException();
        }

        private void OnPointerUp(IDragDropItem item, IPointerData data)
        {
            
        }

        private void OnPointerDown(IDragDropItem item, IPointerData data)
        {
            _pointerDownPos = data.PointerPos;
        }
    }
}
