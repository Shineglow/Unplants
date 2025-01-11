using System;

namespace Assets.Unplants.Scripts.General.Types.Obsrvables
{
    public interface IObservableValue<T>
    {
        T Value { get; }
        event Action<T> ValueChanged;
    }
}
