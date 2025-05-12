using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace MVVMGenerators.Generators.ViewModels.Data;

public readonly struct BindableMembersInterfaceData(INamedTypeSymbol @interface, HashSet<string> names)
{
    public readonly HashSet<string> Names = names;
    public readonly INamedTypeSymbol Interface = @interface;
}