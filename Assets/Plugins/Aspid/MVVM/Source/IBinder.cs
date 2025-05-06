namespace Aspid.MVVM
{
    /// <summary>
    /// Interface for binding a component with a <see cref="IViewModel"/>
    /// to provide data binding functionality without the ability to set values.
    /// </summary>
    public interface IBinder
    {
        /// <summary>
        /// Gets the binding mode that determines the direction of data flow.
        /// Default is <see cref="BindMode.OneWay"/>.
        /// </summary>
        public BindMode Mode => BindMode.OneWay;

        public void Bind<T>(BindableMember<T> bindableMember);

        public void Bind(IViewModelEventAdder viewModelEventAdder);
        
        /// <summary>
        /// Unbinds the component from the bound s<see cref="IViewModel"/>.
        /// </summary>
        public void Unbind();
    }
    
    /// <summary>
    /// Interface for binding a component with a <see cref="IViewModel"/>
    /// to provide data binding functionality with value setting capability.
    /// </summary>
    /// <typeparam name="T">The type of value that will be set.</typeparam>
    public interface IBinder<in T> : IBinder
    {
        /// <summary>
        /// Sets the value for the bound component.
        /// </summary>
        /// <param name="value">The value to be set.</param>
        public void SetValue(T? value);
    }
}