using Microsoft.CodeAnalysis;
using Aspid.Generators.Helper;
using System.Collections.Immutable;
using Aspid.MVVM.Generators.Generators.ViewModels.Data;
using Aspid.MVVM.Generators.Generators.ViewModels.Data.Infos;

namespace Aspid.MVVM.Generators.Generators.ViewModels.Body;

public static class GeneratedPropertiesBody
{
    public static void Generate(
        string namespaceName,
        in ViewModelData data,
        DeclarationText declaration,
        in SourceProductionContext context)
    {
        var fields = data.Members.OfType<BindableFieldInfo>().ToImmutableArray();
        if (fields.Length is 0) return;
        
        var code = new CodeWriter();
        
        code.BeginClass(namespaceName, declaration)
            .AppendBody(fields)
            .EndClass(namespaceName);
        
        context.AddSource(declaration.GetFileName(namespaceName, "GeneratedProperties"), code.GetSourceText());
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
