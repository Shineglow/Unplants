using UnityEngine;
using Unplants.Scripts.Data.ResourcesData;

namespace Unplants.Scripts.Data.InteractiveObjectsData
{
    [CreateAssetMenu(fileName = "SeedbadData", menuName = "Unplants/Data/SeedbadData")]
    public class SeedbadData : ScriptableObject, ISeedbadData
    {
        [field: SerializeField] public Vector2Int GridSize { get; private set; }
        [field: SerializeField] public EPlantSizeClass PlantSizeClass { get; private set; }
        [field: SerializeField] public int PlantingSitesCount { get; private set; }
        public IEnvironmentAnimationResource EnvironmentAnimationResource => environmentResourceAnimation;
        public IEnvironmentSpriteResource EnvironmentSpriteResource => environmentResourceSprite;
        [SerializeField] private EnvironmentAnimationResourceSO environmentResourceAnimation;
        [SerializeField] private EnvironmentSpriteResourceSO environmentResourceSprite;
    }
}