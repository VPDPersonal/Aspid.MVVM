using System;
using System.Runtime.CompilerServices;

namespace Aspid.MVVM
{
    public sealed class TwoWayBindableMemberEvent<T> : IBindableMemberEvent
    {
        public event Action<T?>? Changed;

        private T? _value;
        private readonly Action<T?> _setValue;

        public TwoWayBindableMemberEvent(T? value, Action<T?> setValue)
        {
            _value = value;
            _setValue = setValue ?? throw new NullReferenceException(nameof(setValue));
        }
        
        public void Invoke(T value)
        {
            _value = value;
            Changed?.Invoke(value);
        }

        public IBindableMemberEventRemover? Add(IBinder binder)
        {
            var mode = binder.Mode;

            switch (mode)
            {
                case BindMode.OneTime:
                    binder.Cast<T>().SetValue(_value);
                    break;
                
                case BindMode.OneWay: 
                    SubscribeBinder(binder);
                    break;
                
                case BindMode.TwoWay:
                    SubscribeBinder(binder);
                    SubscribeReverseBinder(binder);
                    break;
                
                case BindMode.OneWayToSource:
                    SubscribeReverseBinder(binder);
                    break;
                
                case BindMode.None:
                default: ThrowInvalidOperationException(mode);
                    break;
            }

            return this;
        }

        public void Remove(IBinder binder)
        {
            var mode = binder.Mode;

            switch (mode)
            {
                case BindMode.OneTime: break;
                
                case BindMode.OneWay: 
                    UnsubscribeBinder(binder);
                    break;
                
                case BindMode.TwoWay:
                    UnsubscribeBinder(binder);
                    UnsubscribeReverseBinder(binder);
                    break;
                
                case BindMode.OneWayToSource:
                    UnsubscribeReverseBinder(binder);
                    break;
                
                case BindMode.None:
                default: ThrowInvalidOperationException(mode);
                    break;
            }
        }

        private void SubscribeBinder(IBinder binder)
        {
            var specificBinder = binder.Cast<T>();
            
            specificBinder.SetValue(_value);
            Changed += specificBinder.SetValue;
        }

        private void UnsubscribeBinder(IBinder binder) =>
            Changed -= binder.Cast<T>().SetValue;
        
        private void SubscribeReverseBinder(IBinder binder) =>
            GetReverseBinder(binder).ValueChanged += _setValue;
        
        private void UnsubscribeReverseBinder(IBinder binder) =>
            GetReverseBinder(binder).ValueChanged -= _setValue;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IReverseBinder<T> GetReverseBinder(IBinder binder)
        {
            if (binder is not IReverseBinder<T> specificReverseBinder) 
                throw new InvalidOperationException($"Binder must be of type {typeof(IReverseBinder<T>)}.");

            return specificReverseBinder;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ThrowInvalidOperationException(BindMode mode) =>
            throw new InvalidOperationException();
    }
}