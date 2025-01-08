using Assets.Unplants.Scripts.Data;
using UnityEngine;

[CreateAssetMenu(fileName = "PlantSO", menuName = "Unplants/Data/PlantSO")]
public class PlantSO : ScriptableObject
{
    [field: SerializeField] public float TimeToGrowth { get; private set; }
    [field: SerializeField] public float GrowthSpeed { get; private set; }
    [field: SerializeField] public int HarvestCount { get; private set; }
    [field: SerializeField] public EnvironmentAnimationResourceSO EnvironmentResourceAnimation { get; private set; }
    [field: SerializeField] public EnvironmentSpriteResourceSO EnvironmentResourceSprite { get; private set; }
}
