using Microsoft.CodeAnalysis;
using Aspid.MVVM.Generators.Helpers;
using Aspid.MVVM.Generators.Shared;
using Aspid.Generators.Helper;
using Aspid.MVVM.Generators.Generators.Binders.Body;
using Aspid.MVVM.Generators.Generators.Binders.Data;
using Aspid.MVVM.Generators.Generators.Descriptions;

namespace Aspid.MVVM.Generators.Generators.Binders;

public partial class BinderGenerator
{
    private const string PartialBinderLogName = "BinderLog";
    
    private static void GenerateCode(SourceProductionContext context, BinderData binderData)
    {
        if (AspidTypeDeclaration.ValidateContainers(binderData.Symbol) is { } diagnostic)
        {
            context.ReportDiagnostic(diagnostic);
            return;
        }
        var declarationText = new AspidTypeDeclaration(binderData.Symbol);

        GenerateBinderLog(new BinderDataSpan(binderData), context, declarationText);
    }

    private static void GenerateBinderLog(
        in BinderDataSpan data,
        SourceProductionContext context,
        AspidTypeDeclaration declarationText)
    {
        if (data.Methods.Length == 0) return;
        
        var code = new CodeWriter();

#if DEBUG
        code.AppendLine($"#if !{Defines.ASPID_MVVM_BINDER_LOG_DISABLED}")
            .BeginScopedClass(declarationText)
            .AppendBinderLogBody(data)
            .EndScopedClass(declarationText)
            .AppendLine("#endif");
#else
        code.AppendLine($"#if {Aspid.Generators.Helper.Unity.Defines.UNITY_EDITOR} && !{Defines.ASPID_MVVM_BINDER_LOG_DISABLED}")
            .BeginScopedClass(declarationText)
            .AppendBinderLogBody(data)
            .EndScopedClass(declarationText)
            .Append("#endif");
#endif
        
        context.AddSource(declarationText.GetFileName(PartialBinderLogName), code.GetSourceText());
        
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