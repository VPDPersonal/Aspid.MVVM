using Microsoft.CodeAnalysis;
using Aspid.Generators.Helper;
using System.Collections.Generic;
using System.Collections.Immutable;
using Aspid.MVVM.Generators.Generators.ViewModels.Data;
using Aspid.MVVM.Generators.Generators.ViewModels.Data.Members;
using static Aspid.Generators.Helper.Classes;
using static Aspid.Generators.Helper.Unity.UnityClasses;
using static Aspid.MVVM.Generators.Generators.Descriptions.Classes;
using static Aspid.MVVM.Generators.Generators.Descriptions.Defines;
using static Aspid.MVVM.Generators.Generators.Descriptions.General;

namespace Aspid.MVVM.Generators.Generators.ViewModels.Body;

public static class FindBindableMembersBody
{
    public static void Generate(
        string @namespace,
        in ViewModelData data,
        DeclarationText declaration,
        in SourceProductionContext context)
    {
        string[] baseTypes = [IViewModel];

        var code = new CodeWriter();
        code.BeginClass(@namespace, declaration, baseTypes)
            .AppendBody(data)
            .EndClass(@namespace);

        context.AddSource(declaration.GetFileName(@namespace, "FindBindableMembers"), code.GetSourceText());
    }
    
    private static CodeWriter AppendBody(this CodeWriter code, in ViewModelData data)
    {
        return code
            .AppendMarkers(data)
            .AppendLine()
            .AppendFindBindableMember(data);
    }

    private static CodeWriter AppendMarkers(this CodeWriter code, in ViewModelData data)
    {
        var className = data.Name;
        
        return code.AppendMultiline(
            $"""
            #if !{ASPID_MVVM_UNITY_PROFILER_DISABLED}
            {GeneratedCodeViewModelAttribute}
            [{EditorBrowsableAttribute}({EditorBrowsableState}.Never)]
            private static readonly {ProfilerMarker} __findBindableMemberMarker = new("{className}.FindBindableMember");
            #endif
            """);
    }

    private static CodeWriter AppendFindBindableMember(this CodeWriter code, in ViewModelData data)
    {
        var addedMembers = new HashSet<BindableMember>();
        
        var modifiers = "public";
        if (data.Inheritor is not Inheritor.None) modifiers = "public override";
        else if (!data.Symbol.IsSealed) modifiers = "public virtual";
        
        code.AppendMultiline(
                $"""
                 {GeneratedCodeViewModelAttribute}
                 {modifiers} {FindBindableMemberResult} FindBindableMember(in {FindBindableMemberParameters} parameters)
                 """)
            .BeginBlock()
            .AppendMultiline(
                $"""
                #if !{ASPID_MVVM_UNITY_PROFILER_DISABLED}
                using (__findBindableMemberMarker.Auto())
                #endif
                """)
            .BeginBlock();
        
        if (!data.IdGroups.IsEmpty)
        {
            code.AppendLine("switch(parameters.Id.Length)")
                .BeginBlock();

            foreach (var idGroup in data.IdGroups)
            {
                code.AppendLine($"case {idGroup.Length}:")
                    .BeginBlock()
                    .AppendLine("switch(parameters.Id)")
                    .BeginBlock();

                AppendIdBlock(idGroup.Members);
                
                code.EndBlock()
                    .AppendLine()
                    .AppendLine(data.Inheritor is Inheritor.None
                        ? "return default;"
                        : "return base.FindBindableMember(parameters);")
                    .EndBlock();
            }

            code.EndBlock()
                .AppendLine();
        }

        if (addedMembers.Count != data.Members.Length)
        {
            code.AppendLine("switch(parameters.Id)")
                .BeginBlock();

            AppendIdBlock(data.Members);

            code.EndBlock()
                .AppendLine();
        }
        
        code.AppendLine(data.Inheritor is Inheritor.None
                ? "return default;"
                : "return base.FindBindableMember(parameters);")
            .EndBlock()
            .EndBlock();
        
        return code;

        void AppendIdBlock(ImmutableArray<BindableMember> members)
        {
            foreach (var member in members)
            {
                if (addedMembers.Contains(member)) continue;

                code.AppendMultiline(
                    $$"""
                      case {{member.Id}}:
                      {
                          return new({{member.GeneratedName}}Bindable);
                      }
                      """);
                
                addedMembers.Add(member);
            }
        }
    }
}