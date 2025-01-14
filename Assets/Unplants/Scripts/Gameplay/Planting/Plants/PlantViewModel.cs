using System;
using System.Collections.Generic;
using Unplants.Scripts.Data.InteractiveObjectsData.Plants;
using Unplants.Scripts.Data.ResourcesData;
using Unplants.Scripts.General.Extensions;

namespace Unplants.Scripts.Gameplay.Planting.Plants
{
    public class PlantViewModel : IPlantViewModel
    {
        private IPlantView _view;
        private IPlantModel _model;
        private IPlantConfiguration _configuration;

        private readonly List<Action<IPlantConfiguration, float>> _actions;
        private readonly GrowthViewProgress _growthViewProgress;

        public PlantViewModel(IPlantView view, IPlantModel model, IPlantConfiguration configuration)
        {
            view.ThrowIfNull();
            model.ThrowIfNull();
            configuration.ThrowIfNull();

            _view = view;
            _model = model;
            _configuration = configuration;

            _growthViewProgress = new();
            model.GrowthProgress.ValueChanged += OnGrowthProgressUpdated;
            _actions = new()
            {
                (clip, progress) => ProcessAnimationClip(clip.StageToData, progress),
                (cfg, progress) => ),
                (cfg, progress) => growthViewProgress.Process((IStageToData<IEnvironmentAnimationResource>)cfg, progress, () => {
                    
                }),
                (cfg, progress) => growthViewProgress.Process((IStageToData<IEnvironmentAnimationResource>)cfg, progress, () => {
                    
                }),
            };
        }

        private void ProcessAnimationClip(IGrowthStageToData<IStageToData<IEnvironmentAnimationResource>> clip, float progress)
        {
            _growthViewProgress.Process(clip, progress, data => {
                    
            });
        }

        private void OnGrowthProgressUpdated(float newValue)
        {
            _actions.);
        }
    }
}