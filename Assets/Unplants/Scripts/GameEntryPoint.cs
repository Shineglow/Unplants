using System.Collections.Generic;
using UnityEngine;
using Unplants.General.Systems.DragDropSystem;
using Unplants.Scripts.Data.InteractiveObjectsData.Plants;
using Unplants.Scripts.Gameplay.Planting.Plants;

namespace Unplants.Scripts
{
    public class GameEntryPoint : MonoBehaviour
    {
        private DragDropSystemBase<IDragListener<IDragDropItem>, IDragDropItem> _dragDropSystem;
        private PlantsFactory _plantsFactory;
        private List<PlantBaseView> draggablePlants = new List<PlantBaseView>();

        public void CallOnStart()
        {
            (IPlantModel model, PlantViewModel viewModel) = _plantsFactory.GetPlant(EPlant.Tomato);
            _dragDropSystem.Add(viewModel);
        }
    }
}
