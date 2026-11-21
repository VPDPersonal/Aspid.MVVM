using Microsoft.CodeAnalysis;

namespace Aspid.MVVM.Generators.Generators.ViewModels.Data.Members;

public readonly struct CustomViewModelInterface(string id, IPropertySymbol property, ITypeSymbol @interface)
{
    public readonly string Id = id;
    public readonly ITypeSymbol Interface = @interface;
    public readonly IPropertySymbol Property = property;
}