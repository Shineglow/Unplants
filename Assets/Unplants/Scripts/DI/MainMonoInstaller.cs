using UnityEngine.EventSystems;
using UnityEngine;
using Unplants.Scripts.General.Systems.DI;
using Unplants.General.Systems.EventSystemAbstraction;
using Unplants.General.Systems.DragDropSystem;
using Unplants.Scripts.Gameplay.Systems;
using Unplants.Scripts.Data.InteractiveObjectsData.Plants;
using Unplants.Scripts.Gameplay.Planting.Plants;
using Unplants.Scripts.General;

namespace Assets.Unplants.Scripts.Gameplay.Systems
{
    public class MainMonoInstaller : DIMonoInstaller
    {
        [SerializeField] private EventSystem eventSystem;
        [SerializeField] private Physics2DRaycaster physics2DRaycaster;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private PlantsConfigurationSO _plantsConfigurationSO;
        [SerializeField] private GameEntryPoint gameEntryPoint;
        [SerializeField] private PlantBaseView plantBaseView;

        public override void InstallBindings()
        {
            container.Bind<EventSystem>().AsInstance(eventSystem);
            container.Bind<Physics2DRaycaster>().AsInstance(physics2DRaycaster);
            container.Bind<IRaycasterAbstraction<GameObject>>().To<RaycasterAbstractionBase>().AsSingle();
            container.Bind<Camera>().AsInstance(mainCamera);
            container.Bind<DragDropSystemBase<IDragListener<IDragDropItem>, IDragDropItem>>().To<PlantDragDropSystem>().AsSingle();
            container.BindToInterfacesAndSelf<PlantsConfigurationSO>().AsInstance(_plantsConfigurationSO);
            container.Bind<IPlantView>().To<PlantBaseView>().AsInstance(plantBaseView);
            container.Bind<PlantsFactory>().AsSingle().CreateOnBind();
            container.ResolveAttributeMarked(gameEntryPoint);
            gameEntryPoint.CallOnStart();
        }
    }
}
