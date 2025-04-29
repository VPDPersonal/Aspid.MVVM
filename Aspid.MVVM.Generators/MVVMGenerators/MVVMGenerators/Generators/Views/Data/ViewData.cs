using Microsoft.CodeAnalysis.CSharp.Syntax;
using MVVMGenerators.Generators.Views.Data.Members;

namespace MVVMGenerators.Generators.Views.Data;

public readonly struct ViewData(
    Inheritor inheritor, 
    ViewMembers members,
    TypeDeclarationSyntax declaration)
{
    public readonly ViewMembers Members = members;
    public readonly Inheritor Inheritor = inheritor;
    public readonly TypeDeclarationSyntax Declaration = declaration;
}