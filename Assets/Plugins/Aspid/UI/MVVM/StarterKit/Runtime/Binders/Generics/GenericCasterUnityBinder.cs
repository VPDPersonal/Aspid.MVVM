#nullable enable
using System;
using UnityEngine.Events;
using Aspid.UI.MVVM.StarterKit.Converters;

namespace Aspid.UI.MVVM.StarterKit.Binders.Generics
{
    public class GenericCasterUnityBinder<TFrom, TTo> : Binder, IBinder<TFrom>
    {
        private readonly UnityAction<TTo?> _setValue;
        private readonly IConverter<TFrom?, TTo?> _converter;

        public GenericCasterUnityBinder(UnityAction<TTo?> setValue, Func<TFrom?, TTo?> converter)
            : this(setValue, new GenericFuncConverter<TFrom?, TTo?>(converter)) { }
        
        public GenericCasterUnityBinder(UnityAction<TTo?> setValue, IConverter<TFrom?, TTo?> converter)
        {
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        public void SetValue(TFrom? value) =>
            _setValue(_converter.Convert(value));
    }
    
    public class GenericCasterUnityBinder<TTarget, TFrom, TTo> : Binder, IBinder<TFrom>
    {
        private readonly TTarget _target;
        private readonly UnityAction<TTarget, TTo?> _setValue;
        private readonly IConverter<TFrom?, TTo?> _converter;

        public GenericCasterUnityBinder(TTarget target, UnityAction<TTarget, TTo?> setValue, Func<TFrom?, TTo?> converter)
            : this(target, setValue, new GenericFuncConverter<TFrom?, TTo?>(converter)) { }
        
        public GenericCasterUnityBinder(TTarget target, UnityAction<TTarget, TTo?> setValue, IConverter<TFrom?, TTo?> converter)
        {
            _target = target ?? throw new ArgumentNullException(nameof(target));
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        public void SetValue(TFrom? value) =>
            _setValue(_target, _converter.Convert(value));
    }
}