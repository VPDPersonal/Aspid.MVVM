using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using MVVMGenerators.Helpers.Extensions.Writer;
using MVVMGenerators.Generators.ViewModels.Data;
using MVVMGenerators.Generators.ViewModels.Data.Members;

namespace MVVMGenerators.Generators.ViewModels.Body;

public static class PropertiesBody
{
    public static void Generate(
        string @namespace,
        in ViewModelData data,
        in DeclarationText declaration,
        in SourceProductionContext context)
    {
        if (data.Members.IsEmpty) return;
        
        var code = new CodeWriter();

        code.AppendClassBegin(@namespace, declaration)
            .AppendBody(data)
            .AppendClassEnd(@namespace);

        context.AddSource(declaration.GetFileName(@namespace, "Properties"), code.GetSourceText());
    }
    
    private static CodeWriter AppendBody(this CodeWriter code, in ViewModelData data)
    {
        return code
            .AppendEvents(data)
            .AppendFieldEvents(data)
            .AppendProperties(data)
            .AppendSetMethods(data);
    }
    
    private static CodeWriter AppendEvents(this CodeWriter code, in ViewModelData data)
    {
        foreach (var member in data.Members)
        {
            var @event = member.Event;
            if (!@event.IsEventExist) continue;

            code.AppendMultiline(@event.ToEventDeclarationString())
                .AppendLine();
        }

        return code;
    }

    private static CodeWriter AppendFieldEvents(this CodeWriter code, in ViewModelData data)
    {
        foreach (var member in data.Members)
        {
            var @event = member.Event;
            if (!@event.IsExist) continue;

            code.AppendMultiline(@event.ToFieldDeclarationString())
                .AppendLine();
        }
        
        return code;
    }

    private static CodeWriter AppendProperties(this CodeWriter code, in ViewModelData data)
    {
        foreach (var field in data.Members.OfType<BindableField>())
        {
            if (field.Mode is BindMode.OneTime) continue;
            
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
}