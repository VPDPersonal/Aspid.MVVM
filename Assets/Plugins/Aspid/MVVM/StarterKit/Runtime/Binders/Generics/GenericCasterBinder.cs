#nullable enable
using System;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    public class GenericCasterBinder<TFrom, TTo> : Binder, IBinder<TFrom>
    {
        private readonly Action<TTo?> _setValue;
        private readonly IConverter<TFrom?, TTo?> _converter;

        public GenericCasterBinder(Action<TTo?> setValue, Func<TFrom?, TTo?> converter)
            : this(setValue, converter.ToConvert()) { }
        
        public GenericCasterBinder(Action<TTo?> setValue, IConverter<TFrom?, TTo?> converter)
        {
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

        public GenericCasterBinder(TTarget target, Action<TTarget, TTo?> setValue, Func<TFrom?, TTo?> converter)
            : this(target, setValue, converter.ToConvert()) { }
        
        public GenericCasterBinder(TTarget target, Action<TTarget, TTo?> setValue, IConverter<TFrom?, TTo?> converter)
        {
            _target = target ?? throw new ArgumentNullException(nameof(target));
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        public void SetValue(TFrom? value) =>
            _setValue(_target, _converter.Convert(value));
    }
}