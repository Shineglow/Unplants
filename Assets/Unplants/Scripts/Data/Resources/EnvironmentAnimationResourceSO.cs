using UnityEngine;

namespace Assets.Unplants.Scripts.Data
{
    [CreateAssetMenu(fileName = "EnvironmentAnimationResourceSO", menuName = "Unplants/Resources/EnvironmentAnimationResourceSO")]
    public class EnvironmentAnimationResourceSO : ScriptableObject, IEnvironmentAnimationResource
    {
        [field: SerializeField] public Animation IdleAnimation { get; private set; }
    }
}
