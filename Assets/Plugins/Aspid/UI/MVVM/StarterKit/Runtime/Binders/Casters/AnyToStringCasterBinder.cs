#nullable enable
using System;
using Aspid.UI.MVVM.StarterKit.Converters;
using Aspid.UI.MVVM.StarterKit.Converters.Strings;

namespace Aspid.UI.MVVM.StarterKit.Binders
{
    public sealed class AnyToStringCasterBinder : Binder, IBinder<object?>
    {
        private readonly Action<string> _setValue;
        private readonly IConverter<object?, string> _converter;

        public AnyToStringCasterBinder(Action<string> setValue)
            : this(setValue, new ObjectToStringConverter()) { }
        
        public AnyToStringCasterBinder(Action<string> setValue, Func<object?, string> converter) 
            : this(setValue, new GenericFuncConverter<object?, string>(converter)) { }
        
        public AnyToStringCasterBinder(Action<string> setValue, IConverter<object?, string> converter)
        {
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        public void SetValue(object? value) =>
            _setValue(_converter.Convert(value));
    }
}