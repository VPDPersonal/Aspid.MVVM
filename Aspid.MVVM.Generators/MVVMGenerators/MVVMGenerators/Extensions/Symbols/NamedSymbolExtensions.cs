using System.Linq;
using Microsoft.CodeAnalysis;

namespace MVVMGenerators.Extensions.Symbols;

public static class NamedSymbolExtensions
{
    public static bool HasInterface(this INamedTypeSymbol symbol, string interfaceName) =>
        symbol.AllInterfaces.Any(i => i.ToDisplayString() == interfaceName);
}