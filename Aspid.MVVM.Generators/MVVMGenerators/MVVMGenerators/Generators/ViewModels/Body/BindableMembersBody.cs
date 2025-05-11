using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Helpers.Extensions.Writer;
using MVVMGenerators.Generators.ViewModels.Data;
using MVVMGenerators.Helpers.Extensions.Symbols;
using MVVMGenerators.Generators.ViewModels.Data.Members;

namespace MVVMGenerators.Generators.ViewModels.Body;

public static class BindableMembersBody
{
    public static void Generate(       
        string @namespace,
        in ViewModelDataSpan data,
        in DeclarationText declaration,
        in SourceProductionContext context)
    {
        string[] baseTypes = [$"{data.ClassSymbol.ToDisplayStringGlobal()}.IBindableMembers"];

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
            code.AppendBindableMemberProperty(member, data)
                .AppendLine();

        return code;
    }

    private static CodeWriter AppendBindableMemberProperty(this CodeWriter code, BindableMember member, in ViewModelDataSpan data)
    {
        if (member.Mode is BindMode.None) return code;

        var classType = data.ClassSymbol.ToDisplayStringGlobal();

        return code
            .AppendLine($"{Classes.IBindableMemberEventAdder} {classType}.IBindableMembers.{member.GeneratedName} =>")
            .IncreaseIndent()
            .AppendLine($"{member.Event.ToInstantiateFieldString()};")
            .DecreaseIndent();
    }

    private static CodeWriter AppendBindableMembersInterface(this CodeWriter code, in ViewModelDataSpan data)
    {
        var classType = data.ClassSymbol.BaseType!.ToDisplayStringGlobal();
        
        code.AppendLine(data.Inheritor is Inheritor.InheritorViewModelAttribute 
            ? $"public new interface IBindableMembers : {classType}.IBindableMembers" 
            : $"public interface IBindableMembers : {Classes.IViewModel}")
            .BeginBlock();

        foreach (var member in data.Members)
        {
            code.AppendLine($"public {Classes.IBindableMemberEventAdder} {member.GeneratedName} {{ get; }}")
                .AppendLine();
        }

        return code.EndBlock();
    }
}