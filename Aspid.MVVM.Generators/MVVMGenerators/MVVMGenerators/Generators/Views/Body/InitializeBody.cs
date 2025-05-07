using System;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Generators.Views.Data;
using MVVMGenerators.Helpers.Extensions.Writer;
using MVVMGenerators.Generators.Views.Data.Members;
using MVVMGenerators.Generators.Views.Body.Extensions;
using MVVMGenerators.Helpers.Extensions.Symbols;

namespace MVVMGenerators.Generators.Views.Body;

// ReSharper disable InconsistentNaming
// ReSharper disable once InconsistentNaming
public static class InitializeBody
{
    private const string GeneratedAttribute = General.GeneratedCodeViewAttribute;
    
    private static readonly string IViewModel = Classes.IViewModel.Global;
    private static readonly string ProfilerMarker = Classes.ProfilerMarker.Global;
    private static readonly string EditorBrowsableAttribute = $"[{Classes.EditorBrowsableAttribute.Global}({Classes.EditorBrowsableState.Global}.Never)]";

    public static void Generate(
        string @namespace,
        in ViewDataSpan data,
        in DeclarationText declaration,
        in SourceProductionContext context)
    {
        var code = new CodeWriter();
        var baseTypes = GetBaseTypes(data);

        code.AppendClassBegin([Namespaces.Aspid_MVVM], @namespace, declaration, baseTypes)
            .AppendIView(data)
            .AppendClassEnd(@namespace);
        
        context.AddSource(declaration.GetFileName(@namespace, "Initialize"), code.GetSourceText());
    }
    
    private static CodeWriter AppendIView(this CodeWriter code, in ViewDataSpan data)
    {
        code = data.Inheritor switch
        {
            Inheritor.None => code.AppendNone(data),
            Inheritor.InheritorViewAttribute => code.AppendHasInterfaceOrInheritor(data),
            _ => throw new ArgumentOutOfRangeException()
        };
        
        var isInstantiateBinders = data.MembersByType.AsBinders.Length + data.MembersByType.PropertyBinders.Length > 0;
        if (!isInstantiateBinders) return code;
        
        code.AppendLine()
            .AppendInstantiateBindersMethods(data);
        
        return code;
    }

    private static CodeWriter AppendNone(this CodeWriter code, in ViewDataSpan data)
    {
        var className = data.Declaration.Identifier.Text;

        code.AppendProfilerMarkers(className)
            .AppendLine()
            .AppendMultiline(
                $$"""
                {{GeneratedAttribute}}
                public {{IViewModel}} ViewModel { get; private set; }
                
                {{GeneratedAttribute}}
                public void Initialize({{IViewModel}} viewModel)
                {
                    if (viewModel is null) throw new {{Classes.ArgumentNullException.Global}}(nameof(viewModel));
                    if (ViewModel is not null) throw new {{Classes.InvalidOperationException.Global}}("View is already initialized.");
                    
                    ViewModel = viewModel;
                    InitializeInternal(viewModel);
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
                public void Deinitialize()
                {
                    if (ViewModel is null) return;
                
                    DeinitializeInternal();
                    ViewModel = null;
                }

                {{GeneratedAttribute}}
                protected virtual void DeinitializeInternal()
                """)
            .AppendDeinitializeBody(data)
            .AppendDeinitializeInternalEvents();

        return code;
    }

    private static CodeWriter AppendHasInterfaceOrInheritor(this CodeWriter code, in ViewDataSpan data)
    {
        const bool isOverride = true;
        var className = data.Declaration.Identifier.Text;

        code.AppendProfilerMarkers(className)
            .AppendLine();
            
        code.AppendInitializeInternalDeclaration(isOverride)
            .AppendInitializeBody(data, isOverride)
            .AppendInitializeInternalEvents();
        
        code.AppendDeinitializeInternalDeclaration(isOverride)
            .AppendDeinitializeBody(data, isOverride)
            .AppendDeinitializeInternalEvents();
        
        return code;
    }
    
    private static CodeWriter AppendProfilerMarkers(this CodeWriter code, string className)
    {
        return code.AppendMultiline(
            $"""
             #if !{Defines.ASPID_MVVM_UNITY_PROFILER_DISABLED}
             {EditorBrowsableAttribute}
             {GeneratedAttribute}
             private static readonly {ProfilerMarker} __initializeMarker = new("{className}.Initialize");
             
             {EditorBrowsableAttribute}
             {GeneratedAttribute}
             private static readonly {ProfilerMarker} __deinitializeMarker = new("{className}.Deinitialize");
             #endif
             """);
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

    private static CodeWriter AppendInitializeBody(this CodeWriter code, in ViewDataSpan data, bool isOverride = false)
    {
        code.BeginBlock()
            .AppendMultiline(
                $"""
                #if !{Defines.ASPID_MVVM_UNITY_PROFILER_DISABLED}
                using (__initializeMarker.Auto())
                #endif
                """)
            .BeginBlock();

        for (var i = 0; i < data.GenericViews.Length; i++)
        {
            code.AppendLine(
                    $"if (viewModel is {data.GenericViews[i].ToDisplayStringGlobal()}.IBindableMembers specificViewModel{i})")
                .BeginBlock()
                .AppendLine($"InitializeInternal(specificViewModel{i});")
                .AppendLine("return;")
                .EndBlock();
        }

        var isInstantiateBinders = data.MembersByType.PropertyBinders.Length + data.MembersByType.AsBinders.Length > 0;
        
        code.AppendLineIf(data.GenericViews.Length > 0)
            .AppendLine("OnInitializingInternal(viewModel);")
            .AppendLineIf(isOverride, "base.InitializeInternal(viewModel);")
            .AppendLineIf(isInstantiateBinders, "InstantiateBinders();")
            .AppendLine();

        foreach (var member in data.Members)
            code.AppendBindSafely(member);
        
        code.AppendLine("\nOnInitializedInternal(viewModel);");
        code.EndBlock();
        code.EndBlock();
        
        return code;
    }

    private static CodeWriter AppendDeinitializeBody(this CodeWriter code, in ViewDataSpan data, bool isOverride = false)
    {
        code.BeginBlock()
            .AppendMultiline(
                $"""
                 #if !{Defines.ASPID_MVVM_UNITY_PROFILER_DISABLED}
                 using (__deinitializeMarker.Auto())
                 #endif
                 """)
            .BeginBlock();
        
        code.AppendLine("OnDeinitializingInternal();");
        code.AppendLineIf(isOverride, "base.DeinitializeInternal();");
        code.AppendLine();
        
        foreach (var member in data.Members)
        {
            if (member is CachedBinderMember cachedBinderMember)
            {
                code.AppendLine($"{cachedBinderMember.CachedName}.UnbindSafely();");
            }
            else
            {
                code.AppendLine($"{member.Name}.UnbindSafely();");
            }
        }

        code.AppendLine("\nOnDeinitializedInternal();");
        code.EndBlock();
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
        var asBinderMembers = data.MembersByType.AsBinders;
        var propertyMembers = data.MembersByType.PropertyBinders;
        
        if (propertyMembers.Length > 0)
        {
            foreach (var member in propertyMembers)
            {
                code.AppendLine(member.IsUnityEngineObject 
                    ? $"if (!{member.CachedName}) {member.CachedName} = {member.Name};" 
                    : $"{member.CachedName} ??= {member.Name};");
            }
            
            if (asBinderMembers.Length > 0) 
                code.AppendLine();
        }
        
        if (asBinderMembers.Length > 0)
        {
            var isAppend = false;
            var membersCount = asBinderMembers.Length;

            for (var i = 0; i < asBinderMembers.Length; i++)
            {
                var member = asBinderMembers[i];
                
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
                        ? $"if ({member.Name}) {member.CachedName} ??= new {member.AsBinderType}({member.Name}{member.Arguments});" 
                        : $"{member.CachedName} ??= new {member.AsBinderType}({member.Name}{member.Arguments});");
                }
            }
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
    
    private static string[]? GetBaseTypes(in ViewDataSpan data) =>
        data.Inheritor is Inheritor.None ? [Classes.IView.ToString()] : null;
}