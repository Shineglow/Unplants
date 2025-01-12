using Unplants.Scripts.Data.InteractiveObjectsData;

namespace Unplants.Scripts.Gameplay.Planting.Plants
{
    public class PlantViewModel : IPlantViewModel
    {
        private IPlantView _view;
        private IPlantModel _model;

        public PlantViewModel(IPlantView view, IPlantModel model, IPlantConfiguration configuration)
        {
            _view = view;
            _model = model;

            model.GrowthProgress.ValueChanged += OnGrowthProgressUpdated;
        }

        private void OnGrowthProgressUpdated(float newValue)
        {
            // switch actualValue to view asset or colorChange?
        }
    }
}