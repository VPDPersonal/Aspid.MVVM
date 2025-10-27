using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Aspid.MVVM.Generators.Helpers;

public readonly struct MembersByGroup
{
    public readonly ImmutableArray<ISymbol> All;
    public readonly ImmutableArray<ISymbol> Others;
    public readonly ImmutableArray<IFieldSymbol> Fields;
    public readonly ImmutableArray<IMethodSymbol> Methods;
    public readonly ImmutableArray<IPropertySymbol> Properties;

    public MembersByGroup(INamespaceOrTypeSymbol symbol)
        : this(symbol.GetMembers()) { }

    public MembersByGroup(ImmutableArray<ISymbol> members)
    {
        All = members;

        var others = new List<ISymbol>();
        var fields = new List<IFieldSymbol>();
        var methods = new List<IMethodSymbol>();
        var properties = new List<IPropertySymbol>();
        
        foreach (var member in members)
        {
            switch (member)
            {
                case IFieldSymbol field:
                    fields.Add(field);
                    break;
                
                case IMethodSymbol method:
                    methods.Add(method);
                    break;
                
                case IPropertySymbol property:
                    properties.Add(property);
                    break;
                
                default:
                    others.Add(member);
                    break;
            }
        }

        Others = others.ToImmutableArray();
        Fields = fields.ToImmutableArray();
        Methods = methods.ToImmutableArray();
        Properties = properties.ToImmutableArray();
    }
}