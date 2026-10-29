using System.Linq;
using Microsoft.CodeAnalysis;
using Aspid.Generators.Helper;
using Aspid.Generators.Helper.Unity;
using Aspid.MVVM.Generators.Generators.Views.Data;
using Aspid.MVVM.Generators.Generators.Views.Helpers;
using static Aspid.MVVM.Generators.Generators.Descriptions.Constants;
using Classes = Aspid.MVVM.Generators.Generators.Descriptions.Classes;

namespace Aspid.MVVM.Generators.Generators.Views.Body;

public static class BinderFieldsBody
{
    public static void Generate(
        string @namespace,
        in ViewDataSpan data,
        DeclarationText declaration,
        in SourceProductionContext context)
    {
        var virtualFields = VirtualBinderFields.Collect(data);
        if (virtualFields.Count is 0) return;

        var code = new CodeWriter();
        code.BeginClass(@namespace, declaration);

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

        code.EndClass(@namespace);
        context.AddSource(declaration.GetFileName(@namespace, "BinderFields"), code.GetSourceText());
    }

    private static string EscapeStringLiteral(string value) =>
        $"\"{value.Replace("\\", "\\\\").Replace("\"", "\\\"")}\"";
}
