namespace Aspid.MVVM
{
    /// <summary>
    /// Represents the binding mode that determines the direction of data flow between the ViewModel and the View.
    /// </summary>
    public enum BindMode
    {
        /// <summary>
        /// No binding is applied. This is the default value.
        /// </summary>
        None = 0,
        
        /// <summary>
        /// One-way binding: Updates from the ViewModel to the View are propagated.
        /// Changes in the View do not affect the ViewModel.
        /// </summary>
        OneWay = 1,
        
        /// <summary>
        /// Two-way binding: Updates are propagated in both directions.
        /// Changes in the View are reflected in the ViewModel, and vice versa.
        /// </summary>
        TwoWay = 2,
        
        /// <summary>
        /// One-time binding: The value is propagated from the ViewModel to the View only once.
        /// Subsequent changes in the ViewModel do not affect the View.
        /// </summary>
        OneTime = 3,
        
        /// <summary>
        /// One-way to source binding: Updates from the View to the ViewModel are propagated.
        /// Changes in the ViewModel do not affect the View.
        /// </summary>
        OneWayToSource = 4,
    }
}