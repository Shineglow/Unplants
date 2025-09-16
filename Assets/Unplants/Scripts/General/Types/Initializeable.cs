namespace Unplants.Scripts.General.Types
{
    public struct InitializableValue<T>
    {
        private T _value;

        public T Value{
            get => _value;
            set
            {
                if (Equals(_value, value) && WasInitialized) return;
                _value = value;
                WasInitialized = true;
            }
        }
        
        public bool WasInitialized { get; private set; }

        public void Reset()
        {
            _value = default;
            WasInitialized = false;
        }

        public static implicit operator T(InitializableValue<T> wrapper)
        {
            return wrapper.Value;
        }
    }
}