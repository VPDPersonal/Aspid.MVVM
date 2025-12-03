using Microsoft.CodeAnalysis;
using Aspid.Generators.Helper;
using Aspid.MVVM.Generators.Generators.ViewModels.Data;
using static Aspid.MVVM.Generators.Generators.Descriptions.General;
using static Aspid.MVVM.Generators.Generators.Descriptions.Classes;
using BindMode = Aspid.MVVM.Generators.Generators.ViewModels.Data.BindMode;

namespace Aspid.MVVM.Generators.Generators.ViewModels.Body;

public static class BindableInterfaceMembersBody
{
    public static void Generate(       
        string @namespace,
        in ViewModelData data,
        DeclarationText declaration,
        in SourceProductionContext context)
    {
        var code = new CodeWriter();
        code.BeginClass(@namespace, declaration)
            .AppendProperties(data)
            .EndClass(@namespace);
        
        context.AddSource(declaration.GetFileName(@namespace, "BindableInterfaceMembers"), code.GetSourceText());
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
                    if (!member.Bindable.PropertyType.Contains(IReadOnlyBindableMember))
                        continue;
                }
            }
            
            var interfaceType = customInterface.Interface.ToDisplayStringGlobal();
                
            code.AppendLine(GeneratedCodeViewModelAttribute)
                .AppendLine($"{propertyType} {interfaceType}.{property.Name} => {member.Bindable.PropertyName};")
                .AppendLine();
        }

        return code;
    }
}