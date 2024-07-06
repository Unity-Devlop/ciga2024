using System;
using UnityEngine;

namespace UnityToolkit
{
    public struct BindablePropertyUnRegister : ICommand
    {
        private Action _unRegister;

        public BindablePropertyUnRegister(Action unRegister)
        {
            this._unRegister = unRegister;
        }

        public void Execute()
        {
            _unRegister.Invoke();
            _unRegister = null;
        }
    }


    public class BindData<T>
    {
        private event Action<T> Listeners = delegate { };
        private T _data;

        public BindData(T data)
        {
            _data = data;
        }

        public void SetDirty()
        {
            Listeners(_data);
        }

        public void Listen(Action<T> action)
        {
            Listeners += action;
        }

        public void UnListen(Action<T> action)
        {
            Listeners -= action;
        }
    }

    [Serializable]
    public sealed class BindableProperty<T>
    {
        [SerializeField] private T _value;
        private Action<T> _onValueChanged = (_) => { };

        public T Value
        {
            get => Get();
            set
            {
                if (value == null && _value == null) return;
                if (value != null && value.Equals(_value)) return;
                Set(value);
                _onValueChanged.Invoke(value);
            }
        }

        public T ValueWithoutNotify => _value;

        public BindableProperty(T value = default)
        {
            _value = value;
        }

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.Button]
#endif
        private void Set(T value)
        {
            _value = value;
        }

        private T Get()
        {
            return _value;
        }

        public ICommand Register(Action<T> onValueChanged)
        {
            _onValueChanged += onValueChanged;
            return new BindablePropertyUnRegister(() => UnRegister(onValueChanged));
        }

        public void UnRegister(Action<T> onValueChanged)
        {
            _onValueChanged -= onValueChanged;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public static implicit operator T(BindableProperty<T> property)
        {
            return property.Value;
        }

        public void Invoke()
        {
            _onValueChanged(_value);
        }
    }
}