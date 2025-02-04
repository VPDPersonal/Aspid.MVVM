#nullable enable
using System;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    public sealed class AnyToStringCasterBinder : Binder, IBinder<object?>
    {
        private readonly Action<string?> _setValue;
        private readonly IConverter<object?, string?> _converter;

        public AnyToStringCasterBinder(Action<string?> setValue, BindMode mode = BindMode.OneWay)
            : this(setValue, new ObjectToStringConverter(), mode) { }
        
        public AnyToStringCasterBinder(Action<string?> setValue, IConverter<object?, string?> converter, BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            mode.ThrowExceptionIfTwo();
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        public void SetValue(object? value) =>
            _setValue(_converter.Convert(value));
    }
}