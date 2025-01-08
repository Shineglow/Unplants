using UnityEngine;
using Unplants.Scripts.Data.ResourcesData;

namespace Unplants.Scripts.Data.InteractiveObjectsData
{
    [CreateAssetMenu(fileName = "PlantSO", menuName = "Unplants/Data/PlantSO")]
    public class PlantSO : ScriptableObject, IPlantData
    {
        [field: SerializeField] public string InGameName { get; private set; }
        [field: SerializeField] public EPlantSizeClass SizeClass { get; private set; }
        [field: SerializeField] public float TimeToGrowth { get; private set; }
        [field: SerializeField] public float GrowthSpeed { get; private set; }
        [field: SerializeField] public int HarvestCount { get; private set; }
        public IEnvironmentAnimationResource EnvironmentAnimationResource => environmentResourceAnimation;
        public IEnvironmentSpriteResource EnvironmentSpriteResource => environmentResourceSprite;
        [SerializeField] private EnvironmentAnimationResourceSO environmentResourceAnimation;
        [SerializeField] private EnvironmentSpriteResourceSO environmentResourceSprite;
    }
}
