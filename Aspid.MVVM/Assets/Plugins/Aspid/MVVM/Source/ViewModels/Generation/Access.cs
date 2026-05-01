// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Defines access modifiers for properties generated from fields decorated with <see cref="BindAttribute"/>,
    /// <see cref="OneWayBindAttribute"/>, <see cref="TwoWayBindAttribute"/>, <see cref="OneTimeBindAttribute"/>,
    /// or <see cref="OneWayToSourceBindAttribute"/>.
    /// Each value corresponds to a value from <c>Microsoft.CodeAnalysis.CSharp.SyntaxKind</c>.
    /// </summary>
    public enum Access
    {
        /// <summary>
        /// The generated property has <see langword="private"/> access.
        /// </summary>
        Private = 8344,
        
        /// <summary>
        /// The generated property has <see langword="protected"/> access.
        /// </summary>
        Protected = 8346,
        
        /// <summary>
        /// The generated property has <see langword="public"/> access.
        /// </summary>
        Public = 8343,
    }
}
