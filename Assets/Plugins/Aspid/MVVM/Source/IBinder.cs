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

        /// <summary>
        /// Binds the component using the specified <see cref="IBindableMemberEventAdder"/>.
        /// </summary>
        /// <param name="bindableMemberEventAdder">The event adder for the component to bind to.</param>
        public void Bind(IBindableMemberEventAdder bindableMemberEventAdder);
        
        /// <summary>
        /// Unbinds the component from the bound <see cref="IViewModel"/>.
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