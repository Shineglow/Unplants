using System;

namespace Assets.Unplants.Scripts.General.Types.Obsrvables
{
    public interface IObservableValueSetter<T> : IObservableValue<T>
    {
        T Value {  get; set; }
        void SetAction(Action<T> newAction);
    }
}
