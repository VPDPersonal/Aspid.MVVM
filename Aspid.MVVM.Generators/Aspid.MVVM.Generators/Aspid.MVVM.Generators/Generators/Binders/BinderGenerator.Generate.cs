using Microsoft.CodeAnalysis;
using Aspid.Generators.Helper;
using Aspid.MVVM.Generators.Generators.Binders.Body;
using Aspid.MVVM.Generators.Generators.Binders.Data;
using Aspid.MVVM.Generators.Generators.Descriptions;
using static Aspid.Generators.Helper.Unity.Defines;

namespace Aspid.MVVM.Generators.Generators.Binders;

public partial class BinderGenerator
{
    private const string PartialBinderLogName = "BinderLog";
    
    private static void GenerateCode(SourceProductionContext context, BinderData binderData)
    {
        var declaration = binderData.Declaration;
        var @namespace = declaration.GetNamespaceName();
        var declarationText = new DeclarationText(declaration);

        GenerateBinderLog(@namespace, new BinderDataSpan(binderData), context, declarationText);
    }

    private static void GenerateBinderLog(
        string @namespace,
        in BinderDataSpan data,
        SourceProductionContext context,
        DeclarationText declarationText)
    {
        if (data.Methods.Length == 0) return;
        
        var code = new CodeWriter();

#if DEBUG
        code.AppendLine($"#if !{Defines.ASPID_MVVM_BINDER_LOG_DISABLED}")
            .AppendClassBegin(@namespace, declarationText)
            .AppendBinderLogBody(data)
            .AppendClassEnd(@namespace)
            .AppendLine("#endif");
#else
        code.AppendLine($"#if {UNITY_EDITOR} && !{Defines.ASPID_MVVM_BINDER_LOG_DISABLED}")
            .BeginClass(@namespace, declarationText)
            .AppendBinderLogBody(data)
            .EndClass(@namespace)
            .Append("#endif");
#endif
        
        context.AddSource(declarationText.GetFileName(@namespace, PartialBinderLogName), code.GetSourceText());
        
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