using Microsoft.CodeAnalysis;
using Aspid.Generators.Helper;
using Aspid.MVVM.Generators.Generators.Ids.Data;

namespace Aspid.MVVM.Generators.Generators.Views.Data.Members;

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