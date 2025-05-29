using System.Collections.Generic;

namespace Aspid.MVVM.StarterKit
{
    public sealed class TwoWayDynamicProperty<T> : IDynamicProperty
    {
        private T? _value;
        private readonly TwoWayClassEvent<T> _event;
        
        public TwoWayDynamicProperty(T? value)
        {
            _value = value;
            _event = new TwoWayClassEvent<T>(_value, SetValue);
        }

        public IBindableMemberEventAdder GetAdder() =>
            _event;

        private void SetValue(T? value)
        {
            if (EqualityComparer<T>.Default.Equals(_value, value)) return;
            
            _value = value;
            _event?.Invoke(value);
        }
    }
}