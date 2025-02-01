using UnityEngine;

namespace Unplants.General.Systems.DragDropSystem
{
    [RequireComponent(typeof(Collider))]
    public class DropListnerBaseMono : MonoBehaviour, IDropListener<IDragDropItem>
    {
        public bool Drop(IDragDropItem item)
        {
            item.TransformAbstraction.parent = transform;
            item.TransformAbstraction.localPosition = Vector3.zero;
            item.TransformAbstraction.localScale = Vector3.one;

            return true;
        }
    }
}
