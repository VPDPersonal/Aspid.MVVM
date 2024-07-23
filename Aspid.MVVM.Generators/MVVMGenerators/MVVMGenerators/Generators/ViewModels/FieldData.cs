using Microsoft.CodeAnalysis;

namespace MVVMGenerators.Generators.ViewModels;

public readonly struct FieldData(IFieldSymbol field, int getAccess, int setAccess)
{
    public readonly IFieldSymbol Field = field;
    
    public readonly int SetAccess = setAccess;
    public readonly int GetAccess = getAccess;
}