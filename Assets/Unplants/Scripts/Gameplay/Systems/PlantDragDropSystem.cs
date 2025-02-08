using UnityEngine;
using Unplants.General.Systems.DragDropSystem;
using Unplants.General.Systems.EventSystemAbstraction;

namespace Unplants.Scripts.Gameplay.Systems
{
    public class PlantDragDropSystem : DragDropSystemBase<IDragListener>
    {
        private Camera _camera;

        public PlantDragDropSystem(IRaycasterAbstraction<GameObject> raycaster, Camera camera) : base(raycaster)
        {
            _camera = camera;
        }

        protected override void OnDrag(IDragDropItem item, IPointerData data)
        {
            _camera.WorldToViewportPoint(item.TransformAbstraction.position);
        }
    }
}
