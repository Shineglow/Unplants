using UnityEngine;

namespace Unplants.General.Systems.Unplants.Scripts.General.Systems.DI
{
    public class DIMonoInstaller : MonoBehaviour
    {
        private void Awake()
        {
            DIContainer parentContainer = new DIContainer();
            DIContainer container = new DIContainer(parentContainer);
            parentContainer.AddBinding<IDITest>().To<DITest>();
            container.AddBinding<DITest2>().AsSingle().CreateOnBind();
            var a = container.Resolve<DITest2>();
            Debug.Log(a.I);
            var b = container.Resolve<DITest2>();
            Debug.Log(ReferenceEquals(a,b));
            
            DIContainer container2 = new DIContainer();
            container2.AddBinding<IDITest>().To<DITest>().AsSingle();
            Debug.Log(ReferenceEquals(container2.Resolve<IDITest>(),container2.Resolve<IDITest>()));
        }
    }
}