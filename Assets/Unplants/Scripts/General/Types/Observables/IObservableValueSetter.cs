using System;

namespace Unplants.Scripts.General.Types.Observables
{
    public interface IObservableValueSetter<T>
    {
        T Value {  get; set; }
        void SetAction(Action<T> newAction);
    }
}
