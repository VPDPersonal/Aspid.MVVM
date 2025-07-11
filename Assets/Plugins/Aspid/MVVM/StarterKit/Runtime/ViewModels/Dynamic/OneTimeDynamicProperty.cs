namespace Aspid.MVVM.StarterKit
{
    public sealed class OneTimeDynamicProperty<T> : IDynamicProperty 
    {
        private readonly T _value;
        
        public OneTimeDynamicProperty(T value)
        {
            _value = value;
        }

        public IBinderAdder GetAdder() =>
            new OneTimeBindableMember<T>(_value);
    }
}