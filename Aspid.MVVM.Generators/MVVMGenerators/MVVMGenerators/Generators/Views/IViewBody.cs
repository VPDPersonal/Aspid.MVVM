// using System;
// using Microsoft.CodeAnalysis;
// using MVVMGenerators.Helpers;
// using MVVMGenerators.Descriptions;
// using MVVMGenerators.Extensions.Symbols;
//
// namespace MVVMGenerators.Generators.Views;
//
// // ReSharper disable once InconsistentNaming
// public static class IViewBody
// {
//     public static CodeWriter AppendIViewBody(this CodeWriter code, ViewData viewData)
//     {
//         switch (viewData.Inheritor)
//         {
//             case ViewInheritor.None:
//                 code.AppendExplicitGetBinders();
//                 code.AppendVirtualGetBindersIternal(viewData);
//                 break;
//             
//             case ViewInheritor.InheritorMonoView:
//                 code.AppendOverrideGetBinders();
//                 code.AppendVirtualGetBindersIternal(viewData);
//                 break;
//             
//             case ViewInheritor.InheritorViewAttribute:
//                 code.AppendOverrideGetBindersIternal(viewData);
//                 break;
//             
//             case ViewInheritor.HasInterface:
//             case ViewInheritor.OverrideMonoView:
//                 code.AppendVirtualGetBindersIternal(viewData);
//                 break;
//             
//             default: throw new ArgumentOutOfRangeException();
//         }
//         
//         code.AppendLine()
//             .AppendAddBindersMethod();
//
//         return code;
//     }
//
//     private static void AppendExplicitGetBinders(this CodeWriter code)
//     {
//         code.AppendMultiline(
//             $"""
//              {General.GeneratedCodeViewAttribute}
//              {Classes.IReadOnlyBindersCollectionById.Global} {Classes.IView.Global}.GetBinders() =>
//                  GetBindersIternal();
//
//              """);
//     }
//     
//     private static void AppendOverrideGetBinders(this CodeWriter code)
//     {
//         code.AppendMultiline(
//             $"""
//              {General.GeneratedCodeViewAttribute}
//              protected sealed override {Classes.IReadOnlyBindersCollectionById.Global} GetBinders() =>
//                  GetBindersIternal();
//
//              """);
//     }
//     
//     private static void AppendOverrideGetBindersIternal(this CodeWriter code, ViewData viewData)
//     {
//         code.AppendMultiline(
//                 $$"""
//                   {{General.GeneratedCodeViewAttribute}}
//                   protected override {{Classes.BindersCollectionById.Global}} GetBindersIternal()
//                   {
//                       var binders = base.GetBindersIternal(); 
//                       
//                   """)
//             .IncreaseIndent();
//         
//         foreach (var field in viewData.Fields)
//         {
//             code.AppendLine(field.Type.Kind != SymbolKind.ArrayType ?
//                 $"binders.Add(\"{field.GetPropertyName()}\", new[] {{ {field.Name} }});" :
//                 $"binders.Add(\"{field.GetPropertyName()}\", {field.Name});");
//         }
//             
//         code.AppendLine()
//             .AppendMultiline(
//                 """
//                 AddBinders(ref binders);
//                 return binders;
//                 """)
//             .EndBlock();
//     }
//
//     private static void AppendVirtualGetBindersIternal(this CodeWriter code, ViewData viewData)
//     {
//         code.AppendMultiline(
//                 $$"""
//                   {{General.GeneratedCodeViewAttribute}}
//                   protected virtual {{Classes.BindersCollectionById.Global}} GetBindersIternal()
//                   {
//                       var binders = new {{Classes.BindersCollectionById.Global}}
//                       {
//                   """)
//             .IncreaseIndent()
//             .IncreaseIndent();
//         
//         foreach (var field in viewData.Fields)
//         {
//             code.AppendLine(field.Type.Kind != SymbolKind.ArrayType ?
//                 $"{{ \"{field.GetPropertyName()}\", new[] {{ {field.Name} }} }}," :
//                 $"{{ \"{field.GetPropertyName()}\", {field.Name} }},");
//         }
//             
//         code.DecreaseIndent()
//             .AppendLine("};")
//             .AppendLine();
//
//         code.AppendMultiline(
//             """
//             AddBinders(ref binders);
//             return binders;
//             """)
//             .EndBlock();
//     }
//
//     private static void AppendAddBindersMethod(this CodeWriter code)
//     {
//         code.AppendMultiline(
//             $"""
//             {General.GeneratedCodeViewAttribute}
//             partial void AddBinders(ref {Classes.BindersCollectionById.Global} binders);
//             """);
//     }
// }