using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unplants.General.Systems.EventSystemAbstraction;

namespace Unplants.General.Systems.DragDropSystem
{
    public abstract class DragDropSystemBase<T1, T2> : IDragDropSystem<T1, T2> where T1 : IDragListener<T2> where T2 : IDragDropItem
    {
        private HashSet<T1> _items;
        protected IRaycasterAbstraction<GameObject> _raycaster { get; private set; }
        protected Vector2 _pointerDownPos;

        public DragDropSystemBase(IRaycasterAbstraction<GameObject> raycaster) 
        {
            _items = new HashSet<T1>();
            _raycaster = raycaster;
        }

        public void Add(T1 item)
        {
            _items.Add(item);
            item.PointerDown += OnPointerDown;
            item.PointerUp += OnPointerUp;
            item.DragBegin += OnDragBegin;
            item.Drag += OnDrag;
            item.DragEnd += OnDragEnd;
            ItemAdded(item);
        }

        public void Remove(T1 item)
        {
            _items.Remove(item);
            item.PointerDown -= OnPointerDown;
            item.PointerUp -= OnPointerUp;
            item.DragBegin -= OnDragBegin;
            item.Drag -= OnDrag;
            item.DragEnd -= OnDragEnd;
            ItemRemoved(item);
        }

        protected virtual void OnDragEnd(T2 item, IPointerData data)
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

        protected abstract void OnDrag(T2 item, IPointerData data);

        protected virtual void OnDragBegin(T2 item, IPointerData data) { }

        protected virtual void OnPointerUp(T2 item, IPointerData data) { }

        protected virtual void OnPointerDown(T2 item, IPointerData data) { }

        protected virtual void ItemAdded(T1 item) { }
        protected virtual void ItemRemoved(T1 item) { }
    }
}
