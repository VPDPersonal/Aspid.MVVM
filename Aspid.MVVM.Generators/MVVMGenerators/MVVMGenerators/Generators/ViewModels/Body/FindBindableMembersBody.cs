using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
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
            private static readonly {Classes.ProfilerMarker} __findBindableMemberMarkerT = new("{className}.FindBindableMember<T>");
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
            .AppendFindBindableMemberBody(span, false)
            .EndBlock()
            .AppendLine()
            .AppendMultiline(
                $"""
                 {General.GeneratedCodeViewModelAttribute}
                 public {modifier} {Classes.FindBindableMemberResult}<T> FindBindableMember<T>(in {Classes.FindBindableMemberParameters} parameters)
                 """)
            .BeginBlock()
            .AppendFindBindableMemberBody(span, true)
            .EndBlock();
    }

    private static CodeWriter AppendFindBindableMemberBody(this CodeWriter code, ViewModelDataSpan span, bool isGeneric)
    {
        code.AppendLine($"#if !{Defines.ASPID_MVVM_UNITY_PROFILER_DISABLED}")
            .AppendLine(isGeneric ? "using (__findBindableMemberMarkerT.Auto())" : "using (__findBindableMemberMarker.Auto())")
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
            : !isGeneric 
                ? "return base.FindBindableMember(parameters);"
                : "return base.FindBindableMember<T>(parameters);";
        
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
                        
                if (isGeneric)
                {
                    code.Append($"return {Classes.FindBindableMemberResult}<T>")
                        .AppendCreateFindBindableMemberResult(member, true)
                        .AppendLine(";");
                }
                else
                {
                    code.Append($"return {Classes.FindBindableMemberResult}")
                        .AppendCreateFindBindableMemberResult(member, false)
                        .AppendLine(";");
                }

                code.EndBlock();
                addedMembers.Add(member);
            }
        }
    }

    private static CodeWriter AppendCreateFindBindableMemberResult(this CodeWriter code, in BindableMember member, bool isGeneric)
    {
        switch (member.Mode)
        {
            case BindMode.OneWay:
                {
                    var methodName = "OneWay".GetCreateFindBindableMemberResultMethodName(member, isGeneric);
                    var @event = member is BindableField field ? field.Event : ((BindableBindAlso)member).Event;
                    return code.Append($".{methodName}({@event.FieldName} ??= new(), {member.SourceName})");
                }
            
            case BindMode.TwoWay:
                {
                    var methodName = "TwoWay".GetCreateFindBindableMemberResultMethodName(member, isGeneric);
                    var @event = member is BindableField field ? field.Event : ((BindableBindAlso)member).Event;
                    return code.Append($".{methodName}({@event.FieldName} ??= new(Set{member.GeneratedName}), {member.SourceName})");
                }
            
            case BindMode.OneTime:
                {
                    var methodName = "OneTime".GetCreateFindBindableMemberResultMethodName(member, isGeneric);
                    return code.Append($".{methodName}({member.SourceName})");
                }
            
            case BindMode.OneWayToSource:
                {
                    var methodName = "OneWayToSource".GetCreateFindBindableMemberResultMethodName(member, isGeneric);
                    var @event = member is BindableField field ? field.Event : ((BindableBindAlso)member).Event;
                    return code.Append($".{methodName}({@event.FieldName} ??= new(Set{member.GeneratedName}))");
                }
        }

        return code;
    }

    private static string GetCreateFindBindableMemberResultMethodName(this string methodName, BindableMember member, bool isGeneric)
    {
        if (isGeneric && member.IsValueType) 
            methodName += "ByValueType";
        
        return methodName;
    }
}