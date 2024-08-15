using Microsoft.CodeAnalysis.CSharp.Syntax;
using MVVMGenerators.Generators.Views.Data.Members;

namespace MVVMGenerators.Generators.Views.Data;

public readonly struct ViewData(
    Inheritor inheritor, 
    MembersContainer members,
    TypeDeclarationSyntax declaration)
{
    public readonly Inheritor Inheritor = inheritor;
    public readonly MembersContainer Members = members;
    public readonly TypeDeclarationSyntax Declaration = declaration;
}