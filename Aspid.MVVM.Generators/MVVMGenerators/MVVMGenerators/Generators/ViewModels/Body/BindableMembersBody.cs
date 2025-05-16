using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using MVVMGenerators.Helpers.Extensions.Writer;
using MVVMGenerators.Generators.ViewModels.Data;
using MVVMGenerators.Helpers.Extensions.Symbols;
using MVVMGenerators.Generators.ViewModels.Data.Members;
using static MVVMGenerators.Helpers.Descriptions.General;
using static MVVMGenerators.Helpers.Descriptions.Classes;
using BindMode = MVVMGenerators.Generators.ViewModels.Data.BindMode;

namespace MVVMGenerators.Generators.ViewModels.Body;

public static class BindableMembersBody
{
    public static void Generate(       
        string @namespace,
        in ViewModelData data,
        in DeclarationText declaration,
        in SourceProductionContext context)
    {
        string[] baseTypes = [$"{data.Symbol.ToDisplayStringGlobal()}.IBindableMembers"];

        var code = new CodeWriter();
        code.AppendClassBegin(@namespace, declaration, baseTypes)
            .AppendProperties(data)
            .AppendInterface(data)
            .AppendClassEnd(@namespace);
        
        context.AddSource(declaration.GetFileName(@namespace, "BindableMembers"), code.GetSourceText());
    }

    private static CodeWriter AppendProperties(this CodeWriter code, in ViewModelData data)
    {
        var bindableMembersInterface = data.Symbol.ToDisplayStringGlobal() + ".IBindableMembers";
        
        foreach (var member in data.Members)
        {
            if (member.Mode is BindMode.None) return code;

            AppendProperty(bindableMembersInterface, member.GeneratedName, member);
            code.AppendLine();

            if (data.CustomViewModelInterfaces.TryGetValue(member.Id.SourceValue, out var customInterface))
            {
                AppendProperty(customInterface.Interface.ToDisplayStringGlobal(), customInterface.PropertyName, member);
                code.AppendLine();
            }
        }

        return code;
        
        void AppendProperty(string interfaceType, string propertyName, BindableMember member)
        { 
            code.AppendLine($"{IBindableMemberEventAdder} {interfaceType}.{propertyName} =>")
                .IncreaseIndent()
                .AppendLine($"{member.Event.ToInstantiateFieldString()};")
                .DecreaseIndent();
        }
    }
    
    private static CodeWriter AppendInterface(this CodeWriter code, in ViewModelData data)
    {
        var members = data.Members;

        code.AppendLine(GeneratedCodeViewModelAttribute)
            .Append(data.Inheritor is Inheritor.None 
                ? $"public interface IBindableMembers : {IViewModel}" 
                : $"public new interface IBindableMembers : {data.Symbol.BaseType}.IBindableMembers");

        if (members.Length > 0)
        {
            return code
                .BeginBlock()
                .AppendLoop(members, member =>
                {
                    code.AppendLine($"public {IBindableMemberEventAdder} {member.GeneratedName} {{ get; }}")
                        .AppendLine();
                })
                .EndBlock();
        }

        return code.AppendLine("{ }");
    }
}