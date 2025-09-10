using Microsoft.CodeAnalysis;
using Aspid.Generator.Helpers;
using Aspid.MVVM.Generators.ViewModels.Data;
using Aspid.MVVM.Generators.ViewModels.Data.Members;
using static Aspid.MVVM.Generators.Descriptions.General;

namespace Aspid.MVVM.Generators.ViewModels.Body;

public static class PropertiesBody
{
    public static void Generate(
        string @namespace,
        in ViewModelData data,
        in DeclarationText declaration,
        in SourceProductionContext context)
    {
        var code = new CodeWriter();

        code.AppendClassBegin(@namespace, declaration)
            .AppendBody(data)
            .AppendClassEnd(@namespace);

        context.AddSource(declaration.GetFileName(@namespace, "Properties"), code.GetSourceText());
    }
    
    private static CodeWriter AppendBody(this CodeWriter code, in ViewModelData data)
    {
        if (!data.Members.IsEmpty)
        {
            code.AppendFieldEvents(data)
                .AppendProperties(data)
                .AppendBindableMembers(data)
                .AppendSetMethods(data);
        }
        
        return code.AppendNotifyAll(data);
    }
    
    private static CodeWriter AppendBindableMembers(this CodeWriter code, in ViewModelData data)
    {
        foreach (var member in data.Members)
        {
            code.AppendMultiline(member.ToBindableMemberPropertyDeclarationString())
                .AppendLine();
        }

        return code;
    }

    private static CodeWriter AppendFieldEvents(this CodeWriter code, in ViewModelData data)
    {
        foreach (var member in data.Members)
        {
            var fieldDeclaration = member.ToBindableMemberFieldDeclarationString();
            if (fieldDeclaration is null) continue;
            
            code.AppendMultiline(fieldDeclaration)
                .AppendLine();
        }
        
        return code;
    }

    private static CodeWriter AppendProperties(this CodeWriter code, in ViewModelData data)
    {
        foreach (var field in data.Members.OfType<BindableField>())
        {
            if (field.Member.IsConst) continue;
            
            code.AppendMultiline(field.ToDeclarationPropertyString())
                .AppendLine();
        }

        return code;
    }
    
    private static CodeWriter AppendSetMethods(this CodeWriter code, in ViewModelData data)
    {
        foreach (var field in data.Members.OfType<BindableField>())
        {
            if (field.Mode is BindMode.OneTime) continue;
            
            code.AppendMultiline(field.ToSetMethodString())
                .AppendLine();
        }

        return code;
    }

    private static CodeWriter AppendNotifyAll(this CodeWriter code, in ViewModelData data)
    {
        var modifiers = "private";
        if (data.Inheritor is not Inheritor.None) modifiers = "protected override";
        else if (!data.Symbol.IsSealed) modifiers = "protected virtual";
        
        code.AppendLine(GeneratedCodeViewModelAttribute)
            .AppendLine($"{modifiers} void NotifyAll()")
            .BeginBlock()
            .AppendLineIf(data.Inheritor is Inheritor.Inheritor, "base.NotifyAll();");
        
        foreach (var member in data.Members)
        {
            var invoke = member.ToInvokeBindableMemberString();
            code.AppendLineIf(!string.IsNullOrWhiteSpace(invoke), invoke);
        }

        return code.EndBlock();
    }
}