using System.Linq;
using Microsoft.CodeAnalysis;
using Aspid.Generators.Helper;
using System.Collections.Generic;
using Aspid.MVVM.Generators.Generators.Views.Data;
using Aspid.MVVM.Generators.Generators.Descriptions;
using Aspid.MVVM.Generators.Generators.ViewModels.Data.Infos;
using Aspid.MVVM.Generators.Generators.ViewModels.Factories;
using Aspid.MVVM.Generators.Generators.Views.Body.Extensions;
using static Aspid.Generators.Helper.Classes;
using static Aspid.Generators.Helper.Unity.UnityClasses;
using static Aspid.MVVM.Generators.Generators.Descriptions.Defines;
using static Aspid.MVVM.Generators.Generators.Descriptions.General;

namespace Aspid.MVVM.Generators.Generators.Views.Body;

public static class GenericInitializeView
{
    public static void Generate(
        string @namespace,
        in ViewDataSpan data,
        DeclarationText declaration,
        in SourceProductionContext context)
    {
        foreach (var genericView in data.GenericViews)
        {
            var code = new CodeWriter();

            code.BeginClass([Namespaces.Aspid_MVVM], @namespace, declaration, null)
                .AppendGenericViews(data, genericView)
                .EndClass(@namespace);
            
            context.AddSource(declaration.GetFileName(@namespace, genericView.Type.ToDisplayString()), code.GetSourceText());
        }
    }
    
    private static CodeWriter AppendGenericViews(this CodeWriter code, in ViewDataSpan data, GenericViewData genericView)
    {
        code.AppendProfilerMarker(data, genericView.Type)
            .AppendLine()
            .AppendInitialize(data, genericView);

        return code;
    }

    private static CodeWriter AppendInitialize(this CodeWriter code, in ViewDataSpan data, GenericViewData genericView)
    {
        string modifier;
        if (genericView.IsSelf) modifier = data.Symbol.IsSealed ? "private" : "protected virtual";
        else modifier = "protected override";
        
        var typeName = genericView.Type.ToDisplayStringGlobal();

        if (genericView.IsSelf)
        {
            code.AppendMultiline(
                $$"""
                  {{GeneratedCodeViewAttribute}}
                  public void Initialize({{typeName}} viewModel)
                  {
                      if (viewModel is null) throw new {{ArgumentNullException}}(nameof(viewModel));
                      if (ViewModel is not null) throw new {{InvalidOperationException}}("View is already initialized.");
                      
                      ViewModel = viewModel;
                      InitializeInternal(viewModel);
                  }
                  """);
            
            code.AppendLine();
        }
        
        code.AppendMultiline(
            $"""
            {GeneratedCodeViewAttribute}
            {modifier} void InitializeInternal({typeName} viewModel)
            """)
            .BeginBlock()
            .AppendLine($"#if !{ASPID_MVVM_UNITY_PROFILER_DISABLED}")
            .AppendLine($"using (__initialize{genericView.Type.ToDisplayString().Replace(".", "_")}Marker.Auto())")
            .AppendLine("#endif")
            .BeginBlock();

        code.AppendLine("if (__isInitializing)")
            .BeginBlock()
            .AppendLineIf(!genericView.IsSelf, "base.InitializeInternal(viewModel);")
            .AppendLine("return;")
            .EndBlock()
            .AppendLine()
            .AppendLine("__isInitializing = true;")
            .AppendLine();
        
        code.AppendLine("OnInitializingInternal(viewModel);");
        code.AppendLineIf(data.Inheritor is not Inheritor.None, "base.InitializeInternal(viewModel);");
        
        var isInstantiateBinders = data.MembersByType.PropertyBinders.Length + data.MembersByType.AsBinders.Length > 0;
        code.AppendLineIf(isInstantiateBinders, "InstantiateBinders();");
        code.AppendLine();

        if (genericView.Type.TypeKind is not TypeKind.Interface)
        {
            var bindableMembers = new Dictionary<string, IBindableMemberInfo>();

            for (var viewModelType = genericView.Type; viewModelType is not null; viewModelType = viewModelType.BaseType)
            {
                foreach (var memberPair in  
                         BindableMembersFactory.Create(viewModelType, data.Declaration, out _)
                             .ToDictionary(bindable => bindable.Id.SourceValue, bindable => bindable))
                {
                    bindableMembers.Add(memberPair.Key, memberPair.Value);
                }
            }

            foreach (var member in data.Members)
            {
                if (bindableMembers.TryGetValue(member.Id.SourceValue, out var bindableMember))
                {
                    code.AppendBindSafely(member, bindableMember);
                }
                else
                {
                    code.AppendBindSafely(member);
                }
            }
        }
        else
        {
            var customViewModelInterfaces = CustomViewModelInterfacesFactory.Create(genericView.Type);

            foreach (var member in data.Members)
            {
                if (customViewModelInterfaces.TryGetValue(member.Id.SourceValue, out var bindableMember))
                {
                    code.AppendBindSafely(member, bindableMember.Property.Name);
                }
                else
                {
                    code.AppendBindSafely(member);
                }
            }
        }
        
        code.AppendLine()
            .AppendLine("OnInitializedInternal(viewModel);")
            .AppendLine("__isInitializing = false;");

        code.EndBlock()  
            .EndBlock();

        return code;
    }

    private static CodeWriter AppendProfilerMarker(this CodeWriter code, in ViewDataSpan data, ITypeSymbol viewModelType)
    {
        var viewModelTypeName = viewModelType.ToDisplayString();
        
        return code.AppendMultiline(
            $"""
            #if !{ASPID_MVVM_UNITY_PROFILER_DISABLED}
            {GeneratedCodeViewAttribute}
            [{EditorBrowsableAttribute}({EditorBrowsableState}.Never)]
            private readonly static {ProfilerMarker} __initialize{viewModelTypeName.Replace(".", "_")}Marker = new("{data.Declaration.Identifier.Text}.{viewModelTypeName}.Initialize");
            #endif
            """);
    }
}