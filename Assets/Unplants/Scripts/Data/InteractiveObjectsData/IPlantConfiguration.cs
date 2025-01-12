using Unplants.Scripts.Data.ResourcesData;

namespace Unplants.Scripts.Data.InteractiveObjectsData
{
    public interface IPlantConfiguration
    {
        string InGameName { get; }
        EPlantSizeClass SizeClass { get; }
        float TimeToGrowth { get; }
        float GrowthSpeed { get; }
        int HarvestCount { get; }
        IEnvironmentAnimationResource EnvironmentAnimationResource { get; }
        IEnvironmentSpriteResource EnvironmentSpriteResource { get; }
    }
}