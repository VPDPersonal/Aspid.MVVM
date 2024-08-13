using System.Linq;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Generators.ViewModels.Body;
using MVVMGenerators.Helpers;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Helpers.Extensions.Declarations;
using MVVMGenerators.Helpers.Extensions.Writer;

namespace MVVMGenerators.Generators.ViewModels;

public partial class ViewModelGenerator
{
    private static void GenerateCode(SourceProductionContext context, ViewModelData viewModel)
    {
        var declaration = viewModel.Declaration;
        var namespaceName = declaration.GetNamespaceName();
        var declarationText = declaration.GetDeclarationText();

        if (viewModel.Fields.Length > 0)
        {
            GenerateProperties(context, namespaceName, declarationText, viewModel);
            IdBodyGenerator.GenerateViewModelId(context, declarationText, namespaceName,
                viewModel.Fields.Select(field => field));
        }

        GenerateIViewModel(context, namespaceName, declarationText, viewModel);
    }

    private static void GenerateProperties(SourceProductionContext context, string namespaceName,
        DeclarationText declarationText, ViewModelData viewModel)
    {
        var code = new CodeWriter();
        code.AppendClass(namespaceName, declarationText,
            body: () => code.AppendPropertiesBody(viewModel));

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
            body: () => code.AppendIViewModelBody(viewModel),
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