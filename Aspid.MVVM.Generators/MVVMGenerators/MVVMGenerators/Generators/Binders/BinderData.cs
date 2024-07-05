using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MVVMGenerators.Generators.Binders;

public readonly struct BinderData(
    TypeDeclarationSyntax declaration, 
    bool hasBinderLogInBaseType,
    IReadOnlyCollection<IMethodSymbol> binderLogMethods,
    bool hasBindInheritorsAlsoInBaseType,
    IReadOnlyCollection<ITypeSymbol> bindInheritorsAlsoTypes)
{
    public readonly TypeDeclarationSyntax Declaration = declaration;

    public readonly bool HasBinderLogInBaseType = hasBinderLogInBaseType;
    public readonly IReadOnlyCollection<IMethodSymbol> BinderLogMethods = binderLogMethods;

    public readonly bool HasBindInheritorsAlsoInBaseType = hasBindInheritorsAlsoInBaseType;
    public readonly IReadOnlyCollection<ITypeSymbol> BindInheritorsAlsoTypes = bindInheritorsAlsoTypes;
}