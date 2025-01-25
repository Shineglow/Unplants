using System.Collections.Generic;

namespace Unplants.Scripts.Data.InteractiveObjectsData.Plants
{
    public interface IGrowthStageToData<T>
    {
        IReadOnlyList<T> StageToData { get; }
    }
}