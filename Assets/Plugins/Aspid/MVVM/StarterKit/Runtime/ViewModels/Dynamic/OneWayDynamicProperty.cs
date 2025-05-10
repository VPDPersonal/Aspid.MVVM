namespace Aspid.MVVM.StarterKit
{
    public sealed class OneWayDynamicProperty<T> : IDynamicProperty
    {
        private readonly OneWayBindableMemberEvent<T> _event;
        
        public OneWayDynamicProperty(T value)
        {
            _event = new OneWayBindableMemberEvent<T>(value);
        }

        public IBindableMemberEventAdder GetAdder() =>
            _event;
    }
}