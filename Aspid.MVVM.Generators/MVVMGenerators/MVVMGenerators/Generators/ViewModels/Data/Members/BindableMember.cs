using Microsoft.CodeAnalysis;
using MVVMGenerators.Generators.Ids.Data;
using MVVMGenerators.Helpers.Extensions.Symbols;

namespace MVVMGenerators.Generators.ViewModels.Data.Members;

public class BindableMember(
    ISymbol member,
    BindMode mode,
    string type,
    string sourceName,
    string generatedName,
    string idPostfix = "")
{
    public readonly IdData Id = new(member, idPostfix);
    public readonly BindMode Mode = mode;
    public readonly ISymbol Member = member;

    public readonly string Type = type;
    public readonly string SourceName = sourceName;
    public readonly string GeneratedName = generatedName;
    public readonly ViewModelEvent Event = new(mode, generatedName, type);


    public virtual bool IsValueType { get; } = member.GetSymbolType()?.IsValueType ?? false;
}