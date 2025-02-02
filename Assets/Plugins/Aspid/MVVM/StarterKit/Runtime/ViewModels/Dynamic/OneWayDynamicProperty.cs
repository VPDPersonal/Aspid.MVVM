namespace Aspid.MVVM.StarterKit.ViewModels
{
    public sealed class OneWayDynamicProperty<T> : IDynamicProperty
    {
        private readonly T _value;
        private OneWayViewModelEvent<T> _event;
        
        public OneWayDynamicProperty(T value)
        {
            _value = value;
        }

        public BindResult AddBinder(IBinder binder)
        {
            _event ??= new OneWayViewModelEvent<T>();
            return new BindResult(_event.AddBinder(binder, _value));
        }
    }
}