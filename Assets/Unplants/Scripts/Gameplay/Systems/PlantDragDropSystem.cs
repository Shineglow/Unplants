using UnityEngine;
using Unplants.General.Systems.DragDropSystem;
using Unplants.General.Systems.EventSystemAbstraction;

namespace Unplants.Scripts.Gameplay.Systems
{
    public class PlantDragDropSystem : DragDropSystemBase<IDragListener<IDragDropItem>, IDragDropItem>
    {
        private Camera _camera;

        public PlantDragDropSystem(IRaycasterAbstraction<GameObject> raycaster, Camera camera) : base(raycaster)
        {
            _camera = camera;
        }

        protected override void OnDrag(IDragDropItem item, IPointerData data)
        {
            MovingItem(item, data);
        }

        private void MovingItem(IDragDropItem item, IPointerData data)
        {
            var pointerWorldPosition = _camera.ScreenToWorldPoint(data.PointerScreenPos);
            pointerWorldPosition.z = 0;
            item.Transform.position += pointerWorldPosition - item.Transform.position;
        }
    }
}
