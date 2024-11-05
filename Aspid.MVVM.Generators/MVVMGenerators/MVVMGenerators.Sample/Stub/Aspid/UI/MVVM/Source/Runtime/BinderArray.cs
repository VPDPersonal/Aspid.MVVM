using System;
using Aspid.UI.MVVM.ViewModels;

namespace Aspid.UI.MVVM
{
    public sealed class BinderArray<T, TBinder> : Binder, IBinder<T>, IReverseBinder<T>, IViewModel
        where TBinder : IBinder<T>
    {
        public event Action<T>? ValueChanged;
        
        private event Action<T>? Changed;
        
        private readonly TBinder[] _binders;

        public bool IsReverseEnabled { get; private set; }

        public BinderArray(TBinder[] binders)
        {
            _binders = binders;
        }

        void IBinder<T>.SetValue(T value) => Changed?.Invoke(value);

        protected override void OnBinding(IViewModel viewModel, string id)
        {
            foreach (var binder in _binders)
            {
                if (!binder.IsReverseEnabled)
                {
                    IsReverseEnabled = false;
                    return;
                }
            }

            IsReverseEnabled = true;
        }

        protected override void OnBound(IViewModel viewModel, string id)
        {
            foreach (var binder in _binders)
                binder.Bind(this, id);
        }

        protected override void OnUnbound(IViewModel viewModel, string id)
        {
            foreach (var binder in _binders)
                binder.Unbind(this, id);
        }

        void IViewModel.AddBinder(IBinder binder, string propertyName) =>
            Changed += ((IBinder<T>)binder).SetValue;

        void IViewModel.RemoveBinder(IBinder binder, string propertyName) =>    
            Changed -= ((IBinder<T>)binder).SetValue;
    }
}