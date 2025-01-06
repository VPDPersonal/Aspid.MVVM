using System;
using UnityEngine;
using Aspid.MVVM.Mono;
using System.Runtime.CompilerServices;

namespace Aspid.MVVM
{
    [Serializable]
    public sealed class BinderArray<T> : Binder, IBinder<T>, IReverseBinder<T>, IViewModel
    {
        public event Action<T>? ValueChanged;

        [SerializeField] private MonoBinder[] _binders;
        
        private string? _id;
        private bool _isInitialized;
        private RemoveBinderFromViewModel? _removeBinderFromViewModel;

        protected override void OnBinding(IViewModel viewModel, string id) => _id = id;

        protected override void OnUnbound() => _id = null;

        public bool IsReverseEnabled => _removeBinderFromViewModel is not null;

        IRemoveBinderFromViewModel? IViewModel.AddBinder(IBinder binder, string _)
        {
            if (!binder.IsReverseEnabled) return null;
            
            _removeBinderFromViewModel ??= new RemoveBinderFromViewModel(this);
            _removeBinderFromViewModel.AddBinder(binder);
            
            return _removeBinderFromViewModel;
        }

        void IBinder<T>.SetValue(T? value)
        {
            if (!_isInitialized)
            {
                foreach (var binder in _binders)
                {
                    binder.Bind(this, _id!);
                    SetValue(binder, value);
                }
                
                _isInitialized = true;
            }
            else
            {
                foreach (var binder in _binders)
                    SetValue(binder, value);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SetValue(IBinder monoBinder, T? value) =>
            ((IBinder<T>)monoBinder).SetValue(value);

        private void OnValueChanged(T value) => ValueChanged?.Invoke(value);
        
        private sealed class RemoveBinderFromViewModel : IRemoveBinderFromViewModel
        {
            private readonly BinderArray<T> _array;

            public RemoveBinderFromViewModel(BinderArray<T> array)
            {
                _array = array;
            }

            public void AddBinder(IBinder binder)
            {
                if (binder is not IReverseBinder<T> reverseBinder) return;
                reverseBinder.ValueChanged += _array.OnValueChanged;
            }

            public void RemoveBinder(IBinder binder)
            {
                if (binder is not IReverseBinder<T> reverseBinder) return;
                reverseBinder.ValueChanged -= _array.OnValueChanged;
            }
        }
    }
}