using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Helpers.Extensions.Writer;
using MVVMGenerators.Generators.ViewModels.Data;
using MVVMGenerators.Generators.ViewModels.Extensions;

namespace MVVMGenerators.Generators.ViewModels.Body;

public static class FindBindableMembersBody
{
    // ReSharper disable once InconsistentNaming
    private static readonly string EditorBrowsableAttribute = $"[{Classes.EditorBrowsableAttribute}({Classes.EditorBrowsableState}.Never)]";

    public static void Generate(
        string @namespace,
        in ViewModelDataSpan data,
        in DeclarationText declaration,
        in SourceProductionContext context)
    {
        string[] baseTypes = [Classes.IViewModel.Global];

        var code = new CodeWriter();
        code.AppendClassBegin(@namespace, declaration, baseTypes)
            .AppendFound(data)
            .AppendClassEnd(@namespace);

        context.AddSource(declaration.GetFileName(@namespace, "FindBindableMembers"), code.GetSourceText());
    }
    
    private static CodeWriter AppendFound(this CodeWriter code, ViewModelDataSpan span)
    {
        return code.AppendProfilerMarkers(span.Name)
            .AppendLine()
            .AppendFindBindableMembersDeclaration(span);
    }

    private static CodeWriter AppendProfilerMarkers(this CodeWriter code, string className)
    {
        return code.AppendMultiline(
            $"""
            #if !{Defines.ASPID_MVVM_UNITY_PROFILER_DISABLED}
            {EditorBrowsableAttribute}
            {General.GeneratedCodeViewModelAttribute}
            private static readonly {Classes.ProfilerMarker} __findBindableMemberMarker = new("{className}.FindBindableMember");
            #endif
            """);
    }

    private static CodeWriter AppendFindBindableMembersDeclaration(this CodeWriter code, ViewModelDataSpan span)
    {
        var modifier = span.Inheritor is Inheritor.None ? "virtual" : "override";

        return code.AppendMultiline(
                $"""
                 {General.GeneratedCodeViewModelAttribute}
                 public {modifier} {Classes.FindBindableMemberResult} FindBindableMember({Classes.Id} id)
                 """)
            .BeginBlock()
            .AppendFindBindableMemberBody(span, false)
            .EndBlock()
            .AppendLine()
            .AppendMultiline(
                $"""
                 {General.GeneratedCodeViewModelAttribute}
                 public {modifier} {Classes.FindBindableMemberResult}<T> FindBindableMember<T>({Classes.Id} id)
                 """)
            .BeginBlock()
            .AppendFindBindableMemberBody(span, true)
            .EndBlock();
    }

    private static CodeWriter AppendFindBindableMemberBody(this CodeWriter code, ViewModelDataSpan span, bool isGeneric)
    {
        code.AppendLine($"#if !{Defines.ASPID_MVVM_UNITY_PROFILER_DISABLED}")
            .AppendLine("using (__findBindableMemberMarker.Auto())")
            .AppendLine("#endif")
            .BeginBlock();
        
        if (!span.IdLengthMemberGroups.IsEmpty)
        {
            AppendSwitchBlock("id.Length");

            foreach (var idGroup in span.IdLengthMemberGroups)
            {
                AppendCaseBlock($"{idGroup.Length}");
                AppendSwitchBlock("id.HashCode");
            
                foreach (var hasCodeGroup in idGroup.HashCodeGroup)
                {
                    AppendCaseBlock($"{hasCodeGroup.HashCode}");
                    AppendSwitchBlock("id.Value");
                
                    foreach (var member in hasCodeGroup.Members)
                    {
                        var type = member.Type;
                        
                        AppendCaseBlock(member.Id.ToString());
                        
                        if (isGeneric)
                        {
                            code.Append($"var __result__ = new {Classes.FindBindableMemberResult}<{type}>(true, ");
                            code.AppendBindableMemberInstance(member)
                                .AppendLine(");")
                                .AppendLine($"return {Classes.Unsafe}.As<{Classes.FindBindableMemberResult}<{type}>, {Classes.FindBindableMemberResult}<T>>(ref __result__);");
                        }
                        else
                        {
                            code.Append($"return new {Classes.FindBindableMemberResult}(true, ");
                            code.AppendBindableMemberInstance(member)
                                .AppendLine(");");
                        }

                        code.EndBlock();
                    }
                
                    code.EndBlock()
                        .AppendLine()
                        .AppendLine("break;")
                        .EndBlock();
                }

                code.EndBlock()
                    .AppendLine()
                    .AppendLine("break;")
                    .EndBlock();
            }

            code.EndBlock()
                .AppendLine();
        }

        var returnCode = span.Inheritor is Inheritor.None
            ? "return default;" 
            : !isGeneric 
                ? "return base.FindBindableMember(id);"
                : "return base.FindBindableMember<T>(id);";
        
        code.AppendLine(returnCode)
            .EndBlock();

        return code;

        void AppendSwitchBlock(string conditional) => 
            code.AppendLine($"switch({conditional})")
                .BeginBlock();
        
        void AppendCaseBlock( string conditional) => 
            code.AppendLine($"case {conditional}:")
                .BeginBlock();
    }
}