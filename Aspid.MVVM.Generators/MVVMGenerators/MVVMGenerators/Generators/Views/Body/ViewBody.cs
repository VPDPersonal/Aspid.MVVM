using System;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Generators.Views.Data;
using MVVMGenerators.Helpers.Extensions.Writer;
using MVVMGenerators.Generators.Views.Data.Members;

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
            Inheritor.InheritorViewAttribute => code.AppendInheritorView(data),
            Inheritor.InheritorMonoView => code.AppendInheritorMonoView(data),
            Inheritor.HasInterface => code.AppendHasInterface(data),
            _ => throw new ArgumentOutOfRangeException()
        };
        
        var isInstantiateBinders = data.PropertyMembers.Length + data.AsBinderMembers.Length > 0;
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
                #if !{{Defines.ASPID_UI_MVVM_UNITY_PROFILER_DISABLED}}
                {{GeneratedAttribute}}
                private static readonly {{ProfilerMarker}} _initializeMarker = new("{{className}}.Initialize");
                
                {{GeneratedAttribute}}
                private static readonly {{ProfilerMarker}} _deinitializeMarker = mew("{{className}}.Deinitialize");
                #endif
                
                {{GeneratedAttribute}}
                public IViewModel? ViewModel { get; private set; }
                
                {{GeneratedAttribute}}
                void {{IView}}.Initialize({{IViewModel}} viewModel)
                {
                    #if !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
                    using (_initializeMarker.Auto())
                    #endif
                    {
                        ViewModel = viewModel;
                        InitializeIternal(viewModel);
                    }
                }
                
                {{GeneratedAttribute}}
                protected virtual void InitializeIternal({{IViewModel}} viewModel)
                """)
            .BeginBlock()
            .AppendInitializeBody(data)
            .EndBlock()
            .AppendLine()
            .AppendMultiline(
                $$"""
                
                {{GeneratedAttribute}}
                void {{IView}}.Deinitialize()
                {
                    #if !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
                    using (_deinitializeMarker.Auto())
                    #endif
                    {
                        DeinitializeIternal();
                    }
                }

                {{GeneratedAttribute}}
                protected virtual void DeinitializeIternal()
                """)
            .BeginBlock()
            .AppendDeinitializeBody(data)
            .EndBlock();

        return code;
    }

    private static CodeWriter AppendInheritorView(this CodeWriter code, in ViewDataSpan data)
    {
        const bool isOverride = true;
        
        if (!data.IsInitializeOverride)
        {
            code.AppendInitializeIternalDeclaration(isOverride)
                .BeginBlock()
                .AppendInitializeBody(data)
                .AppendLine("base.InitializeIternal(viewModel);")
                .EndBlock();
        }
        
        if (!data.IsDeinitializeOverride)
        {
            code.AppendLineIf(!data.IsInitializeOverride)
                .AppendDeinitializeIternalDeclaration(isOverride)
                .BeginBlock()
                .AppendDeinitializeBody(data)
                .AppendLine("base.DeinitializeIternal();")
                .EndBlock();
        }

        return code;
    }

    public static CodeWriter AppendInheritorMonoView(this CodeWriter code, in ViewDataSpan data)
    {
        const bool isOverride = true;
        
        if (!data.IsInitializeOverride)
        {
            code.AppendInitializeIternalDeclaration(isOverride)
                .BeginBlock()
                .AppendInitializeBody(data)
                .EndBlock();
        }
        
        if (!data.IsDeinitializeOverride)
        {
            code.AppendLineIf(!data.IsInitializeOverride)
                .AppendDeinitializeIternalDeclaration(isOverride)
                .BeginBlock()
                .AppendDeinitializeBody(data)
                .EndBlock();
        }
        
        return code;
    }

    public static CodeWriter AppendHasInterface(this CodeWriter code, in ViewDataSpan data)
    {
        const bool isOverride = false;
        
        if (!data.IsInitializeOverride)
        {
            code.AppendInitializeIternalDeclaration(isOverride)
                .BeginBlock()
                .AppendInitializeBody(data)
                .EndBlock();
        }
        
        if (!data.IsDeinitializeOverride)
        {
            code.AppendLineIf(!data.IsInitializeOverride)
                .AppendDeinitializeIternalDeclaration(isOverride)
                .BeginBlock()
                .AppendDeinitializeBody(data)
                .EndBlock();
        }
        
        return code;
    }

    private static CodeWriter AppendDeinitializeIternalDeclaration(this CodeWriter code, bool isOverride)
    {
        var modificator = isOverride ? "override" : "virtual";
        
        code.AppendMultiline(
            $"""
             {GeneratedAttribute}
             protected {modificator} void DeinitializeIternal()
             """);

        return code; 
    }

    private static CodeWriter AppendInitializeIternalDeclaration(this CodeWriter code, bool isOverride)
    {
        var modificator = isOverride ? "override" : "virtual";
        
        code.AppendMultiline(
            $"""
            {GeneratedAttribute}
            protected {modificator} void InitializeIternal({IViewModel} viewModel)
            """);

        return code;
    }

    private static CodeWriter AppendInitializeBody(this CodeWriter code, in ViewDataSpan data)
    {
        var isInstantiateBinders = data.PropertyMembers.Length + data.AsBinderMembers.Length > 0;
        
        code.AppendLineIf(isInstantiateBinders, "InstantiateBinders();\n")
            .AppendMethodBody("BindSafely", data);

        return code;
    }
    
    private static CodeWriter AppendDeinitializeBody(this CodeWriter code, in ViewDataSpan data)
    {
        code.AppendMethodBody("UnbindSafely", data);
        return code;
    }

    private static CodeWriter AppendMethodBody(this CodeWriter code, string bindMethodName, in ViewDataSpan data)
    {
        code.AppendLoop(data.FieldMembers, AppendFieldMember)
            .AppendLoop(data.PropertyMembers, AppendPropertyMember)
            .AppendLoop(data.AsBinderMembers, AppendAsBinderMember);

        return code;

        void AppendFieldMember(FieldMember member) =>
            Append(member.Name, member.Id);

        void AppendPropertyMember(PropertyMember member) =>
            Append(member.FieldName, member.Id);

        void AppendAsBinderMember(AsBinderMember member) =>
            Append(member.BinderName, member.Id);

        void Append(string name, string idName) =>
            code.AppendLine($"{name}.{bindMethodName}(ViewModel, {idName});");
    }

    private static CodeWriter AppendInstantiateBindersMethods(this CodeWriter code, in ViewDataSpan data)
    {
        code.AppendMultiline(
                $"""
                {GeneratedAttribute}
                private void InstantiateBinders()
                """)
            .BeginBlock()
            .AppendCreateBinders(data)
            .EndBlock();

        return code;
    }

    private static CodeWriter AppendCreateBinders(this CodeWriter code, in ViewDataSpan data)
    {
        if (data.PropertyMembers.Length > 0)
        {
            code.AppendLoop(data.PropertyMembers, member =>
            {
                code.AppendLine(member.IsUnityEngineObject 
                    ? $"if (!{member.FieldName}) {member.FieldName} = {member.Name};" 
                    : $"{member.FieldName} ??= {member.Name};");
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
                    var localName = $"local{name}";
                    var binderName = member.BinderName;
                    var binderType = member.AsBinderType;
                    
                    code.AppendLineIf(isAppend)
                        .AppendMultiline(
                        $$"""
                        if ({{binderName}} == null)
                        {
                            var {{localName}} = {{name}};
                            {{binderName}} = new {{binderType}}[{{localName}}.Length];
                            
                            for (var i = 0; i < {{localName}}.Length; i++)
                                {{binderName}}[i] = new {{member.AsBinderType}}({{localName}}[i]);
                        }
                        """)
                        .AppendLineIf(i + 1 < membersCount);

                    isAppend = false;
                }
                else
                {
                    isAppend = true;
                    code.AppendLine(member.IsUnityEngineObject 
                        ? $"if ({member.Name}) {member.BinderName} = new {member.AsBinderType}({member.Name});" 
                        : $"{member.BinderName} ??= new {member.AsBinderType}({member.Name});");
                }
            });
        }

        return code;
    }
}