using UnityEngine;

namespace Unplants.General.Systems.Unplants.Scripts.General.Systems.DI
{
    public class DIMonoInstaller : MonoBehaviour
    {
        protected DIContainerBase container = new DIContainerBase();

        private void Awake()
        {
            container.AddBinding<IDITest>().To<DITest>();
            container.AddBinding<DITest2>();
            var a = container.Resolve<DITest2>();
            
            Debug.Log(a.I);
        }
    }
}