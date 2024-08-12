using Microsoft.CodeAnalysis;

namespace MVVMGenerators.Generators.Views.Data.Members;

public interface IMember
{
    public string Id { get; }
    
    public string Name { get; }
    
    public ITypeSymbol? Type { get; }
}