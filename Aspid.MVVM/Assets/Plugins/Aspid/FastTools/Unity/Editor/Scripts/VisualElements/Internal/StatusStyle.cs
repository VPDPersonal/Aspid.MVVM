// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// Defines the visual status of an Aspid UI element, controlling its color accent.
    /// </summary>
    public enum StatusStyle
    {
        /// <summary>
        /// No status applied.
        /// </summary>
        None,
        
        /// <summary>
        /// Indicates an informational state.
        /// </summary>
        Info,
        
        /// <summary>
        /// Indicates a warning or cautionary state.
        /// </summary>
        Warning,

        /// <summary>
        /// Indicates an error or critical state.
        /// </summary>
        Error,
        
        /// <summary>
        /// Indicates a successful or positive state.
        /// </summary>
        Success,
    }
}
