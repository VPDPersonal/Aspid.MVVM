using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using System.Collections.Generic;
using System.Collections.Immutable;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Helpers.Extensions.Writer;
using MVVMGenerators.Generators.ViewModels.Data;
using MVVMGenerators.Generators.ViewModels.Data.Members;

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
                 public {modifier} {Classes.FindBindableMemberResult} FindBindableMember(in {Classes.FindBindableMemberParameters} parameters)
                 """)
            .BeginBlock()
            .AppendFindBindableMemberBody(span)
            .EndBlock();
    }

    private static CodeWriter AppendFindBindableMemberBody(this CodeWriter code, ViewModelDataSpan span)
    {
        code.AppendLine($"#if !{Defines.ASPID_MVVM_UNITY_PROFILER_DISABLED}")
            .AppendLine("using (__findBindableMemberMarker.Auto())")
            .AppendLine("#endif")
            .BeginBlock();

        var addedMembers = new HashSet<BindableMember>();
        
        if (!span.IdLengthMemberGroups.IsEmpty)
        {
            AppendSwitchBlock("parameters.Id.Length");
            
            foreach (var idGroup in span.IdLengthMemberGroups)
            {
                AppendCaseBlock($"{idGroup.Length}");
                AppendSwitchBlock("parameters.Id");

                AppendIdBlock(idGroup.BindableMembers);
                
                code.EndBlock()
                    .AppendLine()
                    .AppendLine("return default;")
                    .EndBlock();
            }

            code.EndBlock()
                .AppendLine();
        }
        
        if (addedMembers.Count != span.Members.Length)
        {
            AppendSwitchBlock("parameters.Id");
            AppendIdBlock(span.Members);

            code.EndBlock()
                .AppendLine();
        }
        
        var returnCode = span.Inheritor is Inheritor.None
            ? "return default;" 
            : "return base.FindBindableMember(parameters);";
        
        code.AppendLine(returnCode)
            .EndBlock();

        return code;

        void AppendSwitchBlock(string conditional) => 
            code.AppendLine($"switch({conditional})")
                .BeginBlock();
        
        void AppendCaseBlock( string conditional) => 
            code.AppendLine($"case {conditional}:")
                .BeginBlock();

        void AppendIdBlock(ImmutableArray<BindableMember> members)
        {
            foreach (var member in members)
            {
                if (addedMembers.Contains(member)) continue;
                AppendCaseBlock(member.Id.ToString());

                code.AppendReturnFindBindableMemberResult(member)
                    .EndBlock();
                
                addedMembers.Add(member);
            }
        }
    }

    private static CodeWriter AppendReturnFindBindableMemberResult(this CodeWriter code, in BindableMember member)
    {
        var @event = member.Event;
        return code.AppendLine($"return new({@event.ToInstantiateFieldString()});");
    }
}