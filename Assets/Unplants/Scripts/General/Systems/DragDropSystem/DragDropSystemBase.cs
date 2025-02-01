using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unplants.General.Systems.EventSystemAbstraction;

namespace Unplants.General.Systems.DragDropSystem
{
    public sealed class DragDropSystemBase : IDragDropSystem<IDragListener>
    {
        private HashSet<IDragListener> _items;
        private IRaycasterAbstraction<GameObject> _raycaster;
        private Vector2 _pointerDownPos;

        public DragDropSystemBase(IRaycasterAbstraction<GameObject> raycaster) 
        {
            _items = new HashSet<IDragListener>();
            _raycaster = raycaster;
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
            var raycastResults = _raycaster.GetObjectUnderMouse(data);

            if (raycastResults.Count() == 0)
                return;

            foreach (var go in raycastResults)
            {
                if(go.TryGetComponent<IDropListener<IDragDropItem>>(out var listener))
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
