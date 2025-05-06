namespace Aspid.MVVM.StarterKit
{
    public sealed class OneWayDynamicProperty<T> : IDynamicProperty
    {
        private readonly T _value;
        private OneWayViewModelEvent<T>? _event;
        
        public OneWayDynamicProperty(T value)
        {
            _value = value;
        }

        public IViewModelEventAdder GetAdder()
        {
            _event ??= new OneWayViewModelEvent<T>();
            return BindableMember<T>.OneWay(_event, _value);
        }
    }
}