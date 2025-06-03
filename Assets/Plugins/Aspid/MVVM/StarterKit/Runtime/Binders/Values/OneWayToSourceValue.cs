using System;

namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    [BindModeOverride(BindMode.OneWayToSource)]
    public class OneWayToSourceValue<T> : TwoWayValue<T>
    {
        public OneWayToSourceValue() 
            : base(BindMode.OneWayToSource) { }

        public OneWayToSourceValue(T? value) 
            : base(value, BindMode.OneWayToSource) { }

        public OneWayToSourceValue(T? value, IConverter<T?, T?>? converter)
            : base(value, converter, BindMode.OneWayToSource) { }
    }
}