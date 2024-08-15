using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Helpers.Extensions.Writer;
using MVVMGenerators.Helpers.Extensions.Declarations;

namespace MVVMGenerators.Generators.Binders;

public partial class BinderGenerator
{
    private static void GenerateCode(SourceProductionContext context, BinderData binderData)
    {
        var declaration = binderData.Declaration;
        var namespaceName = declaration.GetNamespaceName();
        var declarationText = declaration.GetDeclarationText();

        GenerateBinderLog(context, binderData, namespaceName, declarationText);
    }

    private static void GenerateBinderLog(
        SourceProductionContext context, BinderData binderData,
        string namespaceName, DeclarationText declarationText)
    {
        if (binderData.BinderLogMethods.Count == 0) return;
        
        var code = new CodeWriter();

#if DEBUG
        code.AppendLine($"#if !{Defines.ULTIMATE_UI_MVVM_BINDER_LOG_DISABLED}")
            .AppendClass(namespaceName, declarationText, body: () => code.AppendBinderLog(binderData))
            .AppendLine("#endif");
#else
        code.AppendLine($"#if {Defines.UNITY_EDITOR} && !{Defines.ULTIMATE_UI_MVVM_BINDER_LOG_DISABLED}")
            .AppendClass(namespaceName, declarationText, body: () => code.AppendBinderLog(binderData))
            .Append("#endif");
#endif
        
        context.AddSource(declarationText.GetFileName(namespaceName, PartialBinderLogName), code.GetSourceText());
        
        #region Generation Example
        /*  #if UNITY_EDITOR
         *  namespace MyNamespace
         *  {
         *  |   public partial class MyClassName
         *  |   {
         *  |   |   [SerializeField] private bool _isDebug;
         *  |   |   [SerializeField] private List<string> _log;
         *  |   |
         *  |   |   protected bool IsDebug => _isDebug;
         *  |   |
         *  |   |   void IBinder<SomeType>.SetValue(SomeType value)
         *  |   |   {
         *  |   |   |   if (IsDebug)
         *  |   |   |   {
         *  |   |   |   |   try
         *  |   |   |   |   {
         *  |   |   |   |   |   SetValue(value);
         *  |   |   |   |   |   AddLog($"SetValue: {value}");
         *  |   |   |   |   }
         *  |   |   |   |   catch (Exception e)
         *  |   |   |   |   {
         *  |   |   |   |   |   AddLog($"<color=red>Exception: {e.Message}. {nameof(value)}: value</color>")
         *  |   |   |   |   |   throw;
         *  |   |   |   |   }
         *  |   |   |   }
         *  |   |   |   else SetValue(value);
         *  |   |   }
         *  |   |
         *  |   |   // Other SetValue methods
         *  |   |
         *  |   |   protected void AddLog(string log)
         *  |   |   {
         *  |   |   |   _log ??= new global::System.Collections.Generic.List<string>();
         *  |   |   |   _log.Add(log);
         *  |   |   }
         *  |   }
         *  }
         *  #endif
         */
        #endregion
    }
}