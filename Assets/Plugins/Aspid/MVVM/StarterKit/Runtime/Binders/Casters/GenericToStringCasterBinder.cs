#nullable enable
using System;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    public sealed class GenericToStringCasterBinder<T> : Binder, IBinder<T>
    {
        private readonly Action<string?> _setValue;
        private readonly IConverter<T?, string?> _converter;
        
        public GenericToStringCasterBinder(Action<string?> setValue, string format, BindMode mode = BindMode.OneWay)
            : this(setValue, new GenericToString<T>(format), mode) { }
        
        public GenericToStringCasterBinder(Action<string?> setValue, IConverter<T?, string?> converter, BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            mode.ThrowExceptionIfTwo();
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }
        
        public void SetValue(T? value) =>
            _setValue(_converter.Convert(value));
    }
}