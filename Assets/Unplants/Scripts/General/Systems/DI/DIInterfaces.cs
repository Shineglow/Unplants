namespace Unplants.Scripts.General.Systems.DI
{
    public interface IBindTo<T>
    {
        IBindAsSingle<T> To<T1>() where T1 : T;
    }

    public interface IBindAsSingle<T>
    {
        ICreateOnBind<T> AsSingle();
    }

    public interface ICreateOnBind<T>
    {
        void CreateOnBind();
    }

    public interface IBindAsInstance<T>
    {
        void AsInstance<T1>(T1 instance) where T1 : T;
    }
}
