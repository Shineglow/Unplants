using Unplants.General.Systems.DragDropSystem;

namespace Unplants.Scripts.Gameplay.Planting.PlantingSite
{
    public interface IPlantingSite <T>: IDropListener<T> where T : IDragDropItem
    {
        
    }
}
