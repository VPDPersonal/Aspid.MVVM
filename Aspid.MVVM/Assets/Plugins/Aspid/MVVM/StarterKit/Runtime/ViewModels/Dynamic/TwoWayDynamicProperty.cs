using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    public sealed class TwoWayDynamicProperty<T> : IDynamicProperty
    {
        private T? _value;
        private readonly TwoWayBindableMember<T> _bindableMember;
        
        public TwoWayDynamicProperty(T? value)
        {
            _value = value;
            _bindableMember = new TwoWayBindableMember<T>(_value, SetValue);
        }

        public IBinderAdder GetAdder() =>
            _bindableMember;

        private void SetValue(T? value)
        {
#pragma warning disable CS8604 // Possible null reference argument.
            if (EqualityComparer<T>.Default.Equals(_value, value)) return;
#pragma warning restore CS8604 // Possible null reference argument.
            
            _value = value;
            _bindableMember.Value = value;
        }
    }
}