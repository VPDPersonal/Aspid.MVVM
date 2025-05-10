using System;
using System.Runtime.CompilerServices;

namespace Aspid.MVVM
{
    public sealed class OneWayToSourceBindableMemberEvent<T> : IBindableMemberEvent
    {
        private readonly Action<T?> _setValue;
        
        public OneWayToSourceBindableMemberEvent(Action<T?> setValue)
        {
            _setValue = setValue ?? throw new NullReferenceException(nameof(setValue));
        }

        public IBindableMemberEventRemover? Add(IBinder binder)
        {
            var mode = binder.Mode;
            
            if (mode is BindMode.OneWayToSource or BindMode.TwoWay)
            {
                GetReverseBinder(binder).ValueChanged += _setValue;
                return this;
            }
            
            throw new InvalidOperationException();
        }

        public void Remove(IBinder binder) =>
            GetReverseBinder(binder).ValueChanged -= _setValue;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IReverseBinder<T> GetReverseBinder(IBinder binder)
        {
            if (binder is not IReverseBinder<T> specificReverseBinder) 
                throw new InvalidOperationException($"Binder must be of type {typeof(IReverseBinder<T>)}.");

            return specificReverseBinder;
        }
    }
}