using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

namespace Unplants.Scripts.Data.InteractiveObjectsData.Plants
{
    public class PlantsConfigurationSO : ScriptableObject, IPlantsConfiguration
    {
        [SerializeField] private List<PlantSO> _plants;
        private bool wasOrdered;

        public IPlantConfiguration GetPlantConfiguration(EPlant plant)
        {
            ReorderPlantsConfiguration();
            return _plants[(int)plant];
        }

        private void ReorderPlantsConfiguration()
        {
            if (wasOrdered) return;

            var count = (int)EPlant.Count;
            if(_plants.Count < count)
            {
                _plants.Add(null);
            }
            else if (_plants.Count > count)
            {
                for (int i = 0; i < _plants.Count; i++)
                {
                    if (_plants[i] == null)
                    {
                        _plants.RemoveAtSwapBack(i);
                    }
                }

                if (_plants.Count > count)
                {
                    Debug.LogError("plants has duplicates");
                }
            }

            for(int i = 0; i < _plants.Count; i++)
            {
                var plantCfg = _plants[i];

                int indexOfConfiguration = (int)plantCfg.PlantType;
                while (indexOfConfiguration != i)
                {
                    if(_plants[indexOfConfiguration] == null)
                    {
                        _plants[indexOfConfiguration] = _plants[i];
                        break;
                    }

                    var cfgTmp = _plants[indexOfConfiguration];
                    if(cfgTmp.PlantType == plantCfg.PlantType)
                    {
                        Debug.LogError("plants has duplicates");
                        break;
                    }
                    _plants[indexOfConfiguration] = plantCfg;
                    plantCfg = cfgTmp;
                    indexOfConfiguration = (int)plantCfg.PlantType;
                }
            }

            wasOrdered = true;
        }
    }
}
