using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    [BindModeOverride(BindMode.OneTime)]
    public class OneTimeValue<T> : OneWayValue<T>
    {
        public OneTimeValue() 
            : base(BindMode.OneTime) { }

        public OneTimeValue(T? value)
            : base(value, BindMode.OneTime) { }

        public OneTimeValue(T? value, IConverter<T?, T?>? converter) 
            : base(value, converter, BindMode.OneTime) { }
    }
}