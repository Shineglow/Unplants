using System;
using UnityEngine;

namespace Unplants.General.Systems.DragDropSystem
{
    [RequireComponent(typeof(Collider))]
    public class DropListnerBaseMono : MonoBehaviour, IDropListener<IDragDropItem>
    {
        public event Action<IDragDropItem> ItemDropped;

        public bool Drop(IDragDropItem item)
        {
            ItemDropped?.Invoke(item);

            return true;
        }
    }
}
