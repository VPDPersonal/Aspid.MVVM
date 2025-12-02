using Microsoft.CodeAnalysis;
using Aspid.MVVM.Generators.Generators.Ids.Data;
using Aspid.MVVM.Generators.Generators.ViewModels.Data.Infos;

namespace Aspid.MVVM.Generators.Generators.ViewModels.Data.Members;

public abstract class BindableMember<T> : BindableMember
    where T : ISymbol
{
    public readonly T Member;
    
    protected BindableMember(T member, BindMode mode, string type, string sourceName, string generatedName, string idPostfix, GeneratedBindableMembers bindable) 
        : base(member, mode, type, sourceName, generatedName, idPostfix, bindable)
    {
        Member = member;
    }
}

public abstract class BindableMember
{
    public readonly string Type;
    public readonly string SourceName;
    public readonly string GeneratedName;
    public readonly GeneratedBindableMembers Bindable;

    public readonly IdData Id;
    public readonly BindMode Mode;
    
    protected BindableMember(
        ISymbol member,
        BindMode mode, 
        string type,
        string sourceName,
        string generatedName, 
        string idPostfix,
        GeneratedBindableMembers bindable)
    {
        Type = type;
        Mode = mode;
        SourceName = sourceName;
        GeneratedName = generatedName;
        Id = new IdData(member, idPostfix);
        Bindable = bindable;
    }
}