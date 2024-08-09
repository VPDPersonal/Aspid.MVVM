using System.Linq;
using System.Threading;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;
using System.Runtime.CompilerServices;
using MVVMGenerators.Helpers.Descriptions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MVVMGenerators.Helpers.Extensions.Writer;
using MVVMGenerators.Helpers.Extensions.Symbols;
using MVVMGenerators.Helpers.Extensions.Declarations;

namespace MVVMGenerators.Generators.ViewModels;

[Generator(LanguageNames.CSharp)]
public class ViewModelGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var provider = context.SyntaxProvider.CreateSyntaxProvider(SyntacticPredicate, FindViewModels)
            .Where(foundForSourceGenerator => foundForSourceGenerator.IsNeed)
            .Select((foundForSourceGenerator, _) => foundForSourceGenerator.Container);
        
        context.RegisterSourceOutput(
            source: provider,
            action: GenerateCode);
    }
    
    private static bool SyntacticPredicate(SyntaxNode node, CancellationToken cancellationToken)
    {
        var candidate = node switch
        {
            ClassDeclarationSyntax or StructDeclarationSyntax => node as TypeDeclarationSyntax,
            _ => null
        };
    
        return candidate is not null
            && candidate.AttributeLists.Count > 0
            && candidate.Modifiers.Any(SyntaxKind.PartialKeyword)
            && !candidate.Modifiers.Any(SyntaxKind.StaticKeyword);
    }
    
    private static FoundForGenerator<ViewModelData> FindViewModels(GeneratorSyntaxContext context, CancellationToken cancellationToken)
    {
        Debug.Assert(context.Node is TypeDeclarationSyntax);
        var candidate = Unsafe.As<TypeDeclarationSyntax>(context.Node);
        if (!candidate.HasAttribute(context.SemanticModel, Classes.ViewModelAttribute.FullName)) return default;

        var symbol = context.SemanticModel.GetDeclaredSymbol(candidate, cancellationToken);
        if (symbol is null) return default;
    
        var fields = new List<FieldData>();
        var methods = new List<IMethodSymbol>();
    
        foreach (var member in symbol.GetMembers())
        {
            switch (member)
            {
                case IFieldSymbol field:
                    if (field.HasAttribute(Classes.BindAttribute.FullName))
                    {
                        var getAccess = -1;
                        var setAccess = -1;

                        if (field.HasAttribute(Classes.AccessAttribute.FullName, out var accessAttribute))
                        {
                            if (accessAttribute!.ConstructorArguments.Length > 0)
                            {
                                var value = (int)(accessAttribute!.ConstructorArguments[0].Value ?? 0);
                                getAccess = value;
                                setAccess = value;
                            }
                            
                            foreach (var argument in accessAttribute!.NamedArguments)
                            {
                                switch (argument.Key)
                                {
                                    case "Get": getAccess = (int)(argument.Value.Value ?? -1); break;
                                    case "Set": setAccess = (int)(argument.Value.Value ?? -1); break;
                                }
                            }
                        }

                        fields.Add(new FieldData(field, getAccess, setAccess));
                    }
                    break;
                
                case IMethodSymbol method: 
                    if (method.HasAttribute("UltimateUI.MVVM.ViewModels.BindCommandAttribute")) 
                        methods.Add(method);
                    break;
            }
        }

        var hasBaseType = false; 
        var hasInterface = symbol.HasInterface(Classes.IViewModel.FullName);
        
        for (var type = symbol.BaseType; type != null; type = type.BaseType)
        {
            if (type.HasInterface(Classes.IViewModel.FullName))
                hasInterface = true;
            
            if (type.HasAttribute(Classes.ViewModelAttribute.FullName))
                hasBaseType = true;
            
            if (hasInterface && hasBaseType) break;
        }

        return new FoundForGenerator<ViewModelData>(true,
            new ViewModelData(hasBaseType, hasInterface, candidate, fields, methods));
    }
    
    private static void GenerateCode(SourceProductionContext context, ViewModelData viewModel)
    {
        var declaration = viewModel.Declaration;
        var namespaceName = declaration.GetNamespaceName();
        var declarationText = declaration.GetDeclarationText();

        if (viewModel.Fields.Count > 0)
        {
            GenerateProperties(context, namespaceName, declarationText, viewModel);
            IdBodyGenerator.GenerateViewModelId(context, declarationText, namespaceName,
                viewModel.Fields.Select(field => field.Field));
        }
        
        GenerateIViewModel(context, namespaceName, declarationText, viewModel);
    }

    private static void GenerateProperties(SourceProductionContext context, string namespaceName,
        DeclarationText declarationText, ViewModelData viewModel)
    {
        var code = new CodeWriter();
        code.AppendClass(namespaceName, declarationText,
            body: () => code.AppendViewModelProperties(viewModel.Fields));
        
        context.AddSource(declarationText.GetFileName(namespaceName, "BindProperty"), code.GetSourceText());

        #region Generation Example
        /*  namespace MyNamespace
         *  {
         *  |   public partial class MyClassName
         *  |   {
         *  |   |   public event Action<SomeType> MyNameChanged;
         *  |   |
         *  |   |   private SomeType MyName
         *  |   |   {
         *  |   |   |   get => _myName;
         *  |   |   |   set
         *  |   |   |   {
         *  |   |   |   |   if (ViewModelUtility.EqualsDefault(_someName, value)) return;
         *  |   |   |   |
         *  |   |   |   |   OnMyNameChanging(_myName, value);
         *  |   |   |   |   _myName = value;
         *  |   |   |   |   OnMyNameChanged(value)
         *  |   |   |   |   MyNameChanged?.Invoke(_myName);
         *  |   |   |   }
         *  |   |   }
         *  |   |
         *  |   |   // Other Properties
         *  |   |
         *  |   |   partial void OnMyNameChanging(int oldValue, int newValue);
         *  |   |
         *  |   |   partial void OnMyNameChanged(int newValue);
         *  |   |
         *  |   |   // Other Methods
         *  |   }
         *  }
         */
        #endregion
    }
    
    private static void GenerateIViewModel(SourceProductionContext context, string namespaceName,
        DeclarationText declarationText, ViewModelData viewModel)
    {
        string[]? baseTypes = null;
        if (!viewModel.HasViewModelInterface)
            baseTypes = [Classes.IViewModel.Global];

        var code = new CodeWriter();
        code.AppendClass(namespaceName, declarationText,
            body: () => code.AppendIViewModel(viewModel.HasViewModelBaseType, viewModel.HasViewModelInterface,
                declarationText.Name, viewModel.Fields.Select(field => field.Field)),
            baseTypes);
        
        context.AddSource(declarationText.GetFileName(namespaceName, "IViewModel"), code.GetSourceText());

        #region Generation Example
        /*  namespace MyNamespace
         *  {
         *  |   public partial class MyClassName : IViewModel
         *  |   {
         *  |   |   #if !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
         *  |   |   private static readonly ProfilerMarker _addBinderMarker = new("MyClassName.AddBinder");
         *  |   |   private static readonly ProfilerMarker _removeBinderMarker = new("MyClassName.RemoveBinder");
         *  |   |   #endif
         *  |   |
         *  |   |   public void AddBinder(IBinder binder, string propertyName)
         *  |   |   {
         *  |   |   |   #if ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
         *  |   |   |   using (_addBinderMarker.Auto())
         *  |   |   |   #endif
         *  |   |   |   {
         *  |   |   |   |    AddBinderIternal(binder, propertyName);
         *  |   |   |   }
         *  |   |   }
         *  |   |
         *  |   |   public virtual void AddBinderInternal(IBinder binder, string propertyName)
         *  |   |   {
         *  |   |   |   switch (propertyName)
         *  |   |   |   {
         *  |   |   |   |   case MyPropertyId: AddBinderLocal(MyProperty, ref MyPropertyChanged); return;
         *  |   |   |   |   // Other properties
         *  |   |   |   }
         *  |   |   |   return;
         *  |   |   |
         *  |   |   |   void AddBinderLocal<T>(T value, ref Action<T> changed)
         *  |   |   |   {
         *  |   |   |   |   if (binder is not IBinder<T> specificBinder)
         *  |   |   |   |       throw new Exception();
         *  |   |   |   |
         *  |   |   |   |   specificBinder.SetValue(value);
         *  |   |   |   |   changed += specificBinder.SetValue;
         *  |   |   |   }
         *  |   |   }
         *  |   |
         *  |   |   public void RemoveBinder(IBinder binder, string propertyName)
         *  |   |   {
         *  |   |   |   #if ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
         *  |   |   |   using (_removeBinderMarker.Auto())
         *  |   |   |   #endif
         *  |   |   |   {
         *  |   |   |   |   RemoveBinderIternal(binder, propertyName);
         *  |   |   |   }
         *  |   |   }
         *  |   |   
         *  |   |   protected virtual void RemoveBinderIternal(IBinder binder, string propertyName)
         *  |   |   {
         *  |   |   |   switch (propertyName)
         *  |   |   |   {
         *  |   |   |   |   case MyPropertyId: RemoveBinderLocal(ref MyPropertyChanged); return;
         *  |   |   |   |   // Other properties
         *  |   |   |   }
         *  |   |   |   return;
         *  |   |   |
         *  |   |   |   void RemoveBinderLocal<T>(ref Action<T> changed)
         *  |   |   |   {
         *  |   |   |   |   if (binder is IBinder<T> specificBinder)
         *  |   |   |   |       changed -= specificBinder.SetValue;
         *  |   |   |   }
         *  |   |   }
         *  |   }
         *  }
         */
        #endregion
    }
}