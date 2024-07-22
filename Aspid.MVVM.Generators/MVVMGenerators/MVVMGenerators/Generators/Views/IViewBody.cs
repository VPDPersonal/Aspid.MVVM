using System;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Helpers.Extensions.Writer;
using MVVMGenerators.Helpers.Extensions.Symbols;

namespace MVVMGenerators.Generators.Views;

// ReSharper disable once InconsistentNaming
public static class IViewBody
{
    public static CodeWriter AppendIView(this CodeWriter code, ViewData viewData)
    {
        switch (viewData.Inheritor)
        {
            case ViewInheritor.None: return AppendNone(code, viewData);
            case ViewInheritor.InheritorViewAttribute: return AppendInheritorView(code, viewData);
            case ViewInheritor.InheritorMonoView: return AppendInheritorMonoView(code, viewData);
            case ViewInheritor.OverrideMonoView: break;
            case ViewInheritor.HasInterface: return AppendHasInterface(code, viewData);
            default: throw new ArgumentOutOfRangeException();
        }

        return code;
    }

    public static CodeWriter AppendNone(CodeWriter code, ViewData viewData)
    {
        return code.AppendMultiline(
            $$"""
            #if !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
            {{General.GeneratedCodeViewAttribute}}
            private static readonly {{Classes.ProfilerMarker.Global}} _initializeMarker = new("{{viewData.Declaration.Identifier.Text}}.Initialize");
            #endif
            
            {{General.GeneratedCodeViewAttribute}}
            void {{Classes.IView.Global}}.Initialize({{Classes.IViewModel.Global}} viewModel)
            {
                #if !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
                using (_initializeMarker.Auto())
                #endif
                {
                    InitializeIternal(viewModel);
                }
            }
            
            {{General.GeneratedCodeViewAttribute}}
            protected virtual void InitializeIternal({{Classes.IViewModel.Global}} viewModel)
            """)
            .BeginBlock()
            .AppendLoop(viewData.Fields, field =>
            {
                var fieldName = field.Name;
                var idName = $"{field.GetPropertyName()}Id";

                if (field.Type is IArrayTypeSymbol)
                {
                    code.AppendMultiline(
                        $"""
                         for (var i = 0; i < {fieldName}.Length; i++)
                             {fieldName}[i].Bind(viewModel, {idName});
                             
                         """);
                }
                else
                {
                    code.AppendLine($"{fieldName}.Bind(viewModel, {idName});")
                        .AppendLine();
                }
            })
            .EndBlock();
    }

    public static CodeWriter AppendInheritorView(CodeWriter code, ViewData viewData)
    {
        return code.AppendMultiline(
                $"""
                  {General.GeneratedCodeViewAttribute}
                  protected override void InitializeIternal({Classes.IViewModel.Global} viewModel)
                  """)
            .BeginBlock()
            .AppendLoop(viewData.Fields, field =>
            {
                var fieldName = field.Name;
                var idName = $"{field.GetPropertyName()}Id";

                if (field.Type is IArrayTypeSymbol)
                {
                    code.AppendMultiline(
                        $"""
                         for (var i = 0; i < {fieldName}.Length; i++)
                             {fieldName}[i].Bind(viewModel, {idName});
                             
                         """);
                }
                else
                {
                    code.AppendLine($"{fieldName}.Bind(viewModel, {idName});")
                        .AppendLine();
                }
            })
            .AppendLine("base.InitializeIternal(viewModel);")
            .EndBlock();
    }
    
    public static CodeWriter AppendInheritorMonoView(CodeWriter code, ViewData viewData)
    {
        return code.AppendMultiline(
                $"""
                  {General.GeneratedCodeViewAttribute}
                  protected override void InitializeIternal({Classes.IViewModel.Global} viewModel)
                  """)
            .BeginBlock()
            .AppendLoop(viewData.Fields, field =>
            {
                var fieldName = field.Name;
                var idName = $"{field.GetPropertyName()}Id";

                if (field.Type is IArrayTypeSymbol)
                {
                    code.AppendMultiline(
                        $"""
                         for (var i = 0; i < {fieldName}.Length; i++)
                             {fieldName}[i].Bind(viewModel, {idName});
                             
                         """);
                }
                else
                {
                    code.AppendLine($"{fieldName}.Bind(viewModel, {idName});")
                        .AppendLine();
                }
            })
            .EndBlock();
    }
    
    public static CodeWriter AppendHasInterface(CodeWriter code, ViewData viewData)
    {
        return code.AppendMultiline(
                $"""
                  {General.GeneratedCodeViewAttribute}
                  protected virtual void InitializeIternal({Classes.IViewModel.Global} viewModel)
                  """)
            .BeginBlock()
            .AppendLoop(viewData.Fields, field =>
            {
                var fieldName = field.Name;
                var idName = $"{field.GetPropertyName()}Id";

                if (field.Type is IArrayTypeSymbol)
                {
                    code.AppendMultiline(
                        $"""
                         for (var i = 0; i < {fieldName}.Length; i++)
                             {fieldName}[i].Bind(viewModel, {idName});
                             
                         """);
                }
                else
                {
                    code.AppendLine($"{fieldName}.Bind(viewModel, {idName});")
                        .AppendLine();
                }
            })
            .EndBlock();
    }
}