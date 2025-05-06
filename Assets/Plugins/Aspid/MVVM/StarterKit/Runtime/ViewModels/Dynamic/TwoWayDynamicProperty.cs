using System.Collections.Generic;

namespace Aspid.MVVM.StarterKit
{
    public sealed class TwoWayDynamicProperty<T> : IDynamicProperty
    {
        private T _value;
        private TwoWayViewModelEvent<T>? _event;
        
        public TwoWayDynamicProperty(T value)
        {
            _value = value;
        }
        
        public IViewModelEventAdder GetAdder()
        {
            _event ??= new TwoWayViewModelEvent<T>()
            {
                SetValue = SetValue
            };

            return BindableMember<T>.TwoWay(_event, _value);
        }

        private void SetValue(T? value)
        {
            if (EqualityComparer<T>.Default.Equals(_value, value)) return;
            
            _value = value;
            _event?.Invoke(value);
        }
    }
}