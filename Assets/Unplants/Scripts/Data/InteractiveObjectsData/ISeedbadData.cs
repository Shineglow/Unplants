using UnityEngine;
using Unplants.Scripts.Data.ResourcesData;

namespace Unplants.Scripts.Data.InteractiveObjectsData
{
    public interface ISeedbadData
    {
        Vector2Int GridSize { get; }
        EPlantSizeClass PlantSizeClass { get; }
        int PlantingSitesCount { get; }
        IEnvironmentAnimationResource EnvironmentAnimationResource { get; }
        IEnvironmentSpriteResource EnvironmentSpriteResource { get; }
    }
}