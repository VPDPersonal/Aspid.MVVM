#nullable enable
using System;
using UnityEngine.Events;

namespace Aspid.MVVM.StarterKit.Unity
{
    public class UnityGenericCasterBinder<TFrom, TTo> : Binder, IBinder<TFrom>
    {
        private readonly UnityAction<TTo?> _setValue;
        private readonly IConverter<TFrom?, TTo?> _converter;
        
        public UnityGenericCasterBinder(
            UnityAction<TTo?> setValue, 
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
    
    public class UnityGenericCasterBinder<TTarget, TFrom, TTo> : Binder, IBinder<TFrom>
    {
        private readonly TTarget _target;
        private readonly UnityAction<TTarget, TTo?> _setValue;
        private readonly IConverter<TFrom?, TTo?> _converter;
        
        public UnityGenericCasterBinder(
            TTarget target,
            UnityAction<TTarget, TTo?> setValue,
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