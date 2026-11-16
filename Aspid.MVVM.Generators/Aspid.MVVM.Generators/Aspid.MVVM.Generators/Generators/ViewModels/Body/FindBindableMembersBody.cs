using Microsoft.CodeAnalysis;
using Aspid.MVVM.Generators.Helpers;
using Aspid.MVVM.Generators.Shared;
using Aspid.Generators.Helper;
using System.Collections.Generic;
using System.Collections.Immutable;
using Aspid.MVVM.Generators.Generators.ViewModels.Data;
using Aspid.MVVM.Generators.Generators.ViewModels.Data.Infos;
using static Aspid.Generators.Helper.Unity.UnityClasses;
using static Aspid.MVVM.Generators.Generators.Descriptions.Classes;
using static Aspid.MVVM.Generators.Generators.Descriptions.Defines;
using static Aspid.MVVM.Generators.Generators.Descriptions.Constants;

namespace Aspid.MVVM.Generators.Generators.ViewModels.Body;

public static class FindBindableMembersBody
{
    internal static void Generate(
        in ViewModelData data,
        AspidTypeDeclaration declaration,
        in SourceProductionContext context)
    {
        string[] baseTypes = [IViewModel];

        var code = new CodeWriter();
        code.BeginScopedClass(declaration, baseTypes)
            .AppendBody(data)
            .EndScopedClass(declaration);

        context.AddSource(declaration.GetFileName("FindBindableMembers"), code.GetSourceText());
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
            #if {ASPID_MVVM_PROFILER}
            {EditorBrowsableAttributeNever}
            {GeneratedCodeViewModelAttribute}
            private static readonly {ProfilerMarker} __findBindableMemberMarker = new("{className}.FindBindableMember");
            #endif
            """);
    }

    private static CodeWriter AppendFindBindableMember(this CodeWriter code, in ViewModelData data)
    {
        var addedMembers = new HashSet<IBindableMemberInfo>();
        
        var modifiers = "public";
        if (data.Inheritor is not Inheritor.None) modifiers = "public override";
        else if (!data.Symbol.IsSealed) modifiers = "public virtual";
        
        code.AppendMultiline(
                $"""
                 /// <inheritdoc/>
                 /// <exception cref="global::System.ArgumentNullException">The query identifier is <see langword="null"/>.</exception>
                 /// <exception cref="global::System.ArgumentException">The query identifier is empty or whitespace.</exception>
                 {GeneratedCodeViewModelAttribute}
                 {modifiers} {FindBindableMemberResult} FindBindableMember(in {FindBindableMemberParameters} parameters)
                 """)
            .BeginBlock()
            .AppendMultiline(
                $"""
                #if {ASPID_MVVM_PROFILER}
                using (__findBindableMemberMarker.Auto())
                #endif
                """)
            .BeginBlock()
            .AppendLine("parameters.EnsureValid();");
        
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

        void AppendIdBlock(ImmutableArray<IBindableMemberInfo> members)
        {
            foreach (var member in members)
            {
                if (addedMembers.Contains(member)) continue;

                code.AppendMultiline(
                    $$"""
                      case {{member.Id}}:
                      {
                          return new({{member.Bindable.PropertyName}});
                      }
                      """);
                
                addedMembers.Add(member);
            }
        }
    }
}
