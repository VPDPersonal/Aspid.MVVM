using System.Text;
using Microsoft.CodeAnalysis;

namespace MVVMGenerators.Extensions.Symbols;

public static class MethodSymbolExtensions
{
    public static string GetParametersAsText(this IMethodSymbol symbol)
    {
        var text = new StringBuilder();

        foreach (var parameter in symbol.Parameters)
        {
            if (text.Length > 0)
                text.Append(", ");

            text.Append(parameter.ContainingType.ToDisplayString())
                .Append(" ")
                .Append(parameter.Name);
        }

        return text.ToString();
    }
}