using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using Unplants.Scripts.Gameplay.Planting.Plants;

namespace Unplants.Scripts.Data.InteractiveObjectsData.Plants
{
    [CreateAssetMenu(fileName = "PlantsConfiguration", menuName = "Unplants/Configuration/PlantsConfiguration")]
    public class PlantsConfigurationSO : ScriptableObject, IPlantsConfiguration
    {
        [field: SerializeField] public PlantBaseView Prefab { get; private set; }
        [SerializeField] private List<PlantSO> plants;

        public void Init()
        {
            ReorderPlantsConfiguration();
        }

        public IPlantConfiguration GetPlantConfiguration(EPlant plant)
        {
            return plants[(int)plant];
        }

        private void ReorderPlantsConfiguration()
        {
            var count = (int)EPlant.Count;
            if(plants.Count < count)
            {
                while(plants.Count < count)
                    plants.Add(null);
            }
            else if (plants.Count > count)
            {
                for (int i = 0; i < plants.Count; i++)
                {
                    if (plants[i] == null)
                    {
                        plants.RemoveAtSwapBack(i);
                    }
                }

                if (plants.Count > count)
                {
                    Debug.LogError("plants has duplicates");
                }
            }

            for(int i = 0; i < plants.Count; i++)
            {
                var plantCfg = plants[i];
                if (plantCfg == null || (int)plantCfg.PlantType == i) continue;

                if (plants[(int)plantCfg.PlantType]?.PlantType == plantCfg.PlantType)
                {
                    Debug.LogError("plants has duplicates");
                    continue;
                }
                
                plants[i] = plants[(int)plantCfg.PlantType];
                plants[(int)plantCfg.PlantType] = plantCfg;
            }
        }
    }
}
