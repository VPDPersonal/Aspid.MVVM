// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Specifies the direction of data flow between the ViewModel and the View.
    /// </summary>
    public enum BindMode
    {
        /// <summary>
        /// No binding; every bindable member rejects it.
        /// </summary>
        None = 0,
        
        /// <summary>
        /// Pushes every ViewModel change to the View.
        /// </summary>
        OneWay = 1,
        
        /// <summary>
        /// Pushes ViewModel changes to the View and View changes to the ViewModel.
        /// </summary>
        TwoWay = 2,
        
        /// <summary>
        /// Pushes the ViewModel value to the View once, on bind.
        /// </summary>
        OneTime = 3,
        
        /// <summary>
        /// Pushes View changes to the ViewModel.
        /// </summary>
        OneWayToSource = 4,
    }
}
