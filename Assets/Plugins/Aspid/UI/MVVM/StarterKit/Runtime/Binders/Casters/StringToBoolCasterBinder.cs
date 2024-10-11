#nullable enable
using System;
using Aspid.UI.MVVM.StarterKit.Converters;
using Aspid.UI.MVVM.StarterKit.Converters.Bools;

namespace Aspid.UI.MVVM.StarterKit.Binders.Casters
{
    public sealed class StringToBoolCasterBinder : Binder, IBinder<string>
    {
        private readonly Action<bool> _setValue;
        private readonly IConverter<string, bool> _converter;
        
        public StringToBoolCasterBinder(Action<bool> setValue, bool isInvert = false)
            : this(setValue, new StringEmptyToBoolConverter(isInvert)) { }
        
        public StringToBoolCasterBinder(Action<bool> setValue, Func<string, bool> converter) 
            : this(setValue, new GenericFuncConverter<string, bool>(converter)) { }
        
        public StringToBoolCasterBinder(Action<bool> setValue, IConverter<string, bool> converter)
        {
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        public void SetValue(string value) =>
            _setValue(_converter.Convert(value));
    }
}