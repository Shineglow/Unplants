using UnityEngine;

namespace Assets.Unplants.Scripts.Data
{
    [CreateAssetMenu(fileName = "EnvironmentSpriteResourceSO", menuName = "Unplants/Resources/EnvironmentSpriteResourceSO")]
    public class EnvironmentSpriteResourceSO : MonoBehaviour, IEnvironmentSpriteResource
    {
        [field: SerializeField] public Sprite Sprite { get; private set; }
    }
}
