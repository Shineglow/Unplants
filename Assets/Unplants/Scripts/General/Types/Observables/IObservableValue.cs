using System;

namespace Unplants.Scripts.General.Types.Observables
{
    public interface IObservableValue<T>
    {
        T Value { get; }
        event Action<T> ValueChanged;
    }
}
