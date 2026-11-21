using Microsoft.CodeAnalysis;
using Aspid.MVVM.Generators.Helpers;
using Aspid.MVVM.Generators.Shared;
using Aspid.Generators.Helper;
using System.Collections.Immutable;
using Aspid.MVVM.Generators.Generators.ViewModels.Data;
using Aspid.MVVM.Generators.Generators.ViewModels.Data.Infos;

namespace Aspid.MVVM.Generators.Generators.ViewModels.Body;

public static class GeneratedPropertiesBody
{
    internal static void Generate(
        in ViewModelData data,
        AspidTypeDeclaration declaration,
        in SourceProductionContext context)
    {
        var fields = data.Members.OfType<BindableFieldInfo>().ToImmutableArray();
        if (fields.Length is 0) return;
        
        var code = new CodeWriter();
        
        code.BeginScopedClass(declaration)
            .AppendBody(fields)
            .EndScopedClass(declaration);
        
        context.AddSource(declaration.GetFileName("GeneratedProperties"), code.GetSourceText());
    }

    extension(CodeWriter code)
    {
        private CodeWriter AppendBody(in ImmutableArray<BindableFieldInfo> fields)
        {
            foreach (var field in fields)
            {
                code.AppendMultiline(field.Declaration);
            }

            return code;
        }
    }
}