using System.Linq;
using Microsoft.CodeAnalysis;
using Aspid.Generator.Helpers;
using System.Collections.Generic;
using Aspid.MVVM.Generators.Views.Data;
using Aspid.MVVM.Generators.Descriptions;
using Aspid.MVVM.Generators.ViewModels.Factories;
using Aspid.MVVM.Generators.Views.Body.Extensions;
using Aspid.MVVM.Generators.ViewModels.Data.Members;

namespace Aspid.MVVM.Generators.Views.Body;

public static class GenericInitializeView
{
    public static void Generate(
        string @namespace,
        in ViewDataSpan data,
        in DeclarationText declaration,
        in SourceProductionContext context)
    {
        foreach (var genericView in data.GenericViews)
        {
            var code = new CodeWriter();
            
            var baseTypes = genericView is { IsSelf: true, Type.TypeKind: not TypeKind.Interface }
                ? new[] { $"{Classes.IView}<{genericView.Type.ToDisplayStringGlobal()}.IBindableMembers>" }
                : null;

            code.AppendClassBegin([Namespaces.Aspid_MVVM], @namespace, declaration, baseTypes)
                .AppendGenericViews(data, genericView)
                .AppendClassEnd(@namespace);
            
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
        var typeBindableMembersName = genericView.Type.TypeKind is not TypeKind.Interface
            ? $"{typeName}.IBindableMembers"
            : $"{typeName}";

        if (genericView.IsSelf)
        {
            if (genericView.Type.TypeKind is not TypeKind.Interface)
            {
                code.AppendMultiline(
                    $$"""
                      public void Initialize({{typeName}} viewModel)
                      {
                          if (viewModel is null) throw new {{Classes.ArgumentNullException}}(nameof(viewModel));
                          if (ViewModel is not null) throw new {{Classes.InvalidOperationException}}("View is already initialized.");
                          
                          ViewModel = viewModel;
                          InitializeInternal({{Classes.Unsafe}}.As<{{typeName}}, {{typeBindableMembersName}}>(ref viewModel));
                      }
                      """);
            
                code.AppendLine();
            }
        
            code.AppendMultiline(
                $$"""
                  public void Initialize({{typeBindableMembersName}} viewModel)
                  {
                      if (viewModel is null) throw new {{Classes.ArgumentNullException}}(nameof(viewModel));
                      if (ViewModel is not null) throw new {{Classes.InvalidOperationException}}("View is already initialized.");
                      
                      ViewModel = viewModel;
                      InitializeInternal(viewModel);
                  }
                  """);
            
            code.AppendLine();
        }
        
        code.AppendLine($"{modifier} void InitializeInternal({typeBindableMembersName} viewModel)");
        code.BeginBlock();
        
        code.AppendLine($"#if !{Defines.ASPID_MVVM_UNITY_PROFILER_DISABLED}")
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
            var bindableMembers = new Dictionary<string, BindableMember>();

            for (var viewModelType = genericView.Type; viewModelType is not null; viewModelType = viewModelType.BaseType)
            {
                foreach (var memberPair in  
                         BindableMembersFactory.Create(viewModelType)
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
                    code.AppendBindSafely(member, bindableMember.PropertyName);
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
            #if !{Defines.ASPID_MVVM_UNITY_PROFILER_DISABLED}
            private readonly static {Classes.ProfilerMarker} __initialize{viewModelTypeName.Replace(".", "_")}Marker = new("{data.Declaration.Identifier.Text}.{viewModelTypeName}.Initialize");
            #endif
            """);
    }
}