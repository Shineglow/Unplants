using Unplants.Scripts.Gameplay.Planting.Plants;

namespace Unplants.Scripts.Gameplay.Planting.PlantingSite
{
    public interface IPlantingSiteFilter
    {
        bool IsPlanting(PlantingSiteData siteData, IPlantModel plantView);
    }
}
