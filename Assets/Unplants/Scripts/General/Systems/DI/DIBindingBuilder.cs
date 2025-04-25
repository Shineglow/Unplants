namespace Unplants.Scripts.General.Systems.DI
{
    public class DIBindingBuilder<T> : DIBindingBuilderAbstract, IDIBindingBuilder<T>, IBindAsSingle<T>, ICreateOnBind<T>
    {
        public DIBindingBuilder()
        {
            _record.Binding = typeof(T);
        }

        IBindAsSingle<T> IBindTo<T>.To<T1>()
        {
            _record.To = typeof(T1);
            _record.DIBindingParameters.typeOfInstance = typeof(T1);
            return this;
        }

        void IBindAsInstance<T>.AsInstance<T1>(T1 instance)
        {
            _record.Instance = instance;
            _record.DIBindingParameters.asInstance = true;
        }

        ICreateOnBind<T> IBindAsSingle<T>.AsSingle()
        {
            _record.DIBindingParameters.isSingle = true;
            return this;
        }

        void ICreateOnBind<T>.CreateOnBind()
        {
            _record.DIBindingParameters.createInstanceOnBind = true;
        }
    }
}