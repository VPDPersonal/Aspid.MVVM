using System;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Generators.Views.Data;
using MVVMGenerators.Helpers.Extensions.Writer;

namespace MVVMGenerators.Generators.Views.Body;

// ReSharper disable InconsistentNaming
// ReSharper disable once InconsistentNaming
public static class ViewBody
{
    private const string GeneratedAttribute = General.GeneratedCodeViewAttribute;

    private static readonly string IView = Classes.IView.Global;
    private static readonly string IViewModel = Classes.IViewModel.Global;
    private static readonly string ProfilerMarker = Classes.ProfilerMarker.Global;

    public static CodeWriter AppendIView(this CodeWriter code, in ViewDataSpan data)
    {
        code = data.Inheritor switch
        {
            Inheritor.None => code.AppendNone(data),
            Inheritor.HasInterface => code.AppendHasInterface(data),
            Inheritor.InheritorView => code.AppendInheritorMonoView(data),
            Inheritor.InheritorViewAttribute => code.AppendInheritorViewAttribute(data),
            _ => throw new ArgumentOutOfRangeException()
        };
        
        var isInstantiateBinders = data.ViewProperties.Length + data.AsBinderMembers.Length > 0;
        if (!isInstantiateBinders) return code;
        
        code.AppendLine()
            .AppendInstantiateBindersMethods(data);

        return code;
    }

    private static CodeWriter AppendNone(this CodeWriter code, in ViewDataSpan data)
    {
        var className = data.Declaration.Identifier.Text;

        code.AppendMultiline(
                $$"""
                #if !{{Defines.ASPID_MVVM_UNITY_PROFILER_DISABLED}}
                {{GeneratedAttribute}}
                private static readonly {{ProfilerMarker}} __initializeMarker = new("{{className}}.Initialize");
                
                {{GeneratedAttribute}}
                private static readonly {{ProfilerMarker}} __deinitializeMarker = new("{{className}}.Deinitialize");
                #endif
                
                {{GeneratedAttribute}}
                public {{IViewModel}} ViewModel { get; private set; }
                
                {{GeneratedAttribute}}
                void {{IView}}.Initialize({{IViewModel}} viewModel)
                {
                    #if !{{Defines.ASPID_MVVM_UNITY_PROFILER_DISABLED}}
                    using (__initializeMarker.Auto())
                    #endif
                    {
                        if (viewModel is null) throw new {{Classes.ArgumentNullException.Global}}(nameof(viewModel));
                        if (ViewModel is not null) throw new {{Classes.InvalidOperationException.Global}}("View is already initialized.");
                    
                        ViewModel = viewModel;
                        InitializeInternal(viewModel);
                    }
                }
                
                {{GeneratedAttribute}}
                protected virtual void InitializeInternal({{IViewModel}} viewModel)
                """)
            .AppendInitializeBody(data)
            .AppendInitializeInternalEvents()
            .AppendLine()
            .AppendMultiline(
                $$"""
                {{GeneratedAttribute}}
                void {{IView}}.Deinitialize()
                {
                    if (ViewModel == null) return;
                
                    #if !{{Defines.ASPID_MVVM_UNITY_PROFILER_DISABLED}}
                    using (__deinitializeMarker.Auto())
                    #endif
                    {
                        DeinitializeInternal();
                        ViewModel = null;
                    }
                }

                {{GeneratedAttribute}}
                protected virtual void DeinitializeInternal()
                """)
            .AppendDeinitializeBody(data)
            .AppendDeinitializeInternalEvents();

        return code;
    }

    private static CodeWriter AppendInheritorViewAttribute(this CodeWriter code, in ViewDataSpan data)
    {
        const bool isOverride = true;
        
        if (!data.IsInitializeOverride)
        {
            code.AppendInitializeInternalDeclaration(isOverride)
                .AppendInitializeBody(data, true)
                .AppendInitializeInternalEvents();
        }
        
        if (!data.IsDeinitializeOverride)
        {
            code.AppendLineIf(!data.IsInitializeOverride)
                .AppendDeinitializeInternalDeclaration(isOverride)
                .AppendDeinitializeBody(data, true)
                .AppendDeinitializeInternalEvents();
        }

        return code;
    }

    public static CodeWriter AppendInheritorMonoView(this CodeWriter code, in ViewDataSpan data)
    {
        const bool isOverride = true;
        
        if (!data.IsInitializeOverride)
        {
            code.AppendInitializeInternalDeclaration(isOverride)
                .AppendInitializeBody(data)
                .AppendInitializeInternalEvents();
        }
        
        if (!data.IsDeinitializeOverride)
        {
            code.AppendLineIf(!data.IsInitializeOverride)
                .AppendDeinitializeInternalDeclaration(isOverride)
                .AppendDeinitializeBody(data)
                .AppendDeinitializeInternalEvents();
        }
        
        return code;
    }

    public static CodeWriter AppendHasInterface(this CodeWriter code, in ViewDataSpan data)
    {
        const bool isOverride = false;
        
        if (!data.IsInitializeOverride)
        {
            code.AppendInitializeInternalDeclaration(isOverride)
                .AppendInitializeBody(data)
                .AppendInitializeInternalEvents();
        }
        
        if (!data.IsDeinitializeOverride)
        {
            code.AppendLineIf(!data.IsInitializeOverride)
                .AppendDeinitializeInternalDeclaration(isOverride)
                .AppendDeinitializeBody(data)
                .AppendDeinitializeInternalEvents();
        }
        
        return code;
    }

    private static CodeWriter AppendDeinitializeInternalDeclaration(this CodeWriter code, bool isOverride)
    {
        var modificator = isOverride ? "override" : "virtual";
        
        code.AppendMultiline(
            $"""
             {GeneratedAttribute}
             protected {modificator} void DeinitializeInternal()
             """);

        return code; 
    }

    private static CodeWriter AppendInitializeInternalDeclaration(this CodeWriter code, bool isOverride)
    {
        var modificator = isOverride ? "override" : "virtual";
        
        code.AppendMultiline(
            $"""
            {GeneratedAttribute}
            protected {modificator} void InitializeInternal({IViewModel} viewModel)
            """);

        return code;
    }

    private static CodeWriter AppendInitializeBody(this CodeWriter code, in ViewDataSpan data, bool isOverride = false)
    {
        code.BeginBlock();
        code.AppendLine("OnInitializingInternal(viewModel);");
        code.AppendLineIf(isOverride, "base.InitializeInternal(viewModel);");
        
        var isInstantiateBinders = data.ViewProperties.Length + data.AsBinderMembers.Length > 0;
        code.AppendLineIf(isInstantiateBinders, "InstantiateBinders();");
        code.AppendLine();
        
        foreach (var field in data.FieldMembers)
            code.AppendLine($"{field.FieldName}.BindSafely(viewModel, {field.Id});");
        
        foreach (var property in data.ViewProperties)
            code.AppendLine($"{property.CachedName}.BindSafely(viewModel, {property.Id});");

        foreach (var member in data.AsBinderMembers)
        {
            if (member.CachedName is null || member.Id is null) continue;
            code.AppendLine($"{member.CachedName}.BindSafely(viewModel, {member.Id});");
        }

        code.AppendLine("\nOnInitializedInternal(viewModel);");
        code.EndBlock();
        
        return code;
    }

    private static CodeWriter AppendDeinitializeBody(this CodeWriter code, in ViewDataSpan data, bool isOverride = false)
    {
        code.BeginBlock();
        code.AppendLine("OnDeinitializingInternal();");
        code.AppendLineIf(isOverride, "base.DeinitializeInternal();");
        code.AppendLine();
        
        foreach (var field in data.FieldMembers)
            code.AppendLine($"{field.FieldName}.UnbindSafely();");
        
        foreach (var property in data.ViewProperties)
            code.AppendLine($"{property.CachedName}.UnbindSafely();");

        foreach (var member in data.AsBinderMembers)
        {
            if (member.CachedName is null || member.Id is null) continue;
            code.AppendLine($"{member.CachedName}.UnbindSafely();");
        }

        code.AppendLine("\nOnDeinitializedInternal();");
        code.EndBlock();

        return code;
    }
    
    private static CodeWriter AppendInstantiateBindersMethods(this CodeWriter code, in ViewDataSpan data)
    {
        code.AppendMultiline(
                $"""
                {GeneratedAttribute}
                private void InstantiateBinders()
                """)
            .BeginBlock()
            .AppendLine("OnInstantiatingBinders();\n")
            .AppendCreateBinders(data)
            .AppendLine("\nOnInstantiatedBinders();")
            .EndBlock()
            .AppendMultiline(
                $"""
                
                {GeneratedAttribute}
                partial void OnInstantiatingBinders();
                
                {GeneratedAttribute}
                partial void OnInstantiatedBinders();
                """);

        return code;
    }

    private static CodeWriter AppendCreateBinders(this CodeWriter code, in ViewDataSpan data)
    {
        if (data.ViewProperties.Length > 0)
        {
            code.AppendLoop(data.ViewProperties, member =>
            {
                code.AppendLine(member.IsUnityEngineObjectBinder 
                    ? $"if (!{member.CachedName}) {member.CachedName} = {member.PropertyName};" 
                    : $"{member.CachedName} ??= {member.PropertyName};");
            });
            
            if (data.AsBinderMembers.Length > 0) 
                code.AppendLine();
        }

        if (data.AsBinderMembers.Length > 0)
        {
            var isAppend = false;
            var membersCount = data.AsBinderMembers.Length;
            
            code.AppendLoop(data.AsBinderMembers, (i, member) =>
            {
                if (member.Type is IArrayTypeSymbol)
                {
                    var name = member.Name;
                    var localName = $"__local{name}";
                    var binderName = member.CachedName;
                    var arguments = member.Arguments;
                    var binderType = member.AsBinderType;
                    
                    code.AppendLineIf(isAppend)
                        .AppendMultiline(
                        $$"""
                        if ({{binderName}} == null)
                        {
                            var {{localName}} = {{name}};
                            {{binderName}} = new {{binderType}}[{{localName}}.Length];
                            
                            for (var i = 0; i < {{localName}}.Length; i++)
                                {{binderName}}[i] = new {{member.AsBinderType}}({{localName}}[i]{{arguments}});
                        }
                        """)
                        .AppendLineIf(i + 1 < membersCount);

                    isAppend = false;
                }
                else
                {
                    isAppend = true;
                    code.AppendLine(member.IsUnityEngineObject 
                        ? $"if ({member.Name}) {member.CachedName} ??= new {member.AsBinderType}({member.Name});" 
                        : $"{member.CachedName} ??= new {member.AsBinderType}({member.Name}{member.Arguments});");
                }
            });
        }

        return code;
    }
    
    private static CodeWriter AppendInitializeInternalEvents(this CodeWriter code)
    {
        return code.AppendMultiline(
            $"""
             
             {GeneratedAttribute}
             partial void OnInitializingInternal({IViewModel} viewModel);

             {GeneratedAttribute}
             partial void OnInitializedInternal({IViewModel} viewModel);
             """);
    }
    
    private static CodeWriter AppendDeinitializeInternalEvents(this CodeWriter code)
    {
        return code.AppendMultiline(
            $"""
             
             {GeneratedAttribute}
             partial void OnDeinitializingInternal();

             {GeneratedAttribute}
             partial void OnDeinitializedInternal();
             """);
    }
}