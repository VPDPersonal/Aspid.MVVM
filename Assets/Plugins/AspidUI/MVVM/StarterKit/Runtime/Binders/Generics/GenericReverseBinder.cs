using System;

namespace AspidUI.MVVM.StarterKit.Binders.Generics
{
    public sealed class GenericReverseBinder<T> : Binder, IBinder<T>, IReverseBinder<T>, IDisposable
    {
        public event Action<T> ValueChanged;

        private readonly Action<T> _setValue;
        private readonly Action<Action<T>> _disposable;

        public GenericReverseBinder(Action<Action<T>> initialize, Action<Action<T>> disposable, Action<T> setValue = null)
        {
            _setValue = setValue;
            _disposable = disposable;
            
            initialize.Invoke(OnValueChanged);
        }

        public void SetValue(T value) =>
            _setValue?.Invoke(value);

        private void OnValueChanged(T value) =>
            ValueChanged?.Invoke(value);

        public void Dispose() => _disposable?.Invoke(OnValueChanged);
    }
}