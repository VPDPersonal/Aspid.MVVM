#nullable enable
using System;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    public sealed class StringToBoolCasterBinder : Binder, IBinder<string>
    {
        private readonly Action<bool> _setValue;
        private readonly IConverter<string?, bool> _converter;
        
        public StringToBoolCasterBinder(Action<bool> setValue, BindMode mode)
            : this(setValue, false, mode) { }
        
        public StringToBoolCasterBinder(Action<bool> setValue, bool isInvert = false, BindMode mode = BindMode.OneWay)
            : this(setValue, new StringEmptyToBoolConverter(isInvert), mode) { }
        
        public StringToBoolCasterBinder(Action<bool> setValue, IConverter<string?, bool> converter, BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            mode.ThrowExceptionIfTwo();
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        public void SetValue(string? value) =>
            _setValue(_converter.Convert(value));
    }
}