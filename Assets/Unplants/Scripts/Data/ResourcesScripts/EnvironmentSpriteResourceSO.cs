using UnityEngine;

namespace Unplants.Scripts.Data.ResourcesData
{
    [CreateAssetMenu(fileName = "EnvironmentSpriteResourceSO", menuName = "Unplants/Resources/EnvironmentSpriteResourceSO")]
    public class EnvironmentSpriteResourceSO : ScriptableObject, IEnvironmentSpriteResource
    {
        [field: SerializeField] public Sprite Sprite { get; private set; }
    }
}
