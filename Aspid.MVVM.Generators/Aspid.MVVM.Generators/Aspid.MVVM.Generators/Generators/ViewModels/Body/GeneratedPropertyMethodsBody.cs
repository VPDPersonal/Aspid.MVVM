using Microsoft.CodeAnalysis;
using Aspid.Generators.Helper;
using System.Collections.Immutable;
using Aspid.MVVM.Generators.Generators.ViewModels.Data;
using Aspid.MVVM.Generators.Generators.ViewModels.Data.Infos;

namespace Aspid.MVVM.Generators.Generators.ViewModels.Body;

public static class GeneratedPropertyMethodsBody
{
    public static void Generate(
        string namespaceName,
        in ViewModelData data,
        DeclarationText declaration,
        in SourceProductionContext context)
    {
        var properties = data.Members.OfType<BindablePropertyInfo>().ToImmutableArray();
        if (properties.Length is 0) return;
        
        var code = new CodeWriter();
        
        code.BeginClass(namespaceName, declaration)
            .AppendBody(properties)
            .EndClass(namespaceName);
        
        context.AddSource(declaration.GetFileName(namespaceName, "GeneratedPropertyMethods"), code.GetSourceText());
    }

    extension(CodeWriter code)
    {
        private CodeWriter AppendBody(in ImmutableArray<BindablePropertyInfo> properties)
        {
            foreach (var property in properties)
            {
                code.AppendMultiline(property.Declaration);
            }

            return code;
        }
    }
}

