using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Helpers.Extensions.Symbols;
using MVVMGenerators.Generators.ViewModels.Data;

namespace MVVMGenerators.Generators.ViewModels.Extensions;

public static class SymbolExtensions
{
    public static BindMode GetBindMode(this ISymbol member)
    {
        if (member.HasAttribute(Classes.BindAttribute, out var bindAttribute))
        {
            if (bindAttribute!.ConstructorArguments.Length is 0)
            {
                return member is IFieldSymbol { IsReadOnly: true } 
                    ? BindMode.OneTime 
                    : BindMode.TwoWay;
            }

            return Determine((BindMode)(int)bindAttribute!.ConstructorArguments[0].Value!);
        }
        
        if (member.HasAttribute(Classes.OneWayBindAttribute))
            return Determine(BindMode.OneWay);
        
        if (member.HasAttribute(Classes.TwoWayBindAttribute))
            return Determine(BindMode.TwoWay);
        
        if (member.HasAttribute(Classes.OneTimeBindAttribute))
            return Determine(BindMode.OneTime);
        
        if (member.HasAttribute(Classes.OneWayToSourceBindAttribute))
            return Determine(BindMode.OneWayToSource);
        
        return BindMode.None;

        BindMode Determine(BindMode current)
        {
            switch (member)
            {
                case IFieldSymbol field:
                    {
                        if (field.IsReadOnly && current is not BindMode.OneTime) return BindMode.None;
                        break;
                    }
            }

            return current;
        }
    }
}