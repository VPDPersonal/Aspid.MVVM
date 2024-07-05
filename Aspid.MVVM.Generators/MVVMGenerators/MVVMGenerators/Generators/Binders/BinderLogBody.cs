using Microsoft.CodeAnalysis;
using MVVMGenerators.Descriptions;
using MVVMGenerators.Helpers;

namespace MVVMGenerators.Generators.Binders;

public static class BinderLogBody
{
    public static void AppendBinderLog(this CodeWriter code, BinderData binderData)
    {
        if (!binderData.HasBinderLogInBaseType)
            code.AppendFieldsAndProperties();

        foreach (var method in binderData.BinderLogMethods)
            code.AppendMethod(method);

        if (!binderData.HasBinderLogInBaseType)
            code.AppendAddLog();
    }
    
    private static void AppendFieldsAndProperties(this CodeWriter code)
    {
        code.AppendMultiline(
            $"""
             {General.GeneratedCodeAttribute}
             [{Classes.SerializeFieldAttribute.AttributeGlobal}] private bool _isDebug;

             // TODO Add Custom Property
             {General.GeneratedCodeAttribute}
             [{Classes.SerializeFieldAttribute.AttributeGlobal}] private {Classes.List.Global}<string> _log;

             {General.GeneratedCodeAttribute}
             protected bool IsDebug => _isDebug;
             """);

        code.AppendLine();
    }

    private static void AppendMethod(this CodeWriter code, IMethodSymbol method)
    {
        var parameterName = method.Parameters[0].Name;
        var parameterType = method.Parameters[0].Type.ToDisplayString();

        code.AppendMultiline(
            $$"""
              {{General.GeneratedCodeAttribute}}
              void {{Classes.IBinder.Global}}<{{parameterType}}>.{{method.Name}}({{parameterType}} {{parameterName}})
              {
                  if (IsDebug)
                  {
                      try
                      {
                          SetValue({{parameterName}});
                          AddLog($"Set Value: {{{parameterName}}}");
                      }
                      catch ({{Classes.Exception.Global}} e)
                      {
                          AddLog($"<color=red>Exception: {e.Message}. {nameof({{parameterName}})}: {{parameterName}}</color>");
                          throw;
                      }
                  }
                  else SetValue({{parameterName}});
              }
              """);

        code.AppendLine();
    }

    private static void AppendAddLog(this CodeWriter code)
    {
        code.AppendMultiline(
            $$"""
              {{General.GeneratedCodeAttribute}}
              protected void AddLog(string log)
              {
                  _log ??= new {{Classes.List.Global}}<string>();
                  _log.Add(log);
              }
              """);
    }
}