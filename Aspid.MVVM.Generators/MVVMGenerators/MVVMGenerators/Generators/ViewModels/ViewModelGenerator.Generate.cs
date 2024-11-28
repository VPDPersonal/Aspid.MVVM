using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using System.Collections.Generic;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Helpers.Extensions.Writer;
using MVVMGenerators.Generators.ViewModels.Body;
using MVVMGenerators.Generators.ViewModels.Data;
using MVVMGenerators.Helpers.Extensions.Declarations;

namespace MVVMGenerators.Generators.ViewModels;

public partial class ViewModelGenerator
{
    private static void GenerateCode(SourceProductionContext context, ViewModelData data)
    {
        var dataSpan = new ViewModelDataSpan(data);
        var declaration = dataSpan.Declaration;
        var @namespace = declaration.GetNamespaceName();
        var declarationText = declaration.GetDeclarationText();

        if (dataSpan.Fields.Length + dataSpan.Commands.Length > 0)
        {
            GenerateProperties(@namespace, dataSpan, declarationText, context);

            var idList = new List<(string, string)>();

            foreach (var field in dataSpan.Fields)
            {
                var name = field.PropertyName;
                idList.Add((name, $"{name}Id"));
            }
            
            foreach (var command in dataSpan.Commands)
            {
                var name = command.Execute.Name + "Command";
                idList.Add((name, $"{name}Id"));
            }
            
            foreach (var property in dataSpan.BindAlsoProperties)
            {
                var name = property.Name;
                idList.Add((name, $"{name}Id"));
            }
            
            IdBodyGenerator.GenerateViewModelId(@namespace, declarationText, context, idList);
        }

        if (dataSpan.Commands.Length > 0)
        {
            GenerateCommands(@namespace, dataSpan, declarationText, context);
        }

        GenerateIViewModel(@namespace, dataSpan, declarationText, context);
    }

    private static void GenerateProperties(
        string @namespace,
        in ViewModelDataSpan data,
        DeclarationText declaration,
        SourceProductionContext context)
    {
        var code = new CodeWriter();

        code.AppendClassBegin(@namespace, declaration)
            .AppendPropertiesBody(data)
            .AppendClassEnd(@namespace);

        context.AddSource(declaration.GetFileName(@namespace, "BindProperty"), code.GetSourceText());

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

    private static void GenerateCommands(
        string @namespace,
        in ViewModelDataSpan data,
        DeclarationText declaration,
        SourceProductionContext context)
    {
        var code = new CodeWriter();

        code.AppendClassBegin(@namespace, declaration)
            .AppendRelayCommandBody(data)
            .AppendClassEnd(@namespace);

        context.AddSource(declaration.GetFileName(@namespace, "Commands"), code.GetSourceText());
    }

    private static void GenerateIViewModel(
        string @namespace,
        in ViewModelDataSpan data,
        DeclarationText declaration,
        SourceProductionContext context)
    {
        string[]? baseTypes = null;
        if (data.Inheritor != Inheritor.HasInterface)
            baseTypes = [Classes.IViewModel.Global];

        var code = new CodeWriter();
        code.AppendClassBegin(@namespace, declaration, baseTypes)
            .AppendIViewModelBody(data)
            .AppendClassEnd(@namespace);

        context.AddSource(declaration.GetFileName(@namespace, "IViewModel"), code.GetSourceText());

        #region Generation Example
        /*  namespace MyNamespace
         *  {
         *  |   public partial class MyClassName : IViewModel
         *  |   {
         *  |   |   #if !ASPID_UI_MVVM_UNITY_PROFILER_DISABLED
         *  |   |   private static readonly ProfilerMarker _addBinderMarker = new("MyClassName.AddBinder");
         *  |   |   private static readonly ProfilerMarker _removeBinderMarker = new("MyClassName.RemoveBinder");
         *  |   |   #endif
         *  |   |
         *  |   |   public void AddBinder(IBinder binder, string propertyName)
         *  |   |   {
         *  |   |   |   #if !ASPID_UI_MVVM_UNITY_PROFILER_DISABLED
         *  |   |   |   using (_addBinderMarker.Auto())
         *  |   |   |   #endif
         *  |   |   |   {
         *  |   |   |   |    AddBinderInternal(binder, propertyName);
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
         *  |   |   |   #if !ASPID_UI_MVVM_UNITY_PROFILER_DISABLED
         *  |   |   |   using (_removeBinderMarker.Auto())
         *  |   |   |   #endif
         *  |   |   |   {
         *  |   |   |   |   RemoveBinderInternal(binder, propertyName);
         *  |   |   |   }
         *  |   |   }
         *  |   |
         *  |   |   protected virtual void RemoveBinderInternal(IBinder binder, string propertyName)
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