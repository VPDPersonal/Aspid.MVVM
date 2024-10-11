#nullable enable
using System;
using Aspid.UI.MVVM.StarterKit.Converters;
using Aspid.UI.MVVM.StarterKit.Converters.Strings;

namespace Aspid.UI.MVVM.StarterKit.Binders.Casters
{
    public sealed class GenericToStringCasterBinder<T> : Binder, IBinder<T>
    {
        private readonly Action<string> _setValue;
        private readonly IConverter<T, string> _converter;

        public GenericToStringCasterBinder(Action<string> setValue, string? format = null)
            : this(setValue, new GenericToString<T>(format)) { }
        
        public GenericToStringCasterBinder(Action<string> setValue, Func<T, string> converter) 
            : this(setValue, new GenericFuncConverter<T, string>(converter)) { }
        
        public GenericToStringCasterBinder(Action<string> setValue, IConverter<T, string> converter)
        {
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }
        
        public void SetValue(T value) =>
            _setValue(_converter.Convert(value));
    }
}