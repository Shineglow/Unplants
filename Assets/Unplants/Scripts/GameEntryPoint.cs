using System.Collections.Generic;
using com.shineglow.di.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;
using Unplants.General.Systems.DragDropSystem;
using Unplants.General.Systems.EventSystemAbstraction;
using Unplants.Scripts.Data.InteractiveObjectsData.Plants;
using Unplants.Scripts.Gameplay.Planting.Plants;
using Unplants.Scripts.Gameplay.Systems;

namespace Unplants.Scripts
{
    public class GameEntryPoint : MonoBehaviour
    {
        private DragDropSystemBase<IDragListener<IDragDropItem>, IDragDropItem> _dragDropSystem;
        private PlantsFactory _plantsFactory;
        private List<PlantBaseView> _draggablePlants = new List<PlantBaseView>();

        [SerializeField] private PlantsConfigurationSO plantsConfiguration;
        [SerializeField] private PlantBaseView plantView;
        [SerializeField] private Physics2DRaycaster unityRaycaster;
        private RaycasterAbstractionBase _raycasterAbstraction;
        private DiContainer _container;

        private void Start()
        {
            CallOnStart();
        }

        public void CallOnStart()
        {
            _container = new DiContainer();
            _container.Bind<IPlantsConfiguration>().AsInstance(plantsConfiguration);
            _container.Bind<PlantBaseView>().AsInstance(plantView);
            _container.Bind<PlantsFactory>().IsCachingInstance();
            _container.Bind<EventSystem>().AsInstance(EventSystem.current);
            _container.Bind<Camera>().AsInstance(Camera.main);
            _container.Bind<Physics2DRaycaster>().AsInstance(unityRaycaster);
            _container.Bind<IRaycasterAbstraction<GameObject>>().To<RaycasterAbstractionBase>().IsCachingInstance();
            _container.Bind<RaycasterAbstractionBase>().IsCachingInstance();
            _container.Bind<DragDropSystemBase<IDragListener<IDragDropItem>, IDragDropItem>>().To<PlantDragDropSystem>().IsCachingInstance();

            _plantsFactory = _container.Resolve<PlantsFactory>();
            _raycasterAbstraction = _container.Resolve<RaycasterAbstractionBase>();
            _dragDropSystem = _container.Resolve<DragDropSystemBase<IDragListener<IDragDropItem>, IDragDropItem>>();
            
            (IPlantModel model, PlantViewModel viewModel) = _plantsFactory.GetPlant(EPlant.Tomato);
            _dragDropSystem.Add(viewModel);
        }
    }
}
