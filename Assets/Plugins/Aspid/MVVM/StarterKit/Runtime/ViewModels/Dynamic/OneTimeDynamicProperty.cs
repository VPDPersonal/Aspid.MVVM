namespace Aspid.MVVM.StarterKit
{
    public sealed class OneTimeDynamicProperty<T> : IDynamicProperty
    {
        private readonly T _value;
        
        public OneTimeDynamicProperty(T value)
        {
            _value = value;
        }

        public BindResult AddBinder(IBinder binder)
        {
            if (binder.Mode is BindMode.TwoWay or BindMode.OneWayToSource)
                throw new System.Exception();
				    
            if (binder is not IBinder<T> specificBinder)
                throw new System.Exception($"Binder ({binder.GetType()}) is not {typeof(IBinder<IRelayCommand>)}");
				    
            specificBinder.SetValue(_value);
            return new BindResult(true);
        }
    }
}