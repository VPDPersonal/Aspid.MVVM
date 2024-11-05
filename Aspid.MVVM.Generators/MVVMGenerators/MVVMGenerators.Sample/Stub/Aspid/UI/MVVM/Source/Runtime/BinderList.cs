using System;
using Aspid.UI.MVVM.ViewModels;
using System.Collections.Generic;

namespace Aspid.UI.MVVM
{
    public sealed class BinderList<T, TBinder> : Binder, IBinder<T>, IViewModel
        where TBinder : IBinder<T>
    {
        private event Action<T>? Changed;
        
        private readonly List<TBinder> _binders;
        
        public bool IsReverseEnabled { get; private set; }

        public BinderList(List<TBinder> binders)
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