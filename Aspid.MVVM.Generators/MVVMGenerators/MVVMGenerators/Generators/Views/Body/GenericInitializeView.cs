using System.Linq;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Generators.Views.Data;
using MVVMGenerators.Helpers.Extensions.Writer;
using MVVMGenerators.Helpers.Extensions.Symbols;
using MVVMGenerators.Generators.ViewModels.Factories;
using MVVMGenerators.Generators.Views.Body.Extensions;

namespace MVVMGenerators.Generators.Views.Body;

public static class GenericInitializeView
{
    public static void Generate(
        string @namespace,
        in ViewDataSpan data,
        in DeclarationText declaration,
        in SourceProductionContext context)
    {
        foreach (var viewModelType in data.GenericViews)
        {
            var code = new CodeWriter();
            var baseType = new[] { $"{Classes.IView}<{viewModelType.ToDisplayStringGlobal()}.IBindableMembers>" };

            code.AppendClassBegin([Namespaces.Aspid_MVVM], @namespace, declaration, baseType)
                .AppendGenericViews(data, viewModelType)
                .AppendClassEnd(@namespace);
            
            context.AddSource(declaration.GetFileName(@namespace, viewModelType.ToDisplayString()), code.GetSourceText());
        }
    }
    
    private static CodeWriter AppendGenericViews(this CodeWriter code, in ViewDataSpan data, ITypeSymbol viewModelType)
    {
        code.AppendProfilerMarker(data, viewModelType)
            .AppendLine()
            .AppendInitialize(data, viewModelType);

        return code;
    }

    private static CodeWriter AppendInitialize(this CodeWriter code, in ViewDataSpan data, ITypeSymbol viewModelType)
    {
        var typeName = viewModelType.ToDisplayStringGlobal();
        var typeBindableMembersName = $"{typeName}.IBindableMembers";
        
        code.AppendMultiline(
            $$"""
            public void Initialize({{typeName}} viewModel)
            {
                if (ViewModel is null) return;
                InitializeInternal({{Classes.Unsafe}}.As<{{typeName}}, {{typeBindableMembersName}}>(ref viewModel));
            }
            """);

        code.AppendLine();
        
        code.AppendMultiline(
            $$"""
            public void Initialize({{typeBindableMembersName}} viewModel)
            {
                if (ViewModel is null) return;
                InitializeInternal(viewModel);
            }
            """);

        code.AppendLine();
        code.AppendLine($"protected void InitializeInternal({typeBindableMembersName} viewModel)");
        code.BeginBlock();
        
        code.AppendLine($"#if !{Defines.ASPID_MVVM_UNITY_PROFILER_DISABLED}")
            .AppendLine($"using (__initialize{viewModelType.ToDisplayString().Replace(".", "_")}Marker.Auto())")
            .AppendLine("#endif")
            .BeginBlock();
        
        code.AppendLine("OnInitializingInternal(viewModel);");
        code.AppendLineIf(data.Inheritor is not Inheritor.None, "base.InitializeInternal(viewModel);");
        
        var isInstantiateBinders = data.MembersByType.PropertyBinders.Length + data.MembersByType.AsBinders.Length > 0;
        code.AppendLineIf(isInstantiateBinders, "InstantiateBinders();");
        code.AppendLine();
        
        var bindableMembers = BindableMembersFactory.Create(viewModelType)
            .ToDictionary(bindable => bindable.Id, bindable => bindable);

        foreach (var member in data.Members)
        {
            if (bindableMembers.TryGetValue(member.Id, out var bindableMember))
            {
                code.AppendBindSafely(member, bindableMember);
            }
            else
            {
                code.AppendBindSafely(member);
            }
        }
        
        code.AppendLine()
            .AppendLine("OnInitializedInternal(viewModel);");

        code.EndBlock()
            .EndBlock();

        return code;
    }

    private static CodeWriter AppendProfilerMarker(this CodeWriter code, in ViewDataSpan data, ITypeSymbol viewModelType)
    {
        var viewModelTypeName = viewModelType.ToDisplayString().Replace(".", "_");
        
        return code.AppendMultiline(
            $"""
            #if !{Defines.ASPID_MVVM_UNITY_PROFILER_DISABLED}
            private readonly static {Classes.ProfilerMarker} __initialize{viewModelTypeName}Marker = new("{data.Declaration.Identifier.Text}.{viewModelTypeName}.Initialize");
            #endif
            """);
    }
}