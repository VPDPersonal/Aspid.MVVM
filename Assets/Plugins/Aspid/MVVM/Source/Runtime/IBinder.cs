namespace Aspid.MVVM
{
    /// <summary>
    /// Interface for binding a component with a <see cref="IViewModel"/>
    /// to provide data binding functionality without the ability to set values.
    /// </summary>
    public interface IBinder
    {
        /// <summary>
        /// Determines whether reverse binding (from View to ViewModel) is enabled.
        /// The default value is <c>false</c>.
        /// </summary>
        public bool IsReverseEnabled => false;
        
        /// <summary>
        /// Binds a component using the specified binding parameters.
        /// </summary>
        /// <param name="parameters">
        /// The parameters that contain the ViewModel and the component ID for binding, where the component ID matches
        /// the property name in the ViewModel.
        /// </param>
        public void Bind(in BindParameters parameters);
        
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