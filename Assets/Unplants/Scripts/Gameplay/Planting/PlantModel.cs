using Assets.Unplants.Scripts.General.Types;
using Assets.Unplants.Scripts.General.Types.Obsrvables;
using System;

namespace Assets.Unplants.Scripts.Gameplay.Planting
{
    internal struct PlantModel : IPlantModel
    {
        public ObservableValue<string> NameSetter;
        public ObservableValue<float> GrowthProgressSetter;
        public ObservableValue<float> _growthSpeed;
        public ObservableValue<float> _growthTime;
        public ObservableValue<float> _growthTimeLast;
        public ObservableValue<float> _growthMax;

        IObservableValue<string> IPlantModel.Name => NameSetter;
        IObservableValue<float> IPlantModel.GrowthProgress => GrowthProgressSetter;
        IObservableValue<float> IPlantModel.GrowthSpeed => _growthSpeed;
        IObservableValue<float> IPlantModel.GrowthTime => _growthTime;
        IObservableValue<float> IPlantModel.GrowthTimeLast => _growthTimeLast;
        IObservableValue<float> IPlantModel.GrowthMax => _growthMax;

        public PlantModel(
            string name, 
            float growthProgress, 
            float growthSpeed, 
            float growthTime, 
            float growthTimeLast, 
            float growthMax)
        {
            NameSetter = new ObservableValue<string>(name);
            GrowthProgressSetter = new ObservableValue<float>(growthProgress);
            _growthSpeed = new ObservableValue<float>(growthSpeed);
            _growthTime = new ObservableValue<float>(growthTime);
            _growthTimeLast = new ObservableValue<float>(growthTimeLast);
            _growthMax = new ObservableValue<float>(growthMax);
        }
    }
}
