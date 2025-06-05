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
            
            var baseTypes = viewModelType.TypeKind is not TypeKind.Interface 
                ? new[] { $"{Classes.IView}<{viewModelType.ToDisplayStringGlobal()}.IBindableMembers>" }
                : null;

            code.AppendClassBegin([Namespaces.Aspid_MVVM], @namespace, declaration, baseTypes)
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
        var modifier = data.Symbol.IsSealed ? "private" : "protected";
        var typeName = viewModelType.ToDisplayStringGlobal();
        var typeBindableMembersName = viewModelType.TypeKind is not TypeKind.Interface
            ? $"{typeName}.IBindableMembers"
            : $"{typeName}";

        if (viewModelType.TypeKind is not TypeKind.Interface)
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
        code.AppendLine($"{modifier} void InitializeInternal({typeBindableMembersName} viewModel)");
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

        if (viewModelType.TypeKind is not TypeKind.Interface)
        {
            var bindableMembers = BindableMembersFactory.Create(viewModelType)
                .ToDictionary(bindable => bindable.Id.SourceValue, bindable => bindable);

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
            var customViewModelInterfaces = CustomViewModelInterfacesFactory.Create(viewModelType);

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
            .AppendLine("OnInitializedInternal(viewModel);");

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