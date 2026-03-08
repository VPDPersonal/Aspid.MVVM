// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Identifies a single component of a <see cref="UnityEngine.Color"/> value.
    /// Used by graphic color-component binders to specify which channel to read or write.
    /// </summary>
    public enum ColorComponent
    {
        /// <summary>
        /// The red channel.
        /// </summary>
        R,
        
        /// <summary>
        /// The green channel.
        /// </summary>
        G,
        
        /// <summary>
        /// The blue channel.
        /// </summary>
        B,
        
        /// <summary>
        /// The alpha (opacity) channel.
        /// </summary>
        A,
    }
}