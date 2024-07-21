using System;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using MVVMGenerators.Descriptions;
using MVVMGenerators.Extensions.Symbols;
using MVVMGenerators.Helpers.Extensions;

namespace MVVMGenerators.Generators.Views;

// ReSharper disable once InconsistentNaming
public static class IViewBody
{
    public static CodeWriter AppendIView(this CodeWriter code, ViewData viewData)
    {
        switch (viewData.Inheritor)
        {
            case ViewInheritor.None: AppendNone(code, viewData); break;
            case ViewInheritor.InheritorViewAttribute: break;
            case ViewInheritor.InheritorMonoView: break;
            case ViewInheritor.OverrideMonoView: break;
            case ViewInheritor.HasInterface: break;
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
    
    
//      public static CodeWriter AppendIViewBody(this CodeWriter code, ViewData viewData)
//      {
//          switch (viewData.Inheritor)
//          {
//              case ViewInheritor.None:
//                  code.AppendExplicitGetBinders();
//                  code.AppendVirtualGetBindersIternal(viewData);
//                  break;
//              
//              case ViewInheritor.InheritorMonoView:
//                  code.AppendOverrideGetBinders();
//                  code.AppendVirtualGetBindersIternal(viewData);
//                  break;
//              
//              case ViewInheritor.InheritorViewAttribute:
//                  code.AppendOverrideGetBindersIternal(viewData);
//                  break;
//              
//              case ViewInheritor.HasInterface:
//              case ViewInheritor.OverrideMonoView:
//                  code.AppendVirtualGetBindersIternal(viewData);
//                  break;
//              
//              default: throw new ArgumentOutOfRangeException();
//          }
//          
//          code.AppendLine()
//              .AppendAddBindersMethod();
//
//          return code;
//      }
//
//      private static void AppendExplicitGetBinders(this CodeWriter code)
//      {
//          code.AppendMultiline(
//              $"""
//               {General.GeneratedCodeViewAttribute}
//               {Classes.IReadOnlyBindersCollectionById.Global} {Classes.IView.Global}.GetBinders() =>
//                   GetBindersIternal();
//
//               """);
//      }
//      
//      private static void AppendOverrideGetBinders(this CodeWriter code)
//      {
//          code.AppendMultiline(
//              $"""
//               {General.GeneratedCodeViewAttribute}
//               protected sealed override {Classes.IReadOnlyBindersCollectionById.Global} GetBinders() =>
//                   GetBindersIternal();
//
//               """);
//      }
//      
//      private static void AppendOverrideGetBindersIternal(this CodeWriter code, ViewData viewData)
//      {
//          code.AppendMultiline(
//                  $$"""
//                    {{General.GeneratedCodeViewAttribute}}
//                    protected override {{Classes.BindersCollectionById.Global}} GetBindersIternal()
//                    {
//                        var binders = base.GetBindersIternal(); 
//                        
//                    """)
//              .IncreaseIndent();
//          
//          foreach (var field in viewData.Fields)
//          {
//              code.AppendLine(field.Type.Kind != SymbolKind.ArrayType ?
//                  $"binders.Add(\"{field.GetPropertyName()}\", new[] {{ {field.Name} }});" :
//                  $"binders.Add(\"{field.GetPropertyName()}\", {field.Name});");
//          }
//              
//          code.AppendLine()
//              .AppendMultiline(
//                  """
//                  AddBinders(ref binders);
//                  return binders;
//                  """)
//              .EndBlock();
//      }
//
//      private static void AppendVirtualGetBindersIternal(this CodeWriter code, ViewData viewData)
//      {
//          code.AppendMultiline(
//                  $$"""
//                    {{General.GeneratedCodeViewAttribute}}
//                    protected virtual {{Classes.BindersCollectionById.Global}} GetBindersIternal()
//                    {
//                        var binders = new {{Classes.BindersCollectionById.Global}}
//                        {
//                    """)
//              .IncreaseIndent()
//              .IncreaseIndent();
//          
//          foreach (var field in viewData.Fields)
//          {
//              code.AppendLine(field.Type.Kind != SymbolKind.ArrayType ?
//                  $"{{ \"{field.GetPropertyName()}\", new[] {{ {field.Name} }} }}," :
//                  $"{{ \"{field.GetPropertyName()}\", {field.Name} }},");
//          }
//              
//          code.DecreaseIndent()
//              .AppendLine("};")
//              .AppendLine();
//
//          code.AppendMultiline(
//              """
//              AddBinders(ref binders);
//              return binders;
//              """)
//              .EndBlock();
//      }
//
//      private static void AppendAddBindersMethod(this CodeWriter code)
//      {
//          code.AppendMultiline(
//              $"""
//              {General.GeneratedCodeViewAttribute}
//              partial void AddBinders(ref {Classes.BindersCollectionById.Global} binders);
//              """);
//      }
}