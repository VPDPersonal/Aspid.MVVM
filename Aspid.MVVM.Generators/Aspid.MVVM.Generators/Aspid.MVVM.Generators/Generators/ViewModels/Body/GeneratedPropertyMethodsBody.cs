using Microsoft.CodeAnalysis;
using Aspid.MVVM.Generators.Helpers;
using Aspid.MVVM.Generators.Shared;
using Aspid.Generators.Helper;
using System.Collections.Immutable;
using Aspid.MVVM.Generators.Generators.ViewModels.Data;
using Aspid.MVVM.Generators.Generators.ViewModels.Data.Infos;

namespace Aspid.MVVM.Generators.Generators.ViewModels.Body;

public static class GeneratedPropertyMethodsBody
{
    internal static void Generate(
        in ViewModelData data,
        AspidTypeDeclaration declaration,
        in SourceProductionContext context)
    {
        var properties = data.Members.OfType<BindablePropertyInfo>().ToImmutableArray();
        if (properties.Length is 0) return;
        
        var code = new CodeWriter();
        
        code.BeginScopedClass(declaration)
            .AppendBody(properties)
            .EndScopedClass(declaration);
        
        context.AddSource(declaration.GetFileName("GeneratedPropertyMethods"), code.GetSourceText());
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