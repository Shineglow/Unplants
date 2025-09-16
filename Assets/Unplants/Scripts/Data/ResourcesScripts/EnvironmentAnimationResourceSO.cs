using UnityEngine;

namespace Unplants.Scripts.Data.ResourcesData
{
    [CreateAssetMenu(fileName = "EnvironmentAnimationResourceSO", menuName = "Unplants/Resources/EnvironmentAnimationResourceSO")]
    public class EnvironmentAnimationResourceSO : ScriptableObject, IEnvironmentAnimationResource
    {
        [field: SerializeField] public AnimationClip IdleAnimation { get; private set; }
    }
}
