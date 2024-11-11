using System;

namespace Aspid.UI.MVVM.ViewModels
{
    public sealed class ViewModelEvent<T> : IRemoveBinderFromViewModel
    {
        public event Action<T>? Changed;
        
        public Action<T>? SetValue { get; set; }

        public IRemoveBinderFromViewModel AddBinder(IBinder binder, T? value)
        {
            var specificBinder = binder as IBinder<T>;
            var specificReverseBinder = binder as IReverseBinder<T>;
            ThrowErrorIfInvalidOperation(specificBinder, specificReverseBinder);

            if (specificBinder is not null)
            {
                specificBinder.SetValue(value);
                Changed += specificBinder.SetValue;
            }
            
            if (IsReverseEnabled(specificReverseBinder))
                specificReverseBinder!.ValueChanged -= SetValue;

            return this;
        }

        public void RemoveBinder(IBinder binder)
        {
            var specificBinder = binder as IBinder<T>;
            var specificReverseBinder = binder as IReverseBinder<T>;
            ThrowErrorIfInvalidOperation(specificBinder, specificReverseBinder);
            
            if (specificBinder is not null)
                Changed -= specificBinder.SetValue;

            if (IsReverseEnabled(specificReverseBinder))
                specificReverseBinder!.ValueChanged -= SetValue;
        }
        
        public void Invoke(T value) => Changed?.Invoke(value);
        
        private bool IsReverseEnabled(IReverseBinder<T>? reverseBinder)
        {
            var result = reverseBinder is not null && reverseBinder.IsReverseEnabled;
            if (result && SetValue is null) throw new ArgumentNullException();

            return result;
        }

        private static void ThrowErrorIfInvalidOperation(IBinder<T>? binder, IReverseBinder<T>? reverseBinder)
        {
            if (binder is null && (reverseBinder is null || !reverseBinder.IsReverseEnabled)) 
                throw new InvalidOperationException();
        }
    }
}