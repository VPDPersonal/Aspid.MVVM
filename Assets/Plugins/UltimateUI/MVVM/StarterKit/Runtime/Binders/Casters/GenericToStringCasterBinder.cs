using System;
using UltimateUI.MVVM.StarterKit.Converters;
using UltimateUI.MVVM.StarterKit.Converters.Strings;

namespace UltimateUI.MVVM.StarterKit.Binders.Casters
{
    public sealed class GenericToStringCasterBinder<T> : Binder, IBinder<T>
    {
        private readonly Action<string> _setValue;
        private readonly IConverter<T, string> _converter;

        public GenericToStringCasterBinder(Action<string> setValue, string format = null)
            : this(setValue, new GenericToString<T>(format)) { }
        
        public GenericToStringCasterBinder(Action<string> setValue, Func<T, string> converter) 
            : this(setValue, new GenericFuncConverter<T, string>(converter)) { }
        
        public GenericToStringCasterBinder(Action<string> setValue, IConverter<T, string> converter)
        {
            _setValue = setValue;
            _converter = converter;
        }
        
        public void SetValue(T value) =>
            _setValue(_converter.Convert(value));
    }
}