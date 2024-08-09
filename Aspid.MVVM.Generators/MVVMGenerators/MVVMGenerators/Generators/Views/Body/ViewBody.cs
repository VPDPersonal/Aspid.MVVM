using System;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Generators.Views.Data;
using MVVMGenerators.Helpers.Extensions.Writer;
using MVVMGenerators.Helpers.Extensions.Symbols;

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

        return readOnlyData.Inheritor switch
        {
            Inheritor.None => code.AppendNone(in readOnlyData),
            Inheritor.InheritorViewAttribute => code.AppendInheritorView(in readOnlyData),
            Inheritor.InheritorMonoView => code.AppendInheritorMonoView(in readOnlyData),
            Inheritor.OverrideMonoView => code,
            Inheritor.HasInterface => code.AppendHasInterface(in readOnlyData),
            _ => throw new ArgumentOutOfRangeException()
        };
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
            """)
            .BeginBlock()
            .AppendInitialize(data)
            .EndBlock();
        
        return code;
    }

    private static CodeWriter AppendInheritorView(this CodeWriter code, in ReadOnlyViewData data)
    {
        /* lang=C# */
        code.AppendMultiline(
            $"""
            {GeneratedAttribute}
            protected override void InitializeIternal({IViewModel} viewModel)
            """)
            .BeginBlock()
            .AppendInitialize(data)
            .AppendLine("base.InitializeIternal(viewModel);")
            .EndBlock();

        return code;
    }

    public static CodeWriter AppendInheritorMonoView(this CodeWriter code, in ReadOnlyViewData data)
    {
        /* lang=C# */
        code.AppendMultiline(
             $"""
             {GeneratedAttribute}
             protected override void InitializeIternal({IViewModel} viewModel)
             """)
            .BeginBlock()
            .AppendInitialize(data)
            .EndBlock();

        return code;
    }
    
    public static CodeWriter AppendHasInterface(this CodeWriter code, in ReadOnlyViewData data)
    {
        /* lang=C# */
        code.AppendMultiline(
             $"""
             {GeneratedAttribute}
             protected virtual void InitializeIternal({IViewModel} viewModel)
             """)
            .BeginBlock()
            .AppendInitialize(data)
            .EndBlock();

        return code;
    }
    
    private static CodeWriter AppendInitialize(this CodeWriter code, in ReadOnlyViewData data)
    {
        var AsBinderFieldsCount = data.AsBinderFields.Length;
        var AsBinderPropertyCount = data.AsBinderProperty.Length;
        
        if (AsBinderFieldsCount > 0)
        {
            code.AppendLoop(data.AsBinderFields, field =>
            {
                var name = field.Member.Name;
                code.AppendLine($"{name}Binder ??= new {field.BinderType}({name});");
            });

            if (AsBinderPropertyCount == 0)
                code.AppendLine();
        }
        
        if (AsBinderPropertyCount > 0)
        {
            code.AppendLoop(data.AsBinderProperty, property =>
            {
                var name = property.Member;
                code.AppendLine($"{name.GetFieldName()}Binder ??= new {property.BinderType}({name.Name});");
            });
            
            code.AppendLine();
        }

        var isAppendLine = false;
        
        code.AppendLoop(data.ViewFields, AppendField)
            .AppendLoop(data.BinderFields, AppendField)
            .AppendLoop(data.AsBinderFields, AsBinderField)
            .AppendLoop(data.ViewProperties, AppendProperty)
            .AppendLoop(data.BinderProperty, AppendProperty)
            .AppendLoop(data.AsBinderProperty, AsBinderProperty);

        return code;

        void AppendField(int index, IFieldSymbol field) =>
            Append(field.Name, field.GetPropertyName(), field.Type);
        
        void AppendProperty(int index, IPropertySymbol property) =>
            Append(property.Name, FieldSymbolExtensions.GetPropertyNameFromFieldName(property.Name), property.Type);

        void AsBinderField(AsBinderMember<IFieldSymbol> asBinderField)
        {
            var member = asBinderField.Member;
            
            var name = member.Name + "Binder";
            var propertyName = member.GetPropertyName();
            
            Append(name, propertyName, member.Type);
        }
        
        void AsBinderProperty(AsBinderMember<IPropertySymbol> asBinderProperty)
        {
            var property = asBinderProperty.Member;

            var fieldName = property.GetFieldName();
            var name = fieldName + "Binder";
            
            Append(name, FieldSymbolExtensions.GetPropertyNameFromFieldName(property.Name), property.Type);
        }
        
        void Append(string name, string idName, ITypeSymbol type)
        {
            if (type is IArrayTypeSymbol)
            {
                /* lang=C# */
                code.AppendMultiline(
                    $"""
                     for (var i = 0; i < {name}.Length; i++)
                         {name}[i].Bind(viewModel, {idName});
                         
                     """);
            }
            else
            {
                code.AppendLine($"{name}.Bind(viewModel, {idName});");
            }
        }
    }
}