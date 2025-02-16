using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Unplants.General.Systems.DragDropSystem;
using Unplants.General.Systems.EventSystemAbstraction;
using Unplants.Scripts.Gameplay.Planting.Plants;
using Unplants.Scripts.Gameplay.Systems;

namespace Unplants
{
    public class GameEntryPoint : MonoBehaviour
    {
        [SerializeField] private EventSystem eventSystem;
        [SerializeField] private Physics2DRaycaster physics2DRaycaster;

        private IRaycasterAbstraction<GameObject> _raycaster2D;
        private DragDropSystemBase<IDragListener<IDragDropItem>, IDragDropItem> _dragDropSystem;

        [SerializeField] private List<PlantBaseView> draggablePlants;

        void Start()
        {
            _raycaster2D = new RaycasterAbstractionBase(eventSystem, physics2DRaycaster);
            _dragDropSystem = new PlantDragDropSystem(_raycaster2D, Camera.main);
            
        }
    }
}
