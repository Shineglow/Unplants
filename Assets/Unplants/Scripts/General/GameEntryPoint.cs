using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Unplants.General.Systems.DragDropSystem;
using Unplants.General.Systems.EventSystemAbstraction;
using Unplants.Scripts.Gameplay.Planting.Plants;

namespace Unplants
{
    public class GameEntryPoint : MonoBehaviour
    {
        [SerializeField] private EventSystem eventSystem;
        [SerializeField] private Physics2DRaycaster physics2DRaycaster;

        private IRaycasterAbstraction<GameObject> _raycaster2D;
        private DragDropSystemBase _dragDropSystem;

        private List<PlantBaseView> draggablePlants;

        void Start()
        {
            _raycaster2D = new RaycasterAbstractionBase(eventSystem, physics2DRaycaster);
            _dragDropSystem = new DragDropSystemBase(_raycaster2D);
            foreach (var item in draggablePlants)
            {
                _dragDropSystem.Add(item);
            }
        }
    }
}
