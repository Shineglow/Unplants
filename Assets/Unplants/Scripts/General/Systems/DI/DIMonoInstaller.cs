using UnityEngine;

namespace Unplants.Scripts.General.Systems.DI
{
    public abstract class DIMonoInstaller : MonoBehaviour
    {
        protected DIContainer container;
        protected virtual EInstallBindingMethod installMethod => EInstallBindingMethod.Awake;

        public DIMonoInstaller() : this((DIContainer)null) { }
        public DIMonoInstaller(DIMonoInstaller parentInstaller) : this(parentInstaller.container) { }
        public DIMonoInstaller(DIContainer parentContainer)
        {
            container = new DIContainer(parentContainer);
            CallInstallBinding(EInstallBindingMethod.Constructor);
        }

        private void Awake()
        {
            CallInstallBinding(EInstallBindingMethod.Awake);
        }

        private void Start()
        {
            CallInstallBinding(EInstallBindingMethod.Start);
        }

        public abstract void InstallBindings();

        private void CallInstallBinding(EInstallBindingMethod methodName)
        {
            if (installMethod == methodName)
            {
                InstallBindings();
            }
        }

        public void ResolveInstance<T>(T instance)
        {

        }
    }

    public enum EInstallBindingMethod
    {
        Awake,
        Start,
        Constructor,
    }
}