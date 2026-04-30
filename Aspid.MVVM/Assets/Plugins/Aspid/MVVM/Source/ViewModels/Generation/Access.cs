// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Defines access modifiers for generated properties marked with the <see cref="BindAttribute"/> or <see cref="OneWayBindAttribute"/>
    /// or <see cref="TwoWayBindAttribute"/> or <see cref="OneTimeBindAttribute"/> or or <see cref="OneWayToSourceBindAttribute"/>
    /// Each modifier value corresponds to a value from Microsoft.CodeAnalysis.CSharp.SyntaxKind.
    /// </summary>
    public enum Access
    {
        /// <summary>
        /// The generated property has <c>private</c> access.
        /// </summary>
        Private = 8344,
        
        /// <summary>
        /// The generated property has <c>protected</c> access.
        /// </summary>
        Protected = 8346,
        
        /// <summary>
        /// The generated property has <c>public</c> access.
        /// </summary>
        Public = 8343,
    }
}