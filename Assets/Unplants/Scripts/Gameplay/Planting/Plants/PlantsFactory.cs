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

        public IPlantModel GetPlant(EPlant plantType)
        {
            var plantConfig = _plantsConfiguration.GetPlantConfiguration(plantType);
            var view = Object.Instantiate(_viewPrefab);
            PlantModel plantModel = new PlantModel(
                plantConfig.InGameName, view.transform, 0, plantConfig.GrowthSpeed, plantConfig.TimeToGrowth, 0, 0);
            _ = new PlantViewModel(view, plantModel, plantConfig);

            return new PlantModel();
        }
    }
}
