// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Specifies the access modifier of an accessor of a property generated for a bind field.
    /// </summary>
    /// <remarks>
    /// The values match <c>Microsoft.CodeAnalysis.CSharp.SyntaxKind</c>.
    /// </remarks>
    public enum Access
    {
        /// <summary>
        /// The <see langword="private"/> modifier.
        /// </summary>
        Private = 8344,
        
        /// <summary>
        /// The <see langword="protected"/> modifier.
        /// </summary>
        Protected = 8346,
        
        /// <summary>
        /// The <see langword="public"/> modifier.
        /// </summary>
        Public = 8343,
    }
}
