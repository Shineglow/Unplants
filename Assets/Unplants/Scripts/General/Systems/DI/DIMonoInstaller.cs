using UnityEngine;

namespace Unplants.Scripts.General.Systems.DI
{
    public abstract class DIMonoInstaller : MonoBehaviour
    {
        protected DIContainer container;
        protected virtual EInstallBindingMethod installMethod => EInstallBindingMethod.Awake;

        public DIMonoInstaller()
        {
            container = new DIContainer();
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
            if (installMethod == EInstallBindingMethod.Constructor)
            {
                InstallBindings();
            }
        }
    }

    public enum EInstallBindingMethod
    {
        Awake,
        Start,
        Constructor,
    }
}