using System;
using Unplants.Scripts.Data.InteractiveObjectsData.Plants;
using Unplants.Scripts.General.Extensions;

namespace Unplants.Scripts.Gameplay.Planting.Plants
{
    public class GrowthViewProgress
    {
        public bool Process<T>(
            IGrowthStageToData<IStageToData<T>> configuration,
            float progress,
            Action<T> action)
        {
            configuration.ThrowIfNull();
            action.ThrowIfNull();

            foreach (var colorToStage in configuration.StageToData)
            {
                if (progress >= colorToStage.GrowthStage)
                {
                    action(colorToStage.Data);
                    return true;
                }
            }

            return false;
        }
    }
}