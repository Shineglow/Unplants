namespace Unplants.Scripts.Data.InteractiveObjectsData.Plants
{
    public interface IPlantConfiguration : IGrowthStageToAnimationClip, IGrowthStageToSprite, IGrowthStageToColorChange
    {
        string InGameName { get; }
        EPlantSizeClass SizeClass { get; }
        float TimeToGrowth { get; }
        float GrowthSpeed { get; }
        int HarvestCount { get; }
    }
}