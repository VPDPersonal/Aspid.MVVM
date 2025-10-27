using Microsoft.CodeAnalysis;
using Aspid.Generators.Helper;
using Aspid.MVVM.Generators.Generators.Views.Data;
using Aspid.MVVM.Generators.Generators.Descriptions;
using Aspid.MVVM.Generators.Generators.Views.Data.Members;
using static Aspid.Generators.Helper.Classes;

namespace Aspid.MVVM.Generators.Generators.Views.Body;

public static class BinderCachedBody
{
    private const string GeneratedAttribute = General.GeneratedCodeViewAttribute;

    public static void Generate(
        string @namespace,
        in ViewDataSpan data,
        DeclarationText declaration,
        in SourceProductionContext context)
    {
        if (data.MembersByType.PropertyBinders.Length + data.MembersByType.AsBinders.Length == 0) return;
        var code = new CodeWriter();

        code.BeginClass(@namespace, declaration)
            .AppendCachedBinders(data)
            .EndClass(@namespace);
            
        context.AddSource(declaration.GetFileName(@namespace, "CachedBinders"), code.GetSourceText());
    }
    
    private static CodeWriter AppendCachedBinders(this CodeWriter code, in ViewDataSpan data)
    {
        foreach (var member in data.Members)
        {
            switch (member)
            {
                case AsBinderMember asBinderMember: code.AppendAsBinderMember(asBinderMember); break;
                case CachedBinderMember cachedBinderMember: code.AppendCachedBinderMember(cachedBinderMember); break;
            }
        }

        return code;
    }

    private static CodeWriter AppendCachedBinderMember(this CodeWriter code, in CachedBinderMember cashedBinderMember)
    {
        code.AppendLine($"[{EditorBrowsableAttribute}({EditorBrowsableState}.Never)]")
            .AppendLine(GeneratedAttribute)
            .AppendLine($"private {cashedBinderMember.Type?.ToDisplayStringGlobal()} {cashedBinderMember.CachedName};")
            .AppendLine();

        return code;
    }
    
    private static CodeWriter AppendAsBinderMember(this CodeWriter code, AsBinderMember asBinderMember)
    {
        code.AppendLine($"[{EditorBrowsableAttribute}({EditorBrowsableState}.Never)]")
            .AppendLine(GeneratedAttribute)
            .AppendLine(asBinderMember.Type is IArrayTypeSymbol
                ? $"private {asBinderMember.AsBinderType}[] {asBinderMember.CachedName};"
                : $"private {asBinderMember.AsBinderType} {asBinderMember.CachedName};")
            .AppendLine();

        return code;
    }
}