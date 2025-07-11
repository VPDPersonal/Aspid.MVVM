using Microsoft.CodeAnalysis;
using Aspid.Generator.Helpers;
using Aspid.MVVM.Generators.ViewModels.Data;
using static Aspid.MVVM.Generators.Descriptions.Classes;
using static Aspid.MVVM.Generators.Descriptions.General;
using BindMode = Aspid.MVVM.Generators.ViewModels.Data.BindMode;

namespace Aspid.MVVM.Generators.ViewModels.Body;

public static class BindableMembersBody
{
    public static void Generate(       
        string @namespace,
        in ViewModelData data,
        in DeclarationText declaration,
        in SourceProductionContext context)
    {
        var code = new CodeWriter();
        code.AppendClassBegin(@namespace, declaration)
            .AppendProperties(data)
            .AppendClassEnd(@namespace);
        
        context.AddSource(declaration.GetFileName(@namespace, "BindableMembers"), code.GetSourceText());
    }

    private static CodeWriter AppendProperties(this CodeWriter code, in ViewModelData data)
    {
        foreach (var member in data.Members)
        {
            if (member.Mode is BindMode.None) return code;
            if (!data.CustomViewModelInterfaces.TryGetValue(member.Id.SourceValue, out var customInterface)) continue;
            
            var property = customInterface.Property;
            var propertyType = property.Type.ToDisplayStringGlobal();

            if (propertyType != IBinderAdder)
            {
                if (!propertyType.Contains(member.Type)) continue;

                if (!propertyType.Contains(IReadOnlyValueBindableMember))
                {
                    if (!member.BindableMemberPropertyType.Contains(IReadOnlyBindableMember))
                        continue;
                }
            }
            
            var interfaceType = customInterface.Interface.ToDisplayStringGlobal();
                
            code.AppendLine(GeneratedCodeViewModelAttribute)
                .AppendLine($"{propertyType} {interfaceType}.{property.Name} => {member.GeneratedName}Bindable;")
                .AppendLine();
        }

        return code;
    }
}