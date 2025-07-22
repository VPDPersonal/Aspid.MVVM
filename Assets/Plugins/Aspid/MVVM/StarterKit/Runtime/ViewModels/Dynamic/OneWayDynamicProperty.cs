namespace Aspid.MVVM.StarterKit
{
    public sealed class OneWayDynamicProperty<T> : IDynamicProperty 
    {
        private readonly OneWayBindableMember<T> _binder;
        
        public OneWayDynamicProperty(T value)
        {
            _binder = new OneWayBindableMember<T>(value);
        }

        public IBinderAdder GetAdder() =>
            _binder;
    }
}