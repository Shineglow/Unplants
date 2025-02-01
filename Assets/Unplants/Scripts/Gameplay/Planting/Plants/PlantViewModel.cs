using System;
using System.Collections.Generic;
using Unplants.Scripts.Data.InteractiveObjectsData.Plants;
using Unplants.Scripts.General.Extensions;
using Debug = UnityEngine.Debug;

namespace Unplants.Scripts.Gameplay.Planting.Plants
{
    public class PlantViewModel : IPlantViewModel
    {
        private IPlantView _view;
        private IPlantModel _model;
        private IPlantConfiguration _configuration;

        private readonly List<Func<IPlantConfiguration, float, bool>> _actions;
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
                (cfg, progress) => ProcessAnimationClip(cfg, progress),
                (cfg, progress) => ProcessSprite(cfg, progress),
                (cfg, progress) => ProcessColor(cfg, progress),
            };
        }

        private bool ProcessAnimationClip(IGrowthStageToAnimationClip clip, float progress)
        {
            return false;
            // TODO: Add realization, before first animation was added
        }

        private bool ProcessSprite(IGrowthStageToSprite clip, float progress)
        {
            return false;
            // TODO: Add realization, before first sprites set was added
        }

        private bool ProcessColor(IGrowthStageToColorChange clip, float progress)
        {
            for (int i = clip.StageToData.Count-1; i > -1; i--)
            {
                GrowthStageToColorChange item = clip.StageToData[i];
                if(item.GrowthStage <= progress)
                {
                    _view.SetView(item.Data);
                    return true;
                }
            }

            Debug.LogError("Growth progress out of range!");
            return false;
        }

        private void OnGrowthProgressUpdated(float obj)
        {
            for (int i = 0; i < _actions.Count; i++)
            {
                if (_actions[i].Invoke(_configuration, obj))
                {
                    return;
                }
            }
        }
    }
}