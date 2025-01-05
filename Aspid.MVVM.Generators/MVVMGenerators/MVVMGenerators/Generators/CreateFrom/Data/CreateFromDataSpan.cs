using System;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers.Extensions.Symbols;

namespace MVVMGenerators.Generators.CreateFrom.Data;

public readonly ref struct CreateFromDataSpan(CreateFromData data)
{
    public readonly CreateFromData Data = data;
    public readonly IMethodSymbol Constructor = data.Constructor;

    public readonly ITypeSymbol ToType = data.Constructor.ContainingType;
    public readonly string ToName = data.Declaration.Identifier.Text;
    public readonly string ToTypeName = data.Constructor.ContainingType.ToDisplayStringGlobal();
    
    public readonly ITypeSymbol FromType = data.FromType;
    public readonly string FromTypeName = data.FromType.ToDisplayStringGlobal();
    
    public readonly string MethodName = $"To{data.Declaration.Identifier.Text}";
    public readonly bool CanBeInherited = data.FromType.TypeKind != TypeKind.Struct && !data.FromType.IsSealed;
}