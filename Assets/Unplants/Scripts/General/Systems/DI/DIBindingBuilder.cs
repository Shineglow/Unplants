namespace Unplants.General.Systems.Unplants.Scripts.General.Systems.DI
{
    public class DIBindingBuilder<T> : DIBindingBuilderAbstract
    {
        public DIBindingBuilder()
        {
            _record.Binding.Value = typeof(T);
        }

        public DIBindingBuilder<T> To<T1>() where T1 : T
        {
            _record.To.Value = typeof(T1);
            _record.DIBindingParameters.typeOfInstance = typeof(T1);
            return this;
        }

        public DIBindingBuilder<T> AsSingle()
        {
            _record.DIBindingParameters.isSingle = true;
            return this;
        }

        public DIBindingBuilder<T> CreateOnBind()
        {
            _record.DIBindingParameters.createInstanceOnBind = true;
            return this;
        }
    }
}