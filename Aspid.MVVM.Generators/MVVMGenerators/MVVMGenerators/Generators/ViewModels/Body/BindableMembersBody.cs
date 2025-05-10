using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Helpers.Extensions.Writer;
using MVVMGenerators.Generators.ViewModels.Data;
using MVVMGenerators.Generators.ViewModels.Data.Members;
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

    private static CodeWriter AppendBindableMemberProperties(this CodeWriter code, in ViewModelDataSpan data)
    {
        foreach (var member in data.Members)
        {
            Append(data, member);
        }

        return code;

        void Append(in ViewModelDataSpan data, BindableMember member)
        {
            if (member.Mode is BindMode.None) return;
                    
            code.AppendLine($"{Classes.IBindableMemberEventAdder} {data.Name}.IBindableMembers.{member.GeneratedName} =>")
                .IncreaseIndent()
                .AppendBindableMemberInstance(member)
                .AppendLine(";")
                .DecreaseIndent()
                .AppendLine();
        }
    }

    private static CodeWriter AppendBindableMembersInterface(this CodeWriter code, ViewModelDataSpan data)
    {
        code.AppendLine(data.Inheritor is Inheritor.InheritorViewModelAttribute ?
            $"public new interface IBindableMembers : {data.ClassSymbol.BaseType!.ToDisplayStringGlobal()}.IBindableMembers" :
            $"public interface IBindableMembers : {Classes.IViewModel}");
        code.BeginBlock();

        foreach (var member in data.Members)
        {
            code.AppendLine($"public {Classes.IBindableMemberEventAdder} {member.GeneratedName} {{ get; }}")
                .AppendLine();
        }

        return code.EndBlock();
    }
}