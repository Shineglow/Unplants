using UnityEngine;
using Unplants.Scripts.General.Extensions;

namespace Unplants.Scripts.Gameplay.Planting.Plants
{
    public class PlantBaseView : MonoBehaviour, IPlantView
    {
        [SerializeField] private SpriteRenderer mainSprite;
        [SerializeField] private new Animation animation;

        public bool IsVisible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        private void Awake()
        {
            mainSprite.ThrowIfNull();
            animation.ThrowIfNull();
        }

        public void SetView(Sprite sprite)
        {
            sprite.ThrowIfNull();
            
            mainSprite.sprite = sprite;
        }

        public void SetView(AnimationClip animationClip)
        {
            animation.ThrowIfNull();

            animation.clip = animationClip;
        }
    }
}