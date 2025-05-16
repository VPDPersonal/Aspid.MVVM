using Microsoft.CodeAnalysis;

namespace MVVMGenerators.Generators.ViewModels.Data.Members;

public readonly struct CustomViewModelInterface(string id, string propertyName, ITypeSymbol @interface)
{
    public readonly string Id = id;
    public readonly string PropertyName = propertyName;
    public readonly ITypeSymbol Interface = @interface;
}