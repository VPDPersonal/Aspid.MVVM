using Microsoft.CodeAnalysis;
using Aspid.Generator.Helpers;
using Aspid.MVVM.Generators.Descriptions;
using Aspid.MVVM.Generators.Views.Data;
using Aspid.MVVM.Generators.Views.Data.Members;

namespace Aspid.MVVM.Generators.Views.Body;

public static class BinderCachedBody
{
    private const string GeneratedAttribute = General.GeneratedCodeViewAttribute;
    private static readonly string EditorBrowsableAttribute = $"[{Classes.EditorBrowsableAttribute.Global}({Classes.EditorBrowsableState.Global}.Never)]";

    public static void Generate(
        string @namespace,
        in ViewDataSpan data,
        in DeclarationText declaration,
        in SourceProductionContext context)
    {
        if (data.MembersByType.PropertyBinders.Length + data.MembersByType.AsBinders.Length == 0) return;
        var code = new CodeWriter();

        code.AppendClassBegin(@namespace, declaration)
            .AppendCachedBinders(data)
            .AppendClassEnd(@namespace);
            
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
        code.AppendLine(EditorBrowsableAttribute)
            .AppendLine(GeneratedAttribute)
            .AppendLine($"private {cashedBinderMember.Type?.ToDisplayStringGlobal()} {cashedBinderMember.CachedName};")
            .AppendLine();

        return code;
    }
    
    private static CodeWriter AppendAsBinderMember(this CodeWriter code, AsBinderMember asBinderMember)
    {
        code.AppendLine(EditorBrowsableAttribute)
            .AppendLine(GeneratedAttribute)
            .AppendLine(asBinderMember.Type is IArrayTypeSymbol
                ? $"private {asBinderMember.AsBinderType}[] {asBinderMember.CachedName};"
                : $"private {asBinderMember.AsBinderType} {asBinderMember.CachedName};")
            .AppendLine();

        return code;
    }
}