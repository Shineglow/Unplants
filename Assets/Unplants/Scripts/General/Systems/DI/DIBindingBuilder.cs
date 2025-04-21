namespace Unplants.General.Systems.Unplants.Scripts.General.Systems.DI
{
    public class DIBindingBuilder<T> : DIBindingBuilderAbstract
    {
        public DIBindingBuilder()
        {
            _record.Binding.Value = typeof(T);
        }

        public void To<T1>() where T1 : T
        {
            _record.To.Value = typeof(T1);
            EndChain();
        }
    }
}