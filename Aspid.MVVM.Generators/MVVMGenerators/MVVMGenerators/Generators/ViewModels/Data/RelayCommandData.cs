using Microsoft.CodeAnalysis;

namespace MVVMGenerators.Generators.ViewModels.Data;

public readonly struct RelayCommandData(IMethodSymbol executeMethod, IMethodSymbol? canExecuteMethod = null)
{
    public readonly IMethodSymbol ExecuteMethod = executeMethod;
    public readonly IMethodSymbol? CanExecuteMethod = canExecuteMethod;
}