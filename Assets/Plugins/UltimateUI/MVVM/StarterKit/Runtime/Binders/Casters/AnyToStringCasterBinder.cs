using System;
using UltimateUI.MVVM.StarterKit.Converters;
using UltimateUI.MVVM.StarterKit.Converters.Strings;

namespace UltimateUI.MVVM.StarterKit.Binders.Casters
{
    public sealed class AnyToStringCasterBinder : Binder, IBinder<object>
    {
        private readonly Action<string> _setValue;
        private readonly IConverter<object, string> _converter;

        public AnyToStringCasterBinder(Action<string> setValue)
            : this(setValue, new ObjectToStringConverter()) { }
        
        public AnyToStringCasterBinder(Action<string> setValue, Func<object, string> converter) 
            : this(setValue, new GenericFuncConverter<object, string>(converter)) { }
        
        public AnyToStringCasterBinder(Action<string> setValue, IConverter<object, string> converter)
        {
            _setValue = setValue;
            _converter = converter;
        }

        public void SetValue(object value) =>
            _setValue(_converter.Convert(value));
    }
}