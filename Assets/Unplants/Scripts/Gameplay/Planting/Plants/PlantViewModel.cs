using System;
using System.Collections.Generic;
using Unplants.General.Systems.DragDropSystem;
using Unplants.Scripts.Data.InteractiveObjectsData.Plants;
using Unplants.Scripts.General.Extensions;
using Debug = UnityEngine.Debug;

namespace Unplants.Scripts.Gameplay.Planting.Plants
{
    public class PlantViewModel : IPlantViewModel, IDragListener<IDragDropItem>
    {
        private IPlantView _view;
        private IPlantModel _model;
        private IPlantConfiguration _configuration;

        private readonly List<Func<IPlantConfiguration, float, bool>> _actions;

        public event Action<IDragDropItem, IPointerData> PointerDown;
        public event Action<IDragDropItem, IPointerData> PointerUp;
        public event Action<IDragDropItem, IPointerData> DragBegin;
        public event Action<IDragDropItem, IPointerData> Drag;
        public event Action<IDragDropItem, IPointerData> DragEnd;

        public PlantViewModel(IPlantView view, IPlantModel model, IPlantConfiguration configuration)
        {
            view.ThrowIfNull();
            model.ThrowIfNull();
            configuration.ThrowIfNull();

            _view = view;
            _model = model;
            _configuration = configuration;

            // _view.SetView(configuration.);
            OnGrowthProgressUpdated(model.GrowthProgress.Value);
            
            model.GrowthProgress.ValueChanged += OnGrowthProgressUpdated;
            _actions = new()
            {
                (cfg, progress) => ProcessAnimationClip(cfg, progress),
                (cfg, progress) => ProcessSprite(cfg, progress),
                (cfg, progress) => ProcessColor(cfg, progress),
            };

            view.PointerDown += OnPointerDown;
            view.PointerUp += OnPointerUp;
            view.DragBegin += OnDragBegin;
            view.Drag += OnDrag;
            view.DragEnd += OnDragEnd;
        }

        private bool ProcessAnimationClip(IGrowthStageToAnimationClip stageToClip, float normalizedProgress)
        {
            return false;
            // TODO: Add realization, before first animation was added
        }

        private bool ProcessSprite(IGrowthStageToSprite stageToSprite, float normalizedProgress)
        {
            return false;
            // TODO: Add realization, before first sprites set was added
        }

        private bool ProcessColor(IGrowthStageToColorChange stageToColor, float normalizedProgress)
        {
            for (int i = stageToColor.StageToData.Count-1; i > -1; i--)
            {
                GrowthStageToColorChange item = stageToColor.StageToData[i];
                if(item.NormalizedGrowthStage <= normalizedProgress)
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
                if (_actions[i].Invoke(_configuration, obj/_configuration.TimeToGrowth))
                {
                    return;
                }
            }
        }

        private void OnPointerDown(IPlantView item, IPointerData pointerData) => PointerDown?.Invoke(_model, pointerData);
        private void OnPointerUp(IPlantView item, IPointerData pointerData) => PointerUp?.Invoke(_model, pointerData);
        private void OnDragBegin(IPlantView item, IPointerData pointerData) => DragBegin?.Invoke(_model, pointerData);
        private void OnDrag(IPlantView item, IPointerData pointerData) => Drag?.Invoke(_model, pointerData);
        private void OnDragEnd(IPlantView item, IPointerData pointerData) => DragEnd?.Invoke(_model, pointerData);

    }
}