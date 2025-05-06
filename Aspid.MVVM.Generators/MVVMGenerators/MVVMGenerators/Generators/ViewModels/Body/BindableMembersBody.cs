using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Helpers.Extensions.Writer;
using MVVMGenerators.Generators.ViewModels.Data;
using MVVMGenerators.Helpers.Extensions.Symbols;
using MVVMGenerators.Generators.ViewModels.Extensions;

namespace MVVMGenerators.Generators.ViewModels.Body;

public static class BindableMembersBody
{
    public static void Generate(       
        string @namespace,
        in ViewModelDataSpan data,
        in DeclarationText declaration,
        in SourceProductionContext context)
    {
        string[] baseTypes = [$"{data.Name}.IBindableMembers"];

        var code = new CodeWriter();
        code.AppendClassBegin(@namespace, declaration, baseTypes)
            .AppendBindableMemberProperties(data)
            .AppendLine()
            .AppendBindableMembersInterface(data)
            .AppendClassEnd(@namespace);
        
        context.AddSource(declaration.GetFileName(@namespace, "BindableMembers"), code.GetSourceText());
    }

    private static CodeWriter AppendBindableMemberProperties(this CodeWriter code, ViewModelDataSpan data)
    {
        foreach (var idGroup in data.IdLengthMemberGroups)
        {
            foreach (var hashGroup in idGroup.HashCodeGroup)
            {
                foreach (var member in hashGroup.Members)
                {
                    if (member.Mode is BindMode.None) continue;
                    
                    code.AppendLine($"{Classes.BindableMember}<{member.Type}> {data.Name}.IBindableMembers.{member.GeneratedName} =>")
                        .IncreaseIndent()
                        .AppendBindableMemberInstance(member)
                        .AppendLine(";")
                        .DecreaseIndent()
                        .AppendLine();
                }
            }
        }

        return code;
    }

    private static CodeWriter AppendBindableMembersInterface(this CodeWriter code, ViewModelDataSpan data)
    {
        code.AppendLine(data.Inheritor is Inheritor.InheritorViewModelAttribute ?
            $"public new interface IBindableMembers : {data.ClassSymbol.BaseType!.ToDisplayStringGlobal()}.IBindableMembers" :
            $"public interface IBindableMembers : {Classes.IViewModel}");
        code.BeginBlock();

        foreach (var idGroup in data.IdLengthMemberGroups)
        {
            foreach (var hashGroup in idGroup.HashCodeGroup)
            {
                foreach (var member in hashGroup.Members)
                {
                    code.AppendLine($"public {Classes.BindableMember}<{member.Type}> {member.GeneratedName} {{ get; }}")
                        .AppendLine();
                }
            }
        }

        return code.EndBlock();
    }
}