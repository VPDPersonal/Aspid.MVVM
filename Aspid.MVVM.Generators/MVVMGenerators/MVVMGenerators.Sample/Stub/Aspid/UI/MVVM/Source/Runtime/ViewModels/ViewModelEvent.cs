using System;

namespace Aspid.UI.MVVM.ViewModels
{
    public sealed class ViewModelEvent<T> : IRemoveBinderFromViewModel
    {
        public event Action<T>? Changed;
        
        public Action<T>? SetValue { get; set; }

        public IRemoveBinderFromViewModel AddBinder(IBinder binder, T? value, bool isReverse = false)
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
                if (IsReverseEnabled(specificReverseBinder))
                {
                    isBind = true;
                    specificReverseBinder.ValueChanged += SetValue;
                }
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

            if (binder is IReverseBinder<T> specificReverseBinder)
            {
                if (IsReverseEnabled(specificReverseBinder))
                {
                    isUnbind = true;
                    specificReverseBinder.ValueChanged -= SetValue;
                }
            }
            
            if (!isUnbind)
            {
                throw new InvalidOperationException();
            }
        }
        
        public void Invoke(T value) => Changed?.Invoke(value);
        
        private bool IsReverseEnabled(IReverseBinder<T> specificReverseBinder)
        {
            var result = specificReverseBinder.IsReverseEnabled;
            if (result && SetValue is null) throw new ArgumentNullException();

            return result;
        }
    }
}