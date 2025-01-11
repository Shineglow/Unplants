using Assets.Unplants.Scripts.General.Types.Obsrvables;

namespace Assets.Unplants.Scripts.Gameplay.Planting
{
    internal interface IPlantModel
    {
        IObservableValue<string> Name { get; }
        IObservableValue<float> GrowthProgress { get; } // 0 .. 1, GrowthSpeed * GrowthTime / GrowthMax;
        IObservableValue<float> GrowthSpeed { get; } // > 0, not change while in progress
        IObservableValue<float> GrowthTime { get; } // > 0, not change while in progress
        IObservableValue<float> GrowthTimeLast { get; } // <= GrowthTime
        IObservableValue<float> GrowthMax { get; } // > 0, not changeI
    }
}
