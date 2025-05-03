using System.Collections.Generic;
using UnityEngine;
using Unplants.General.Systems.DragDropSystem;
using Unplants.Scripts.Data.InteractiveObjectsData.Plants;
using Unplants.Scripts.Gameplay.Planting.Plants;
using Unplants.Scripts.General.Systems.DI;

namespace Unplants.Scripts.General
{
    public class GameEntryPoint : MonoBehaviour
    {
        [BindingTarget] private DragDropSystemBase<IDragListener<IDragDropItem>, IDragDropItem> _dragDropSystem;
        [BindingTarget] private PlantsFactory _plantsFactory;
        private List<PlantBaseView> draggablePlants = new List<PlantBaseView>();

        public void CallOnStart()
        {
            (IPlantModel model, PlantViewModel viewModel) = _plantsFactory.GetPlant(EPlant.Tomato);
            _dragDropSystem.Add(viewModel);
        }
    }
}
