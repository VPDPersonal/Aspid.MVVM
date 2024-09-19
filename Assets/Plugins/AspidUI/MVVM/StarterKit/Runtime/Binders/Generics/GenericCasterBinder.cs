using System;
using AspidUI.MVVM.StarterKit.Converters;

namespace AspidUI.MVVM.StarterKit.Binders.Generics
{
    public class GenericCasterBinder<TFrom, TTo> : Binder, IBinder<TFrom>
    {
        private readonly Action<TTo> _setValue;
        private readonly IConverter<TFrom, TTo> _converter;

        public GenericCasterBinder(Action<TTo> setValue, Func<TFrom, TTo> converter)
            : this(setValue, new GenericFuncConverter<TFrom, TTo>(converter)) { }
        
        public GenericCasterBinder(Action<TTo> setValue, IConverter<TFrom, TTo> converter)
        {
            _setValue = setValue;
            _converter = converter;
        }

        public void SetValue(TFrom value) =>
            _setValue(_converter.Convert(value));
    }
    
    public class GenericCasterBinder<TTarget, TFrom, TTo> : Binder, IBinder<TFrom>
    {
        private readonly TTarget _target;
        private readonly Action<TTarget, TTo> _setValue;
        private readonly IConverter<TFrom, TTo> _converter;

        public GenericCasterBinder(TTarget target, Action<TTarget, TTo> setValue, Func<TFrom, TTo> converter)
            : this(target, setValue, new GenericFuncConverter<TFrom, TTo>(converter)) { }
        
        public GenericCasterBinder(TTarget target, Action<TTarget, TTo> setValue, IConverter<TFrom, TTo> converter)
        {
            _target = target;
            _setValue = setValue;
            _converter = converter;
        }

        public void SetValue(TFrom value) =>
            _setValue(_target, _converter.Convert(value));
    }
}