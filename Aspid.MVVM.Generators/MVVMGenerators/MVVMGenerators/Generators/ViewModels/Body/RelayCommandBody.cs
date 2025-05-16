using System.Linq;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using MVVMGenerators.Helpers.Extensions.Writer;
using MVVMGenerators.Generators.ViewModels.Data;
using MVVMGenerators.Generators.ViewModels.Data.Members;

namespace MVVMGenerators.Generators.ViewModels.Body;

public static class RelayCommandBody
{
    public static void Generate(
        string @namespace,
        in ViewModelData data,
        in DeclarationText declaration,
        in SourceProductionContext context)
    {
        if (!data.Members.OfType<BindableCommand>().Any()) return;
        
        var code = new CodeWriter();
            
        code.AppendClassBegin(@namespace, declaration)
            .AppendBody(data)
            .AppendClassEnd(@namespace);
            
        context.AddSource(declaration.GetFileName(@namespace, "Commands"), code.GetSourceText());
    }
    
    private static CodeWriter AppendBody(this CodeWriter code, in ViewModelData data)
    {
        foreach (var command in data.Members.OfType<BindableCommand>())
        {
            code.AppendMultiline(command.ToDeclarationCommandString())
                .AppendLine();
        }
        
        return code;
    }
}