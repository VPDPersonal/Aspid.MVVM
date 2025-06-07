using Microsoft.CodeAnalysis;
using Aspid.Generator.Helpers;
using Aspid.MVVM.Generators.Ids.Data;

namespace Aspid.MVVM.Generators.Views.Data.Members;

public class BinderMember
{
    public readonly IdData Id;
    public readonly string Name;
    public readonly ISymbol Member;
    public readonly ITypeSymbol? Type;
    
    public BinderMember(ISymbol member)
    {
        Member = member;
        Name = member.Name;
        Id = new IdData(member);
        Type = member.GetSymbolType();
    }
}