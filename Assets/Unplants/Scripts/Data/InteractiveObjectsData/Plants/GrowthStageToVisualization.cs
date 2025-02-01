using System;
using UnityEngine;
using UnityEngine.Serialization;
using Unplants.Scripts.Data.ResourcesData;

namespace Unplants.Scripts.Data.InteractiveObjectsData.Plants
{
    [Serializable]
    public struct GrowthStageToSprite : IStageToData<IEnvironmentSpriteResource>
    {
        [field: SerializeField] public float GrowthStage { get; private set; }
        public IEnvironmentSpriteResource Data => environmentSpriteResourceSO;
        [SerializeField] private EnvironmentSpriteResourceSO environmentSpriteResourceSO;
    }

    [Serializable]
    public struct GrowthStageToAnimationClip : IStageToData<IEnvironmentAnimationResource>
    {
        [field: SerializeField] public float GrowthStage { get; private set; }
        public IEnvironmentAnimationResource Data => environmentAnimationResourceSO;
        [SerializeField] private EnvironmentAnimationResourceSO environmentAnimationResourceSO;
    }

    [Serializable]
    public struct GrowthStageToColorChange : IStageToData<Color>
    {
        [field: SerializeField] public float GrowthStage { get; private set; }
        public Color Data => color;
        [SerializeField] private Color color;
    }

    public interface IStageToData<T>
    {
        public float GrowthStage { get; }
        public T Data { get; }
    }
}