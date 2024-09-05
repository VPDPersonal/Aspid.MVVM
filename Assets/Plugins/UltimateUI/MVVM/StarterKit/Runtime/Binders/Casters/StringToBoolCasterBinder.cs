using System;
using UltimateUI.MVVM.StarterKit.Converters;
using UltimateUI.MVVM.StarterKit.Converters.Bools;

namespace UltimateUI.MVVM.StarterKit.Binders.Casters
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
            _setValue = setValue;
            _converter = converter;
        }

        public void SetValue(string value) =>
            _setValue(_converter.Convert(value));
    }
}