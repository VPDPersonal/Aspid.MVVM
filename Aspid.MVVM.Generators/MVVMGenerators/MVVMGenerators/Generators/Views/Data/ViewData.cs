using Microsoft.CodeAnalysis.CSharp.Syntax;
using MVVMGenerators.Generators.Views.Data.Members;

namespace MVVMGenerators.Generators.Views.Data;

public readonly struct ViewData(
    Inheritor inheritor, 
    MembersContainer members,
    bool isInitializeOverride,
    bool isDeinitializeOverride,
    TypeDeclarationSyntax declaration)
{
    public readonly bool IsInitializeOverride = isInitializeOverride;
    public readonly bool IsDeinitializeOverride = isDeinitializeOverride;
    
    public readonly Inheritor Inheritor = inheritor;
    public readonly MembersContainer Members = members;
    public readonly TypeDeclarationSyntax Declaration = declaration;
}