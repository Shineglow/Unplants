using UnityEngine;

namespace Unplants.Scripts.Gameplay.Planting.Plants
{
    public interface IPlantView
    {
        bool IsVisible { get; set; }
        void SetView(AnimationClip animationClip);
        void SetView(Sprite sprite);
    }
}