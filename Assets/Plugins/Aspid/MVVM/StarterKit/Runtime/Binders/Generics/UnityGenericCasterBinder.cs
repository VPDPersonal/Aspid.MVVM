#nullable enable
using System;
using UnityEngine.Events;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    public class UnityGenericCasterBinder<TFrom, TTo> : Binder, IBinder<TFrom>
    {
        private readonly UnityAction<TTo?> _setValue;
        private readonly IConverter<TFrom?, TTo?> _converter;

        public UnityGenericCasterBinder(UnityAction<TTo?> setValue, Func<TFrom?, TTo?> converter)
            : this(setValue, converter.ToConvert()) { }
        
        public UnityGenericCasterBinder(UnityAction<TTo?> setValue, IConverter<TFrom?, TTo?> converter)
        {
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        public void SetValue(TFrom? value) =>
            _setValue(_converter.Convert(value));
    }
    
    public class UnityGenericCasterBinder<TTarget, TFrom, TTo> : Binder, IBinder<TFrom>
    {
        private readonly TTarget _target;
        private readonly UnityAction<TTarget, TTo?> _setValue;
        private readonly IConverter<TFrom?, TTo?> _converter;

        public UnityGenericCasterBinder(
            TTarget target,
            UnityAction<TTarget, TTo?> setValue, 
            Func<TFrom?, TTo?> converter)
            : this(target, setValue, converter.ToConvert()) { }
        
        public UnityGenericCasterBinder(
            TTarget target,
            UnityAction<TTarget, TTo?> setValue,
            IConverter<TFrom?, TTo?> converter)
        {
            _target = target ?? throw new ArgumentNullException(nameof(target));
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        public void SetValue(TFrom? value) =>
            _setValue(_target, _converter.Convert(value));
    }
}