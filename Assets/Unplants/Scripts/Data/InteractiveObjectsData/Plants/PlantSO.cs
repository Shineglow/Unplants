using System.Collections.Generic;
using UnityEngine;

namespace Unplants.Scripts.Data.InteractiveObjectsData.Plants
{
    [CreateAssetMenu(fileName = "PlantSO", menuName = "Unplants/Data/PlantSO")]
    public class PlantSO : ScriptableObject, IPlantConfiguration
    {
        [field: SerializeField] public EPlant PlantType { get; set; }
        [field: SerializeField] public string InGameName { get; private set; }
        [field: SerializeField] public EPlantSizeClass SizeClass { get; private set; }
        [field: SerializeField] public float TimeToGrowth { get; private set; }
        [field: SerializeField] public float GrowthSpeed { get; private set; }
        [field: SerializeField] public int HarvestCount { get; private set; }
        
        [SerializeField] private List<GrowthStageToAnimationClip> growthStageToAnimationClip;
        [SerializeField] private List<GrowthStageToSprite> growthStageToSprite;
        [SerializeField] private List<GrowthStageToColorChange> growthStageToColorChange;
        IReadOnlyList<GrowthStageToAnimationClip> IGrowthStageToData<GrowthStageToAnimationClip>.StageToData => growthStageToAnimationClip;
        IReadOnlyList<GrowthStageToSprite> IGrowthStageToData<GrowthStageToSprite>.StageToData => growthStageToSprite;
        IReadOnlyList<GrowthStageToColorChange> IGrowthStageToData<GrowthStageToColorChange>.StageToData => growthStageToColorChange;
    }
}