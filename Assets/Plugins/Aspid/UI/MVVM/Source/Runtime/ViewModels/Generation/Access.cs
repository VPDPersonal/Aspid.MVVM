namespace Aspid.UI.MVVM.ViewModels.Generation
{
    /// <summary>
    /// Defines access modifiers for generated properties marked with the <see cref="BindAttribute"/> or <see cref="ReadOnlyBindAttribute"/>.
    /// Each modifier value corresponds to a value from Microsoft.CodeAnalysis.CSharp.SyntaxKind.
    /// </summary>
    public enum Access
    {
        Private = 8344,
        Protected = 8346,
        Public = 8343,
    }
}