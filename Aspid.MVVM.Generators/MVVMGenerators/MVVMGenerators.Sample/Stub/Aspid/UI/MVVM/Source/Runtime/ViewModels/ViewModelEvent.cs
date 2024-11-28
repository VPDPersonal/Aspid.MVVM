using System;

namespace Aspid.UI.MVVM.ViewModels
{
    // TODO None sealed
    public sealed class ViewModelEvent<T> : IRemoveBinderFromViewModel
    {
        public event Action<T>? Changed;
        
        public Action<T>? SetValue { get; set; }

        public IRemoveBinderFromViewModel AddBinder(IBinder binder, T? value, bool isReverse)
        {
            var isBind = false;
            
            if (binder is IBinder<T> specificBinder)
            {
                isBind = true;
                specificBinder.SetValue(value);
                Changed += specificBinder.SetValue;
            }

            if (isReverse && binder is IReverseBinder<T> specificReverseBinder)
            {
                if (SetValue is null) throw new ArgumentNullException();
                
                specificReverseBinder.ValueChanged += SetValue;
                return this;
            }

            if (!isBind)
            {
                throw new InvalidOperationException();
            }
            
            return this;
        }

        public void RemoveBinder(IBinder binder)
        {
            var isUnbind = false;

            if (binder is IBinder<T> specificBinder)
            {
                isUnbind = true;
                Changed -= specificBinder.SetValue;
            }

            if (binder.IsReverseEnabled && binder is IReverseBinder<T> specificReverseBinder)
            {
                if (SetValue is null) throw new ArgumentNullException();

                specificReverseBinder.ValueChanged -= SetValue;
                return;
            }
            
            if (!isUnbind)
            {
                throw new InvalidOperationException();
            }
        }
        
        public void Invoke(T value) => Changed?.Invoke(value);
    }
}