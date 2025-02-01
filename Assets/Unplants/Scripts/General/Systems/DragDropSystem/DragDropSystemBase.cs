using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unplants.General.Systems.DragDropSystem
{
    public sealed class DragDropSystemBase : IDragDropSystem<IDragDropListener>
    {
        private HashSet<IDragDropListener> _items;
        private Vector2 _pointerDownPos;

        public DragDropSystemBase() 
        {
            _items = new HashSet<IDragDropListener>();
        }

        public void Add(IDragDropListener item)
        {
            _items.Add(item);
            item.PointerDown += OnPointerDown;
            item.PointerUp += OnPointerUp;
            item.DragBegin += OnDragBegin;
            item.Drag += OnDrag;
            item.DragEnd += OnDragEnd;
        }

        public void Remove(IDragDropListener item)
        {
            _items.Remove(item);
            item.PointerDown += OnPointerDown;
            item.PointerUp += OnPointerUp;
            item.DragBegin += OnDragBegin;
            item.Drag += OnDrag;
            item.DragEnd += OnDragEnd;
        }

        private void OnDragEnd(IDragDropItem item, IPointerData data)
        {
            throw new NotImplementedException();
        }

        private void OnDrag(IDragDropItem item, IPointerData data)
        {
            item.TransformAbstraction.Position = data.PointerPos - _pointerDownPos;
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
