namespace Aspid.MVVM
{
    public readonly struct BindableMember<T> : IViewModelEventAdder
    {
        private readonly T? _value;
        private readonly bool _isDefault;
        private readonly IViewModelEventAdder? _viewModelEvent;

        private BindableMember(T? value)
        {
            _value = value;
            _isDefault = false;
            _viewModelEvent = null;
        }
        
        private BindableMember(IViewModelEventAdder viewModelEvent)
        {
            _value = default;
            _isDefault = false;
            _viewModelEvent = viewModelEvent;
        }
        
        private BindableMember(IViewModelEventAdder viewModelEvent, T? value)
        {
            _value = value;
            _isDefault = true;
            _viewModelEvent = viewModelEvent;
        }
        
        public IViewModelEventRemover? AddBinder(IBinder binder)
        {
            if (_isDefault && binder.Mode is not BindMode.OneWayToSource)
                binder.Cast<T>().SetValue(_value);
            
            return binder.Mode is not BindMode.OneTime 
                ? _viewModelEvent?.AddBinder(binder) 
                : null;
        }

        public static BindableMember<T> OneTime(T? value) =>
            new(value);
        
        public static BindableMember<T> OneWay(IViewModelEventAdder viewModelEvent, T? value) =>
            new(viewModelEvent, value);
        
        public static BindableMember<T> TwoWay(IViewModelEventAdder viewModelEvent, T? value) =>
            new(viewModelEvent, value);
        
        public static BindableMember<T> OneWayToSource(IViewModelEventAdder viewModelEvent) =>
            new(viewModelEvent);
    }
}