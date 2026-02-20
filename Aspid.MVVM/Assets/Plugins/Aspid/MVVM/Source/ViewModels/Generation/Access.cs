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
        Private = 8344,
        Protected = 8346,
        Public = 8343,
    }
}