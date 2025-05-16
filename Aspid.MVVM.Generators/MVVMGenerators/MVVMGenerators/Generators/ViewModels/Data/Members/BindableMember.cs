using Microsoft.CodeAnalysis;
using MVVMGenerators.Generators.Ids.Data;

namespace MVVMGenerators.Generators.ViewModels.Data.Members;

public abstract class BindableMember<T> : BindableMember
    where T : ISymbol
{
    public readonly T Member;

    protected BindableMember(T member, BindMode mode, string type, string name, string idPostfix)
        : base(member, mode, type, name, idPostfix)
    {
        Member = member;
    }

    protected BindableMember(T member, BindMode mode, string type, string sourceName, string generatedName, string idPostfix) 
        : base(member, mode, type, sourceName, generatedName, idPostfix)
    {
        Member = member;
    }
}

public abstract class BindableMember
{
    public readonly string Type;
    public readonly string SourceName;
    public readonly string GeneratedName;

    public readonly IdData Id;
    public readonly BindMode Mode;
    public readonly ViewModelEvent Event;

    protected BindableMember(ISymbol member, BindMode mode, string type, string name, string idPostfix)
        : this(member, mode, type, name, name, idPostfix) { }
    
    protected BindableMember(ISymbol member, BindMode mode, string type, string sourceName, string generatedName, string idPostfix)
    {
        Type = type;
        Mode = mode;
        SourceName = sourceName;
        GeneratedName = generatedName;
        Id = new IdData(member, idPostfix);
        Event = new ViewModelEvent(mode, sourceName, generatedName, type);
    }
}