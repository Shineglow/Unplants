using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Unplants.General.Systems.DragDropSystem;
using Unplants.General.Systems.EventSystemAbstraction;
using Unplants.Scripts.Data.InteractiveObjectsData.Plants;
using Unplants.Scripts.Gameplay.Planting.Plants;
using Unplants.Scripts.Gameplay.Systems;

namespace Unplants.Scripts
{
    public class GameEntryPoint : MonoBehaviour
    {
        private DragDropSystemBase<IDragListener<IDragDropItem>, IDragDropItem> _dragDropSystem;
        private PlantsFactory _plantsFactory;
        private List<PlantBaseView> draggablePlants = new List<PlantBaseView>();

        [SerializeField] private PlantsConfigurationSO plantsConfiguration;
        [SerializeField] private PlantBaseView plantView;
        [SerializeField] private Physics2DRaycaster unityRaycaster;
        private RaycasterAbstractionBase _raycasterAbstraction;

        private void Start()
        {
            CallOnStart();
        }

        public void CallOnStart()
        {
            _plantsFactory = new PlantsFactory(plantsConfiguration, plantView);
            _raycasterAbstraction = new RaycasterAbstractionBase(EventSystem.current, unityRaycaster);
            _dragDropSystem = new PlantDragDropSystem(_raycasterAbstraction, Camera.main);
            
            (IPlantModel model, PlantViewModel viewModel) = _plantsFactory.GetPlant(EPlant.Tomato);
            _dragDropSystem.Add(viewModel);
        }
    }
}
