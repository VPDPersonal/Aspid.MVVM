using System.Collections.Generic;

namespace Aspid.MVVM.StarterKit.ViewModels
{
    public sealed class TwoWayDynamicProperty<T> : IDynamicProperty
    {
        private T _value;
        private TwoWayViewModelEvent<T> _event;
        
        public TwoWayDynamicProperty(T value)
        {
            _value = value;
        }

        public BindResult AddBinder(IBinder binder)
        {
            var mode = binder.Mode;
            _event ??= new TwoWayViewModelEvent<T>();

            if (mode is BindMode.TwoWay or BindMode.OneWayToSource)
                _event.SetValue = SetValue;
            
            return new BindResult(_event.AddBinder(binder, _value, mode));
        }

        private void SetValue(T value)
        {
            if (EqualityComparer<T>.Default.Equals(_value, value)) return;
            
            _value = value;
            _event?.Invoke(value);
        }
    }
}