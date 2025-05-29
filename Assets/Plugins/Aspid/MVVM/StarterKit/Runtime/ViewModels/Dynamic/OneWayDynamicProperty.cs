namespace Aspid.MVVM.StarterKit
{
    public sealed class OneWayDynamicProperty<T> : IDynamicProperty 
    {
        private readonly OneWayClassEvent<T> _event;
        
        public OneWayDynamicProperty(T value)
        {
            _event = new OneWayClassEvent<T>(value);
        }

        public IBindableMemberEventAdder GetAdder() =>
            _event;
    }
}