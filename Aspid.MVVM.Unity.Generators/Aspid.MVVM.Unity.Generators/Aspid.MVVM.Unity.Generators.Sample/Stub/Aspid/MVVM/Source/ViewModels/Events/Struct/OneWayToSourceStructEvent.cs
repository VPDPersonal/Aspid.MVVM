using System;

namespace Aspid.MVVM
{
    public sealed class OneWayToSourceStructEvent<T> : OneWayToSourceStructEvent<T, ValueType>
        where T : struct
    {
        public OneWayToSourceStructEvent(Action<T> setValue) 
            : base(setValue) { }
    }

    public abstract class OneWayToSourceStructEvent<T, TBoxed> : IBindableMemberEvent
        where T : struct, TBoxed
        where TBoxed : class
    {
        private readonly Action<T> _setValue;

        protected OneWayToSourceStructEvent(Action<T> setValue)
        {
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
        }

        public IBindableMemberEventRemover Add(IBinder binder)
        {
            var mode = binder.Mode;
            
            if (mode is not (BindMode.OneWayToSource or BindMode.TwoWay))
                throw new InvalidOperationException($"Mode must be OneWayToSource. Mode = {{{mode}}}");

            switch (binder)
            {
                case IReverseBinder<T> reverseBinder: reverseBinder.ValueChanged += _setValue; break;
                case IReverseBinder<TBoxed> structReverseBinder: structReverseBinder.ValueChanged += SetBoxedValue; break;
                default: throw ReverseBinderInvalidCastException<T>.Struct<TBoxed>();
            }
            
            return this;
        }

        public void Remove(IBinder binder)
        {
            switch (binder)
            {
                case IReverseBinder<T> reverseBinder: reverseBinder.ValueChanged -= _setValue; break;
                case IReverseBinder<TBoxed> structReverseBinder: structReverseBinder.ValueChanged -= SetBoxedValue; break;
                default: throw ReverseBinderInvalidCastException<T>.Struct<TBoxed>();
            }
        }

        private void SetBoxedValue(TBoxed? value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));
            
            _setValue.Invoke((T)value);
        }
    }
}