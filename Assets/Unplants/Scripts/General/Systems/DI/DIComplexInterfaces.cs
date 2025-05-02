namespace Unplants.Scripts.General.Systems.DI
{
    public interface IBindAs<T> : IBindAsSingle<T>, IBindAsInstance<T>{}
    public interface IDIBindingBuilder<T> : IBindTo<T>, IBindAs<T> { }
}