namespace Unplants.Scripts.Data.InteractiveObjectsData.Plants
{
    public interface IPlantConfiguration : IGrowthStageToAnimationClip, IGrowthStageToSprite, IGrowthStageToColorChange
    {
        EPlant PlantType { get; }
        string InGameName { get; }
        EPlantSizeClass SizeClass { get; }
        float TimeToGrowth { get; }
        float GrowthSpeed { get; }
        int HarvestCount { get; }
    }
}