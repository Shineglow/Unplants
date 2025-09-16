using UnityEngine;
using Unplants.Scripts.Data.InteractiveObjectsData.Plants;

namespace Unplants.Scripts.Gameplay.Planting.Plants
{
    public class PlantsFactory
    {
        private IPlantsConfiguration _plantsConfiguration;
        private PlantBaseView _viewPrefab;

        public PlantsFactory(IPlantsConfiguration plantsConfiguration, PlantBaseView viewPrefab)
        {
            _plantsConfiguration = plantsConfiguration;
            _viewPrefab = viewPrefab;
        }

        public (IPlantModel model, PlantViewModel viewModel) GetPlant(EPlant plantType)
        {
            var plantConfig = _plantsConfiguration.GetPlantConfiguration(plantType);
            if (plantConfig == null)
            {
                Debug.LogError($"Miss configuration of {plantType}!");
                return (null, null);
            }
            
            var view = Object.Instantiate(_viewPrefab);
            PlantModel plantModel = new PlantModel(
                plantConfig.InGameName, view.transform, 0, plantConfig.GrowthSpeed, plantConfig.TimeToGrowth, 0, 0);
            var viewModel = new PlantViewModel(view, plantModel, plantConfig);

            return (plantModel, viewModel);
        }
    }
}
