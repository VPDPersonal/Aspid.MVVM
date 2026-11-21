using Microsoft.CodeAnalysis;
using Aspid.MVVM.Generators.Helpers;
using Aspid.MVVM.Generators.Shared;
using Aspid.Generators.Helper;
using Aspid.MVVM.Generators.Generators.Views.Data;
using Aspid.MVVM.Generators.Generators.Views.Data.Members;
using static Aspid.MVVM.Generators.Generators.Descriptions.Constants;

namespace Aspid.MVVM.Generators.Generators.Views.Body;

public static class BinderCachedBody
{
    internal static void Generate(
        in ViewDataSpan data,
        AspidTypeDeclaration declaration,
        in SourceProductionContext context)
    {
        if (data.MembersByType.PropertyBinders.Length + data.MembersByType.AsBinders.Length == 0) return;
        var code = new CodeWriter();

        code.BeginScopedClass(declaration)
            .AppendCachedBinders(data)
            .EndScopedClass(declaration);
            
        context.AddSource(declaration.GetFileName("CachedBinders"), code.GetSourceText());
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
        code.AppendLine(GeneratedCodeViewAttribute)
            .AppendLine(EditorBrowsableAttributeNever)
            .AppendLine($"private {cashedBinderMember.Type?.ToDisplayStringGlobal()} {cashedBinderMember.CachedName};")
            .AppendLine();

        return code;
    }
    
    private static CodeWriter AppendAsBinderMember(this CodeWriter code, AsBinderMember asBinderMember)
    {
        code.AppendLine(GeneratedCodeViewAttribute)
            .AppendLine(EditorBrowsableAttributeNever)
            .AppendLine(asBinderMember.Type is IArrayTypeSymbol
                ? $"private {asBinderMember.AsBinderType}[] {asBinderMember.CachedName};"
                : $"private {asBinderMember.AsBinderType} {asBinderMember.CachedName};")
            .AppendLine();

        return code;
    }
}