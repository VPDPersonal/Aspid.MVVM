using System;

namespace Aspid.MVVM
{
    public sealed class OneWayToSourceClassEvent<T> : IBindableMemberEvent
        where T : class
    {
        private readonly Action<T?> _setValue;

        public OneWayToSourceClassEvent(Action<T?> setValue)
        {
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
        }

        public IBindableMemberEventRemover Add(IBinder binder)
        {
            var mode = binder.Mode;
            
            if (mode is not (BindMode.OneWayToSource or BindMode.TwoWay))
                throw new InvalidOperationException($"Mode must be OneWayToSource. Mode = {{{mode}}}");

            if (binder is not IReverseBinder<T> reverseBinder) 
                throw ReverseBinderInvalidCastException<T>.Class();
            
            reverseBinder.ValueChanged += _setValue;
            return this;
        }

        public void Remove(IBinder binder)
        {
            if (binder is not IReverseBinder<T> reverseBinder) 
                throw ReverseBinderInvalidCastException<T>.Class();
            
            reverseBinder.ValueChanged -= _setValue;
        }
    }
}