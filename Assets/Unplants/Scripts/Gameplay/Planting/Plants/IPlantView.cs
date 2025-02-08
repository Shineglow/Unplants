using UnityEngine;
using Unplants.General.Systems.DragDropSystem;

namespace Unplants.Scripts.Gameplay.Planting.Plants
{
    public interface IPlantView : IDragDropItem, IDragListener
    {
        bool IsVisible { get; set; }
        void SetView(AnimationClip animationClip);
        void SetView(Sprite sprite);
        void SetView(Color color);
    }
}