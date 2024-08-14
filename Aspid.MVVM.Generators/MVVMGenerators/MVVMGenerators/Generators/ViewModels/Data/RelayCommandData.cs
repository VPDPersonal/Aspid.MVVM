using Microsoft.CodeAnalysis;

namespace MVVMGenerators.Generators.ViewModels.Data;

public readonly struct RelayCommandData(
    IMethodSymbol execute, 
    string? canExecuteName = null,
    bool isMethod = false,
    bool isLambda = false)
{
    public readonly IMethodSymbol Execute = execute;
    
    public readonly bool IsLambda = isLambda;
    public readonly bool IsMethod = isMethod;
    public readonly string? CanExecuteName = canExecuteName;
}