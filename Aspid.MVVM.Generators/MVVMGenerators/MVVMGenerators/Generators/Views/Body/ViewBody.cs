using System;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Generators.Views.Data;
using MVVMGenerators.Generators.Views.Data.Members;
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

    public static CodeWriter AppendIView(this CodeWriter code, in ViewData data)
    {
        var readOnlyData = new ReadOnlyViewData(data);

        code = readOnlyData.Inheritor switch
        {
            Inheritor.None => code.AppendNone(in readOnlyData),
            Inheritor.InheritorViewAttribute => code.AppendInheritorView(in readOnlyData),
            Inheritor.InheritorMonoView => code.AppendInheritorMonoView(in readOnlyData),
            Inheritor.OverrideMonoView => code,
            Inheritor.HasInterface => code.AppendHasInterface(in readOnlyData),
            _ => throw new ArgumentOutOfRangeException()
        };
        
        var isInstantiateBinders = readOnlyData.PropertyMembers.Length + readOnlyData.AsBinderMembers.Length > 0;
        if (!isInstantiateBinders) return code;
        
        code.AppendLine()
            .AppendInstantiateBindersMethods(readOnlyData);

        return code;
    }

    private static CodeWriter AppendNone(this CodeWriter code, in ReadOnlyViewData data)
    {
        var className = data.Declaration.Identifier.Text;

        /* lang=C# */
        code.AppendMultiline(
            $$"""
              #if !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
              {{GeneratedAttribute}}
              private static readonly {{ProfilerMarker}} _initializeMarker = new("{{className}}.Initialize");
              #endif

              {{GeneratedAttribute}}
              void {{IView}}.Initialize({{IViewModel}} viewModel)
              {
                  #if !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
                  using (_initializeMarker.Auto())
                  #endif
                  {
                      InitializeIternal(viewModel);
                  }
              }

              {{GeneratedAttribute}}
              protected virtual void InitializeIternal({{IViewModel}} viewModel)
              """).BeginBlock().AppendInitialize(data).EndBlock();

        return code;
    }

    private static CodeWriter AppendInheritorView(this CodeWriter code, in ReadOnlyViewData data)
    {
        /* lang=C# */
        code.AppendMultiline(
            $"""
             {GeneratedAttribute}
             protected override void InitializeIternal({IViewModel} viewModel)
             """).BeginBlock().AppendInitialize(data).AppendLine("base.InitializeIternal(viewModel);").EndBlock();

        return code;
    }

    public static CodeWriter AppendInheritorMonoView(this CodeWriter code, in ReadOnlyViewData data)
    {
        /* lang=C# */
        code.AppendMultiline(
            $"""
             {GeneratedAttribute}
             protected override void InitializeIternal({IViewModel} viewModel)
             """).BeginBlock().AppendInitialize(data).EndBlock();

        return code;
    }

    public static CodeWriter AppendHasInterface(this CodeWriter code, in ReadOnlyViewData data)
    {
        /* lang=C# */
        code.AppendMultiline(
            $"""
             {GeneratedAttribute}
             protected virtual void InitializeIternal({IViewModel} viewModel)
             """).BeginBlock().AppendInitialize(data).EndBlock();

        return code;
    }

    private static CodeWriter AppendInitialize(this CodeWriter code, in ReadOnlyViewData data)
    {
        var isInstantiateBinders = data.PropertyMembers.Length + data.AsBinderMembers.Length > 0;
        
        code.AppendLineIf(isInstantiateBinders, "InstantiateBinders();\n")
            .AppendLoop(data.FieldMembers, AppendFieldMember)
            .AppendLoop(data.PropertyMembers, AppendPropertyMember)
            .AppendLoop(data.AsBinderMembers, AppendAsBinderMember);

        return code;

        void AppendFieldMember(FieldMember member) =>
            Append(member.Name, member.Id);

        void AppendPropertyMember(PropertyMember member) =>
            Append(member.FieldName, member.Id);

        void AppendAsBinderMember(AsBinderMember member) =>
            Append(member.BinderName, member.Id);

        void Append(string name, string idName)
        {
            code.AppendLine($"BindSafely({name}, viewModel, {idName});");
        }
    }
    private static CodeWriter AppendInstantiateBindersMethods(this CodeWriter code, ReadOnlyViewData data)
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
    
    private static CodeWriter AppendCreateBinders(this CodeWriter code, ReadOnlyViewData data)
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

                    /*lang=C#*/
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