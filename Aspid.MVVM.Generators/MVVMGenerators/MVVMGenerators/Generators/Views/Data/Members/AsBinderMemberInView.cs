using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using MVVMGenerators.Generators.Ids;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Helpers.Extensions.Symbols;

namespace MVVMGenerators.Generators.Views.Data.Members;

public readonly struct AsBinderMemberInView
{
    public readonly string Name;
    public readonly string? CachedName;
    public readonly string AsBinderType;
    
    public readonly string? Id;
    public readonly ISymbol Member;
    public readonly string Arguments;
    public readonly ITypeSymbol? Type;
    public readonly bool IsUnityEngineObject;
    
    public AsBinderMemberInView(ISymbol member, ITypeSymbol asBinderType, IReadOnlyList<string>? arguments) 
        : this(member, asBinderType.ToDisplayStringGlobal(), arguments) { }

    public AsBinderMemberInView(ISymbol member, string asBinderType, IReadOnlyList<string>? arguments)
    {
        Member = member;
        Name = member.Name;
        AsBinderType = asBinderType;
        Arguments = arguments is null || arguments.Count == 0 ? string.Empty : ", " + string.Join(", ", arguments);

        switch (member)
        {
            case IFieldSymbol field:
                Type = field.Type;
                CachedName = $"_{field.Name}CachedBinder";
                Id = $"{Classes.Ids.Global}.{field.GetId()}";
                break;
            
            case IPropertySymbol property:
                Type = property.Type;
                Id = $"{Classes.Ids.Global}.{property.GetId()}";
                CachedName = $"_{property.GetFieldName()}CachedPropertyBinder";
                break;
            
            default:
                Id = null;
                Type = null;
                CachedName = null;
                break;
        }

        if (Type != null)
        {
            IsUnityEngineObject = Type switch
            {
                IArrayTypeSymbol => false,
                _ => Type?.HasBaseType(Classes.Object) ?? false
            };
        }
    }
}
