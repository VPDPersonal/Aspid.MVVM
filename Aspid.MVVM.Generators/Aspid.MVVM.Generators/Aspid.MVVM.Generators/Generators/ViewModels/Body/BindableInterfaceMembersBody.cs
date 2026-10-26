using Microsoft.CodeAnalysis;
using Aspid.Generators.Helper;
using Aspid.MVVM.Generators.Generators.ViewModels.Data;
using static Aspid.MVVM.Generators.Generators.Descriptions.Classes;
using static Aspid.MVVM.Generators.Generators.Descriptions.Constants;
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
            if (member.Mode is BindMode.None) continue;
            if (!data.CustomViewModelInterfaces.TryGetValue(member.Id.SourceValue, out var customInterface)) continue;
            
            var property = customInterface.Property;
            var propertyType = property.Type.ToDisplayStringGlobal();

            if (propertyType != IBinderAdder)
            {
                if (!propertyType.Contains(member.Type)) continue;
                if (!IsAssignable(propertyType, member.Bindable.PropertyType)) continue;
            }
            
            var interfaceType = customInterface.Interface.ToDisplayStringGlobal();
                
            code.AppendLine(GeneratedCodeViewModelAttribute)
                .AppendLine($"{propertyType} {interfaceType}.{property.Name} => {member.Bindable.PropertyName};")
                .AppendLine();
        }

        return code;
    }

    /// <summary>
    /// Whether the generated bindable property can back the interface property: a two-way slot needs a two-way
    /// member, a read-only slot takes a read-only or two-way member, and a value slot takes any bindable member.
    /// </summary>
    private static bool IsAssignable(string interfaceType, string generatedType)
    {
        if (interfaceType.Contains(IBindableMember)) return generatedType.Contains(IBindableMember);
        if (interfaceType.Contains(IReadOnlyBindableMember)) return generatedType.Contains(IReadOnlyBindableMember) || generatedType.Contains(IBindableMember);
        
        return true;
    }
}