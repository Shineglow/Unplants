using System;
using UnityEngine;
using Unplants.Scripts.General.Types.Observables;

namespace Unplants.Scripts.Gameplay.Planting.Plants
{
    [Serializable]
    public struct PlantModel : IPlantModel
    {
        [SerializeField] public ObservableValue<string> NameSetter;
        [SerializeField] public ObservableValue<float> GrowthProgressSetter;
        [SerializeField] public ObservableValue<float> _growthSpeed;
        [SerializeField] public ObservableValue<float> _growthTime;
        [SerializeField] public ObservableValue<float> _growthTimeLast;
        [SerializeField] public ObservableValue<float> _growthMax;

        IObservableValue<string> IPlantModel.Name => NameSetter;
        IObservableValue<float> IPlantModel.GrowthProgress => GrowthProgressSetter;
        IObservableValue<float> IPlantModel.GrowthSpeed => _growthSpeed;
        IObservableValue<float> IPlantModel.GrowthTime => _growthTime;
        IObservableValue<float> IPlantModel.GrowthTimeLast => _growthTimeLast;
        IObservableValue<float> IPlantModel.GrowthMax => _growthMax;

        public Transform Transform { get; }

        public PlantModel(
            string name,
            Transform viewTransform,
            float growthProgress,
            float growthSpeed,
            float growthTime,
            float growthTimeLast,
            float growthMax)
        {
            NameSetter = new ObservableValue<string>(name);
            Transform = viewTransform;
            GrowthProgressSetter = new ObservableValue<float>(growthProgress);
            _growthSpeed = new ObservableValue<float>(growthSpeed);
            _growthTime = new ObservableValue<float>(growthTime);
            _growthTimeLast = new ObservableValue<float>(growthTimeLast);
            _growthMax = new ObservableValue<float>(growthMax);
        }
    }
}