using UnityEngine;
using Unplants.Scripts.Gameplay.Planting.Plants;

namespace Unplants.Scripts.Gameplay.Planting.PlantingSite
{
    public class PlantingSiteBase : MonoBehaviour, IPlantingSite<IPlantModel>
    {
        public bool Drop(IPlantModel item)
        {
            return true;
        }
    }
}
