namespace Unplants.Scripts.Data.InteractiveObjectsData.Plants
{
    public interface IPlantsConfiguration
    {
        IPlantConfiguration GetPlantConfiguration(EPlant plant);
    }
}
