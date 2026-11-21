using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Aspid.MVVM.Unity.Generators.ContextMenu;

public readonly struct ContextMenuData(
    ClassDeclarationSyntax declaration, 
    ISymbol symbol, 
    string name,
    string? type,
    string? path,
    int priority)
{
    public readonly string Name = name;
    public readonly string? Type = type;
    public readonly string? Path = path;
    public readonly int Priority = priority;
    public readonly ISymbol Symbol = symbol;
    public readonly ClassDeclarationSyntax Declaration = declaration;
}