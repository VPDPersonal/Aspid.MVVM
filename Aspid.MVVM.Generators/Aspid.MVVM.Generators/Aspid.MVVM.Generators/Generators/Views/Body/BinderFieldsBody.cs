using System.Linq;
using Microsoft.CodeAnalysis;
using Aspid.MVVM.Generators.Helpers;
using Aspid.MVVM.Generators.Shared;
using Aspid.Generators.Helper;
using Aspid.Generators.Helper.Unity;
using Aspid.MVVM.Generators.Generators.Views.Data;
using Aspid.MVVM.Generators.Generators.Views.Helpers;
using static Aspid.MVVM.Generators.Generators.Descriptions.Constants;
using Classes = Aspid.MVVM.Generators.Generators.Descriptions.Classes;

namespace Aspid.MVVM.Generators.Generators.Views.Body;

public static class BinderFieldsBody
{
    internal static void Generate(
        in ViewDataSpan data,
        AspidTypeDeclaration declaration,
        in SourceProductionContext context)
    {
        var virtualFields = VirtualBinderFields.Collect(data);
        if (virtualFields.Count is 0) return;

        var code = new CodeWriter();
        code.BeginScopedClass(declaration);

        foreach (var info in virtualFields.Values)
        {
            var requireBinderArgs = string.Join(", ", info.RequireBinderTypes.Select(type => $"typeof({type})"));

            code.AppendLine(GeneratedCodeViewAttribute);

            if (info.HeaderGroup is not null)
                code.AppendLine($"[{Classes.HeaderGroupAttribute}({EscapeStringLiteral(info.HeaderGroup)})]");

            if (info.Header is not null)
                code.AppendLine($"[{Classes.HeaderAttribute}({EscapeStringLiteral(info.Header)})]");

            code.AppendLine($"[{Classes.RequireBinderAttribute}({requireBinderArgs})]")
                .AppendLine($"[{UnityClasses.SerializeField}] private {Classes.MonoBinder}[] {info.FieldName};")
                .AppendLine();
        }

        code.EndScopedClass(declaration);
        context.AddSource(declaration.GetFileName("BinderFields"), code.GetSourceText());
    }

    private static string EscapeStringLiteral(string value) =>
        $"\"{value.Replace("\\", "\\\\").Replace("\"", "\\\"")}\"";
}
