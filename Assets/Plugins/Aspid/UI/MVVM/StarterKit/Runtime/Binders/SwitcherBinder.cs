namespace Aspid.UI.MVVM.StarterKit.Binders
{
    public abstract class SwitcherBinder<T> : Binder, IBinder<bool>
    {
        private readonly T _trueValue;
        private readonly T _falseValue;

        protected SwitcherBinder(T trueValue, T falseValue)
        {
            _trueValue = trueValue;
            _falseValue = falseValue;
        }

        public void SetValue(bool value) =>
            SetValue(GetValue(value));

        protected abstract void SetValue(T value);
        
        private T GetValue(bool value) =>
            value ? _trueValue : _falseValue;
    }
}