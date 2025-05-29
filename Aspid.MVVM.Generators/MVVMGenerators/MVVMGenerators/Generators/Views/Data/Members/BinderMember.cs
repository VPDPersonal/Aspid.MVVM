using Microsoft.CodeAnalysis;
using MVVMGenerators.Generators.Ids.Data;
using MVVMGenerators.Helpers.Extensions.Symbols;

namespace MVVMGenerators.Generators.Views.Data.Members;

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