﻿using Assets.Unplants.Scripts.General.Types.Obsrvables;
using System;

namespace Assets.Unplants.Scripts.General.Types
{
    public class ObservableValue<T> : IObservableValueSetter<T> where T : IComparable<T>
    {
        private readonly T _value;
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

        private ObservableValue() { }
        public ObservableValue(T value)
        {
            _value = value;
        }
        public ObservableValue(T value, Action<T> onValueChanged) : this(value)
        {
            ValueChanged += onValueChanged;
        }

        public void SetAction(Action<T> newAction) => ValueChanged = newAction;
    }
}
