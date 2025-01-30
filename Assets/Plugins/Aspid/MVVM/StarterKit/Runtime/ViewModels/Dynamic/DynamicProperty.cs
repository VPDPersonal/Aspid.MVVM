namespace Aspid.MVVM.StarterKit.ViewModels
{
    public sealed class DynamicProperty<T> : IDynamicProperty
    {
        private readonly T _value;
        private ViewModelEvent<T> _event;
        
        public DynamicProperty(T value)
        {
            _value = value;
        }

        public BindResult AddBinder(IBinder binder)
        {
            _event ??= new ViewModelEvent<T>();
            return new BindResult(_event.AddBinder(binder, _value, false));
        }
    }
}