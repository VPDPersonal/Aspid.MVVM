// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Specifies when a value written by a binder raises the change event of the bound UI component.
    /// </summary>
    /// <remarks>
    /// User input raises the event in every mode.
    /// </remarks>
    public enum NotifyMode
    {
        /// <summary>
        /// Every write raises the event.
        /// </summary>
        Always,

        /// <summary>
        /// The write made while binding is silent; later writes from the ViewModel raise the event.
        /// </summary>
        AfterBind,

        /// <summary>
        /// No write raises the event.
        /// </summary>
        Never,
    }
}
