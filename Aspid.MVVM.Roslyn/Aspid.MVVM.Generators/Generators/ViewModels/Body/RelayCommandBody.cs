using System.Linq;
using Microsoft.CodeAnalysis;
using Aspid.MVVM.Generators.Helpers;
using Aspid.MVVM.Generators.Shared;
using Aspid.Generators.Helper;
using Aspid.MVVM.Generators.Generators.ViewModels.Data;
using Aspid.MVVM.Generators.Generators.ViewModels.Data.Infos;

namespace Aspid.MVVM.Generators.Generators.ViewModels.Body;

public static class RelayCommandBody
{
    internal static void Generate(
        in ViewModelData data,
        AspidTypeDeclaration declaration,
        in SourceProductionContext context)
    {
        if (!data.Members.OfType<BindableCommandInfo>().Any()) return;
        
        var code = new CodeWriter();
            
        code.BeginScopedClass(declaration)
            .AppendBody(data)
            .EndScopedClass(declaration);
            
        context.AddSource(declaration.GetFileName("Commands"), code.GetSourceText());
    }
    
    private static CodeWriter AppendBody(this CodeWriter code, in ViewModelData data)
    {
        foreach (var command in data.Members.OfType<BindableCommandInfo>())
        {
            code.AppendMultiline(command.CommandDeclaration)
                .AppendLine();
        }
        
        return code;
    }
}