namespace Aspid.UI.MVVM.ViewModels.Generation
{
    /// <summary>
    /// Определяет модификаторы доступа для сгенерированных свойств, отмеченных атрибутам <see cref="BindAttribute"/> или <see cref="ReadOnlyBindAttribute"/>
    /// Значение каждого модификатора соответствует значению из Microsoft.CodeAnalysis.CSharp.SyntaxKind.
    /// </summary>
    public enum Access
    {
        Private = 8344,
        Protected = 8346,
        Public = 8343,
    }
}