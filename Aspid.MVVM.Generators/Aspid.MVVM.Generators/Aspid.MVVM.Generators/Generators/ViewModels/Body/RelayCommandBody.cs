using System.Linq;
using Microsoft.CodeAnalysis;
using Aspid.Generators.Helper;
using Aspid.MVVM.Generators.Generators.ViewModels.Data;
using Aspid.MVVM.Generators.Generators.ViewModels.Data.Members;

namespace Aspid.MVVM.Generators.Generators.ViewModels.Body;

public static class RelayCommandBody
{
    public static void Generate(
        string @namespace,
        in ViewModelData data,
        DeclarationText declaration,
        in SourceProductionContext context)
    {
        if (!data.Members.OfType<BindableCommand>().Any()) return;
        
        var code = new CodeWriter();
            
        code.BeginClass(@namespace, declaration)
            .AppendBody(data)
            .EndClass(@namespace);
            
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