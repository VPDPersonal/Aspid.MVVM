using Microsoft.CodeAnalysis;
using MVVMGenerators.Generators.Ids.Data;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Helpers.Extensions.Symbols;

namespace MVVMGenerators.Generators.Views.Data.Members;

public class BinderMember
{
    public readonly IdData Id;
    public readonly string Name;
    public readonly ISymbol Member;
    public readonly ITypeSymbol? Type;
    public readonly ITypeSymbol? BindingType;
    public readonly bool IsUnityEngineObject;
    
    public BinderMember(ISymbol member)
    {
        Member = member;
        Name = member.Name;
        Id = new IdData(member);
        Type = member.GetSymbolType();

        if (member.HasAttribute(Classes.RequireBinderAttribute, out var attribute))
            BindingType = attribute!.ConstructorArguments[0].Value as ITypeSymbol;

        IsUnityEngineObject = Type is IArrayTypeSymbol arrayType
            ? arrayType.ElementType.HasBaseType(Classes.Object)
            : Type?.HasBaseType(Classes.Object) ?? false;
    }
}