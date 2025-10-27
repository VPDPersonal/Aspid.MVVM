using Microsoft.CodeAnalysis;
using Aspid.Generators.Helper;
using Aspid.MVVM.Generators.Generators.Views.Data;
using Aspid.MVVM.Generators.Generators.Descriptions;
using Aspid.MVVM.Generators.Generators.Views.Data.Members;
using Aspid.MVVM.Generators.Generators.Views.Body.Extensions;
using static Aspid.Generators.Helper.Classes;
using static Aspid.Generators.Helper.Unity.UnityClasses;
using static Aspid.MVVM.Generators.Generators.Descriptions.Classes;

namespace Aspid.MVVM.Generators.Generators.Views.Body;

// ReSharper disable InconsistentNaming
// ReSharper disable once InconsistentNaming
public static class InitializeBody
{
    private const string GeneratedAttribute = General.GeneratedCodeViewAttribute;

    public static void Generate(
        string @namespace,
        in ViewDataSpan data,
        DeclarationText declaration,
        in SourceProductionContext context)
    {
        var code = new CodeWriter();
        var baseTypes = GetBaseTypes(data);

        code.BeginClass([Namespaces.Aspid_MVVM], @namespace, declaration, baseTypes)
            .AppendIView(data)
            .EndClass(@namespace);
        
        context.AddSource(declaration.GetFileName(@namespace, "Initialize"), code.GetSourceText());
    }
    
    private static CodeWriter AppendIView(this CodeWriter code, in ViewDataSpan data)
    {
        code = data.Inheritor switch
        {
            Inheritor.None => code.AppendNone(data),
            Inheritor.InheritorViewAttribute => code.AppendHasInterfaceOrInheritor(data),
            _ => code
        };
        
        if (!data.IsInstantiateBinders) return code;
        
        code.AppendLine()
            .AppendInstantiateBindersMethods(data);
        
        return code;
    }

    private static CodeWriter AppendNone(this CodeWriter code, in ViewDataSpan data)
    {
        var className = data.Declaration.Identifier.Text;
        var modifiers = !data.Symbol.IsSealed ? "protected virtual" : "private";

        code.AppendProfilerMarkers(className)
            .AppendLine()
            .AppendMultiline(
                $"""
                 [global::System.NonSerialized]
                 {GeneratedAttribute}
                 [{EditorBrowsableAttribute}({EditorBrowsableState}.Never)]
                 private bool __isInitializing;
                 
                 """)
            .AppendMultilineIf(data.IsInstantiateBinders, 
                $"""
                [global::System.NonSerialized]
                {GeneratedAttribute}
                [{EditorBrowsableAttribute}({EditorBrowsableState}.Never)]
                private bool __isBindersCached;
                
                """)
            .AppendMultiline(
                $$"""
                {{GeneratedAttribute}}
                public {{IViewModel}} ViewModel { get; protected set; }
                
                {{GeneratedAttribute}}
                public void Initialize({{IViewModel}} viewModel)
                {
                    if (viewModel is null) throw new {{ArgumentNullException}}(nameof(viewModel));
                    if (ViewModel is not null) throw new {{InvalidOperationException}}("View is already initialized.");
                    
                    ViewModel = viewModel;
                    InitializeInternal(viewModel);
                }
                
                {{GeneratedAttribute}}
                {{modifiers}} void InitializeInternal({{IViewModel}} viewModel)
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
                {{modifiers}} void DeinitializeInternal()
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
            .AppendLine()
            .AppendMultiline(
                $"""
                 [global::System.NonSerialized]
                 {GeneratedAttribute}
                 [{EditorBrowsableAttribute}({EditorBrowsableState}.Never)]
                 private bool __isInitializing;
                 
                 """)
            .AppendMultilineIf(data.IsInstantiateBinders, 
                $"""
                [global::System.NonSerialized]
                {GeneratedAttribute}
                [{EditorBrowsableAttribute}({EditorBrowsableState}.Never)]
                private bool __isBindersCached;
                
                """);
            
        code.AppendInitializeInternalDeclaration(data)
            .AppendInitializeBody(data, isOverride)
            .AppendInitializeInternalEvents()
            .AppendLine();
        
        code.AppendDeinitializeInternalDeclaration(data)
            .AppendDeinitializeBody(data, isOverride)
            .AppendDeinitializeInternalEvents();
        
        return code;
    }
    
    private static CodeWriter AppendProfilerMarkers(this CodeWriter code, string className)
    {
        return code.AppendMultiline(
            $"""
             #if !{Defines.ASPID_MVVM_UNITY_PROFILER_DISABLED}
             [{EditorBrowsableAttribute}({EditorBrowsableState}.Never)]
             {GeneratedAttribute}
             private static readonly {ProfilerMarker} __initializeMarker = new("{className}.Initialize");
             
             [{EditorBrowsableAttribute}({EditorBrowsableState}.Never)]
             {GeneratedAttribute}
             private static readonly {ProfilerMarker} __deinitializeMarker = new("{className}.Deinitialize");
             #endif
             """);
    }
    
    private static CodeWriter AppendInitializeInternalDeclaration(this CodeWriter code, in ViewDataSpan data)
    {
        var modifiers = "private";
        if (data.Inheritor is not Inheritor.None) modifiers = "protected override";
        else if (!data.Symbol.IsSealed) modifiers = "protected virtual";
        
        code.AppendMultiline(
            $"""
             {GeneratedAttribute}
             {modifiers} void InitializeInternal({IViewModel} viewModel)
             """);

        return code;
    }

    private static CodeWriter AppendDeinitializeInternalDeclaration(this CodeWriter code, in ViewDataSpan data)
    {
        var modifiers = "private";
        if (data.Inheritor is not Inheritor.None) modifiers = "protected override";
        else if (!data.Symbol.IsSealed) modifiers = "protected virtual";
        
        code.AppendMultiline(
            $"""
             {GeneratedAttribute}
             {modifiers} void DeinitializeInternal()
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

        code.AppendLine("if (__isInitializing)")
            .BeginBlock()
            .AppendLineIf(isOverride, "base.InitializeInternal(viewModel);")
            .AppendLine("return;")
            .EndBlock()
            .AppendLine();

        for (var i = 0; i < data.GenericViews.Length; i++)
        {
            var type = data.GenericViews[i].Type.ToDisplayStringGlobal();
            
            code.AppendLine(
                    $"if (viewModel is {type} specificViewModel{i})")
                .BeginBlock()
                .AppendLine($"InitializeInternal(specificViewModel{i});")
                .AppendLine("return;")
                .EndBlock();
        }
        
        code.AppendLineIf(data.GenericViews.Length > 0)
            .AppendLine("__isInitializing = true;")
            .AppendLine("OnInitializingInternal(viewModel);")
            .AppendLineIf(isOverride, "base.InitializeInternal(viewModel);")
            .AppendLineIf(data.IsInstantiateBinders, "InstantiateBinders();")
            .AppendLine();

        foreach (var member in data.Members)
            code.AppendBindSafely(member);
        
        return code.AppendLine()
            .AppendLine("OnInitializedInternal(viewModel);")
            .AppendLine("__isInitializing = false;")
            .EndBlock()
            .EndBlock();
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

        code.AppendLine()
            .AppendLine("OnDeinitializedInternal();")
            .EndBlock()
            .EndBlock();

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
            .AppendLine("if (__isBindersCached) return;")
            .AppendLine("OnInstantiatingBinders();")
            .AppendLine()
            .AppendCreateBinders(data)
            .AppendLine()
            .AppendLine("OnInstantiatedBinders();")
            .AppendLine("__isBindersCached = true;")
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
                code.AppendLine($"{member.CachedName} = {member.Name};");
            
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
                            $"""
                              var {localName} = {name};
                              {binderName} = new {binderType}[{localName}.Length];
                              
                              for (var i = 0; i < {localName}.Length; i++)
                                  {binderName}[i] = new {member.AsBinderType}({localName}[i]{arguments});
                              """)
                        .AppendLineIf(i + 1 < membersCount);

                    isAppend = false;
                }
                else
                {
                    isAppend = true;
                    code.AppendLine($"{member.CachedName} = new {member.AsBinderType}({member.Name}{member.Arguments});");
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
        data.Inheritor is Inheritor.None ? [IView.ToString()] : null;
}