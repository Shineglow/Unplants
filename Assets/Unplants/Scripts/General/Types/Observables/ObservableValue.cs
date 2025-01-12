using System;
using UnityEngine;

namespace Unplants.Scripts.General.Types.Observables
{
    [Serializable]
    public struct ObservableValue<T> : IObservableValueSetter<T>, IObservableValue<T> where T : IComparable<T>
    {
        [SerializeField] private readonly T _value;
        public T Value
        {
            get => _value;
            set
            {
                if(Value.CompareTo(value) != 0)
                {
                    Value = value;
                    ValueChanged?.Invoke(Value);
                }
            }
        }

        public event Action<T> ValueChanged;

        public ObservableValue(T value) : this(value, null) {}
        public ObservableValue(T value, Action<T> onValueChanged)
        {
            _value = value;
            ValueChanged = onValueChanged;
        }

        public void SetAction(Action<T> newAction) => ValueChanged = newAction;
    }
}
