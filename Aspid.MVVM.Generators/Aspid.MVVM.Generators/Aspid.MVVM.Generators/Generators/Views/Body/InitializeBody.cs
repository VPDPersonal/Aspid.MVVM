#nullable enable
using Microsoft.CodeAnalysis;
using Aspid.Generators.Helper;
using Aspid.MVVM.Generators.Generators.Views.Data;
using Aspid.MVVM.Generators.Generators.Descriptions;
using Aspid.MVVM.Generators.Generators.Views.Helpers;
using Aspid.MVVM.Generators.Generators.Views.Data.Members;
using Aspid.MVVM.Generators.Generators.Views.Body.Extensions;
using static Aspid.Generators.Helper.Classes;
using static Aspid.Generators.Helper.Unity.UnityClasses;
using static Aspid.MVVM.Generators.Generators.Descriptions.Classes;
using static Aspid.MVVM.Generators.Generators.Descriptions.Constants;

namespace Aspid.MVVM.Generators.Generators.Views.Body;

// ReSharper disable InconsistentNaming
// ReSharper disable once InconsistentNaming
public static class InitializeBody
{
    public static void Generate(
        string @namespace,
        in ViewDataSpan data,
        DeclarationText declaration,
        in SourceProductionContext context)
    {
        var code = new CodeWriter();
        var baseTypes = GetBaseTypes(data);

        code.BeginClass(
                [Namespaces.Aspid_MVVM, new("AspidBinderExtensions = global::Aspid.MVVM.BinderExtensions")],
                @namespace,
                declaration,
                baseTypes)
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
                 {GeneratedCodeViewAttribute}
                 {EditorBrowsableAttributeNever}
                 private bool __isInitializing;

                 [global::System.NonSerialized]
                 {GeneratedCodeViewAttribute}
                 {EditorBrowsableAttributeNever}
                 {(!data.Symbol.IsSealed ? "protected" : "private")} bool __isInitializingView;

                 [global::System.NonSerialized]
                 {GeneratedCodeViewAttribute}
                 {EditorBrowsableAttributeNever}
                 {(!data.Symbol.IsSealed ? "protected" : "private")} bool __isDeinitializing;

                 """)
            .AppendMultilineIf(data.IsInstantiateBinders,
                $"""
                [global::System.NonSerialized]
                {GeneratedCodeViewAttribute}
                {EditorBrowsableAttributeNever}
                private bool __isBindersCached;

                """)
            .AppendMultiline(
                $$"""
                {{GeneratedCodeViewAttribute}}
                public {{IViewModel}} ViewModel { get; protected set; }

                /// <inheritdoc/>
                /// <exception cref="global::System.ArgumentNullException"><paramref name="viewModel"/> is <see langword="null"/>.</exception>
                /// <exception cref="global::System.InvalidOperationException">The View is already initialized.</exception>
                {{GeneratedCodeViewAttribute}}
                public void Initialize({{IViewModel}} viewModel)
                {
                    if (viewModel is null)
                        throw new {{ArgumentNullException}}(nameof(viewModel));
                    if (ViewModel is not null)
                        throw new {{InvalidOperationException}}("View is already initialized.");

                    ViewModel = viewModel;
                    __isInitializingView = true;
                    try
                    {
                        InitializeInternal(viewModel);
                    }
                    catch
                    {
                        __isDeinitializing = true;
                        try
                        {
                            DeinitializeInternal();
                        }
                        catch
                        {
                            // Do not replace the initialization exception with a cleanup failure.
                        }
                        finally
                        {
                            ViewModel = null;
                            __isDeinitializing = false;
                        }
                        throw;
                    }
                    finally
                    {
                        __isInitializingView = false;
                    }
                }

                {{GeneratedCodeViewAttribute}}
                {{modifiers}} void InitializeInternal({{IViewModel}} viewModel)
                """)
            .AppendInitializeBody(data)
            .AppendInitializeInternalEvents()
            .AppendLine()
            .AppendMultiline(
                $$"""
                /// <inheritdoc/>
                /// <remarks>Repeated calls during cleanup do nothing; multiple cleanup failures are reported together.</remarks>
                /// <exception cref="global::System.InvalidOperationException">Initialization is in progress.</exception>
                /// <exception cref="global::System.AggregateException">Multiple cleanup operations fail.</exception>
                {{GeneratedCodeViewAttribute}}
                public void Deinitialize()
                {
                    if (__isDeinitializing || ViewModel is null) return;
                    if (__isInitializingView)
                        throw new {{InvalidOperationException}}("View cannot be deinitialized while initialization is in progress.");

                    __isDeinitializing = true;
                    try
                    {
                        DeinitializeInternal();
                    }
                    finally
                    {
                        ViewModel = null;
                        __isDeinitializing = false;
                    }
                }

                {{GeneratedCodeViewAttribute}}
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
                 {GeneratedCodeViewAttribute}
                 {EditorBrowsableAttributeNever}
                 private bool __isInitializing;

                 """)
            .AppendMultilineIf(data.IsInstantiateBinders,
                $"""
                [global::System.NonSerialized]
                {GeneratedCodeViewAttribute}
                {EditorBrowsableAttributeNever}
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
             #if {Defines.ASPID_MVVM_PROFILER}
             {GeneratedCodeViewAttribute}
             {EditorBrowsableAttributeNever}
             private static readonly {ProfilerMarker} __initializeMarker = new("{className}.Initialize");

             {GeneratedCodeViewAttribute}
             {EditorBrowsableAttributeNever}
             private static readonly {ProfilerMarker} __deinitializeMarker = new("{className}.Deinitialize");
             #endif
             """);
    }

    private static CodeWriter AppendInitializeInternalDeclaration(this CodeWriter code, in ViewDataSpan data)
    {
        var modifiers = "private";
        if (data.Inheritor is not Inheritor.None)
            modifiers = "protected override";
        else if (!data.Symbol.IsSealed)
            modifiers = "protected virtual";

        code.AppendMultiline(
            $"""
             {GeneratedCodeViewAttribute}
             {modifiers} void InitializeInternal({IViewModel} viewModel)
             """);

        return code;
    }

    private static CodeWriter AppendDeinitializeInternalDeclaration(this CodeWriter code, in ViewDataSpan data)
    {
        var modifiers = "private";
        if (data.Inheritor is not Inheritor.None)
            modifiers = "protected override";
        else if (!data.Symbol.IsSealed)
            modifiers = "protected virtual";

        code.AppendMultiline(
            $"""
             {GeneratedCodeViewAttribute}
             {modifiers} void DeinitializeInternal()
             """);

        return code;
    }

    private static CodeWriter AppendInitializeBody(this CodeWriter code, in ViewDataSpan data, bool isOverride = false)
    {
        code.BeginBlock()
            .AppendMultiline(
                $"""
                #if {Defines.ASPID_MVVM_PROFILER}
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
            .AppendLine("try")
            .BeginBlock()
            .AppendLine("OnInitializingInternal(viewModel);")
            .AppendLineIf(isOverride, "base.InitializeInternal(viewModel);")
            .AppendLineIf(data.IsInstantiateBinders, "InstantiateBinders();")
            .AppendLine();

        foreach (var member in data.Members)
            code.AppendBindSafely(member);

        var virtualFields = VirtualBinderFields.Collect(data);
        foreach (var info in virtualFields.Values)
            code.AppendLine(
                $"AspidBinderExtensions.BindSafely({info.FieldName}, " +
                $"viewModel.FindBindableMember(new({info.Id.Value})), this, {info.Id.Value});");

        return code.AppendLine()
            .AppendLine("OnInitializedInternal(viewModel);")
            .EndBlock()
            .AppendLine("finally")
            .BeginBlock()
            .AppendLine("__isInitializing = false;")
            .EndBlock()
            .EndBlock()
            .EndBlock();
    }

    private static CodeWriter AppendDeinitializeBody(this CodeWriter code, in ViewDataSpan data, bool isOverride = false)
    {
        code.BeginBlock()
            .AppendMultiline(
                $"""
                 #if {Defines.ASPID_MVVM_PROFILER}
                 using (__deinitializeMarker.Auto())
                 #endif
                 """)
            .BeginBlock();

        code.AppendLine("global::System.Exception __failure = null;")
            .AppendLine("global::System.Collections.Generic.List<global::System.Exception> __failures = null;");
        AppendCleanup("OnDeinitializingInternal();");
        if (isOverride)
            AppendCleanup("base.DeinitializeInternal();");
        code.AppendLine("var __context = new global::Aspid.MVVM.UnbindContext(this);");
        foreach (var member in data.Members)
        {
            var name = member is CachedBinderMember cached ? cached.CachedName : member.Name;
            AppendCleanup(member.Type is IArrayTypeSymbol
                ? $"AspidBinderExtensions.Unbind({name}, in __context);"
                : $"((global::Aspid.MVVM.IBinder){name})?.Unbind(in __context);");
        }
        foreach (var info in VirtualBinderFields.Collect(data).Values)
            AppendCleanup($"AspidBinderExtensions.Unbind({info.FieldName}, in __context);");
        AppendCleanup("OnDeinitializedInternal();");
        code.AppendLine("if (__failures is not null)")
            .AppendLine("    throw new global::System.AggregateException(\"Multiple errors occurred during cleanup.\", __failures);")
            .AppendLine("if (__failure is not null)")
            .AppendLine("    global::System.Runtime.ExceptionServices.ExceptionDispatchInfo.Capture(__failure).Throw();")
            .EndBlock()
            .EndBlock();

        void AppendCleanup(string statement)
        {
            code.AppendLine("try")
                .BeginBlock()
                .AppendLine(statement)
                .EndBlock()
                .AppendLine("catch (global::System.Exception __exception)")
                .BeginBlock()
                .AppendLine("if (__failure is null)")
                .AppendLine("    __failure = __exception;")
                .AppendLine("else")
                .AppendLine("    (__failures ??= new global::System.Collections.Generic.List<global::System.Exception> { __failure }).Add(__exception);")
                .EndBlock();
        }

        return code;
    }

    private static CodeWriter AppendInstantiateBindersMethods(this CodeWriter code, in ViewDataSpan data)
    {
        code.AppendMultiline(
                $"""
                {GeneratedCodeViewAttribute}
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

                {GeneratedCodeViewAttribute}
                partial void OnInstantiatingBinders();

                {GeneratedCodeViewAttribute}
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

             {GeneratedCodeViewAttribute}
             partial void OnInitializingInternal({IViewModel} viewModel);

             {GeneratedCodeViewAttribute}
             partial void OnInitializedInternal({IViewModel} viewModel);
             """);
    }

    private static CodeWriter AppendDeinitializeInternalEvents(this CodeWriter code)
    {
        return code.AppendMultiline(
            $"""

             {GeneratedCodeViewAttribute}
             partial void OnDeinitializingInternal();

             {GeneratedCodeViewAttribute}
             partial void OnDeinitializedInternal();
             """);
    }

    private static string[]? GetBaseTypes(in ViewDataSpan data) =>
        data.Inheritor is Inheritor.None ? [IView.ToString()] : null;
}
