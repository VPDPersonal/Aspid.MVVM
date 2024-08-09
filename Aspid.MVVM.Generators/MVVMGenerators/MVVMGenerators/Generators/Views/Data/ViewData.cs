using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MVVMGenerators.Generators.Views.Data;

public readonly struct ViewData(
    Inheritor inheritor, 
    TypeDeclarationSyntax declaration,
    IFieldSymbol[] viewFields,
    IFieldSymbol[] binderFields,
    AsBinderMember<IFieldSymbol>[] asBinderFields,
    IPropertySymbol[] viewProperties, 
    IPropertySymbol[] binderProperties, 
    AsBinderMember<IPropertySymbol>[] asBinderProperties)
{
    public readonly Inheritor Inheritor = inheritor;
    public readonly TypeDeclarationSyntax Declaration = declaration;

    public readonly IFieldSymbol[] ViewFields = viewFields;
    public readonly IFieldSymbol[] BinderFields = binderFields;
    public readonly AsBinderMember<IFieldSymbol>[] AsBinderFields = asBinderFields;
    
    public readonly IPropertySymbol[] ViewProperties = viewProperties;
    public readonly IPropertySymbol[] BinderProperties = binderProperties;
    public readonly AsBinderMember<IPropertySymbol>[] AsBinderProperties = asBinderProperties;
}