using System;

namespace Aspid.MVVM.StarterKit
{
    public class GenericCasterBinder<TFrom, TTo> : Binder, IBinder<TFrom>
    {
        private readonly Action<TTo?> _setValue;
        private readonly IConverter<TFrom?, TTo?> _converter;
        
        public GenericCasterBinder(
            Action<TTo?> setValue,
            IConverter<TFrom?, TTo?> converter,
            BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            mode.ThrowExceptionIfTwo();
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        public void SetValue(TFrom? value) =>
            _setValue(_converter.Convert(value));
    }
    
    public class GenericCasterBinder<TTarget, TFrom, TTo> : Binder, IBinder<TFrom>
    {
        private readonly TTarget _target;
        private readonly Action<TTarget, TTo?> _setValue;
        private readonly IConverter<TFrom?, TTo?> _converter;
        
        public GenericCasterBinder(
            TTarget target,
            Action<TTarget, TTo?> setValue, 
            IConverter<TFrom?, TTo?> converter,
            BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            mode.ThrowExceptionIfTwo();
            _target = target ?? throw new ArgumentNullException(nameof(target));
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        public void SetValue(TFrom? value) =>
            _setValue(_target, _converter.Convert(value));
    }
}