using Microsoft.CodeAnalysis;
using MVVMGenerators.Generators.Ids.Data;

namespace MVVMGenerators.Generators.ViewModels.Data.Members;

public abstract class BindableMember(
    ISymbol member,
    BindMode mode,
    string sourceName,
    string generatedName,
    string idPostfix = "")
{
    public readonly IdData Id = new(member, idPostfix);
    public readonly BindMode Mode = mode;
    public readonly ISymbol Member = member;

    public readonly string SourceName = sourceName;
    public readonly string GeneratedName = generatedName;
    
    public abstract string Type { get; }

}