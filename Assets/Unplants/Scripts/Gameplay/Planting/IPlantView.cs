using UnityEngine;

namespace Unplants.Scripts.Gameplay.Planting
{
    public interface IPlantView
    {
        void SetView(Sprite sprite);
        void SetView(AnimationClip animation);
    }
}