using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unplants.General.Systems.EventSystemAbstraction;

namespace Unplants.General.Systems.DragDropSystem
{
    public abstract class DragDropSystemBase<T> : IDragDropSystem<T> where T : IDragListener
    {
        private HashSet<T> _items;
        protected IRaycasterAbstraction<GameObject> _raycaster { get; private set; }
        protected Vector2 _pointerDownPos;

        public DragDropSystemBase(IRaycasterAbstraction<GameObject> raycaster) 
        {
            _items = new HashSet<T>();
            _raycaster = raycaster;
        }

        public void Add(T item)
        {
            _items.Add(item);
            item.PointerDown += OnPointerDown;
            item.PointerUp += OnPointerUp;
            item.DragBegin += OnDragBegin;
            item.Drag += OnDrag;
            item.DragEnd += OnDragEnd;
            ItemAdded(item);
        }

        public void Remove(T item)
        {
            _items.Remove(item);
            item.PointerDown -= OnPointerDown;
            item.PointerUp -= OnPointerUp;
            item.DragBegin -= OnDragBegin;
            item.Drag -= OnDrag;
            item.DragEnd -= OnDragEnd;
            ItemRemoved(item);
        }

        protected virtual void OnDragEnd(IDragDropItem item, IPointerData data)
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

        protected abstract void OnDrag(IDragDropItem item, IPointerData data);

        protected virtual void OnDragBegin(IDragDropItem item, IPointerData data) { }

        protected virtual void OnPointerUp(IDragDropItem item, IPointerData data) { }

        protected virtual void OnPointerDown(IDragDropItem item, IPointerData data) { }

        protected virtual void ItemAdded(T item) { }
        protected virtual void ItemRemoved(T item) { }
    }
}
