using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using MVVMGenerators.Descriptions;

namespace MVVMGenerators.Generators.Binders;

public static class BinderLogBody
{
    private const string LogVar = "_log";
    private const string IsDebugVar = "_isDebug";

    private const string IsDebugProperty = "IsDebug";

    private const string AddLogMethod = "AddLog";
    private const string SetValueMethod = "SetValue";
    
    public static void AppendBinderLog(this CodeWriter code, BinderData binderData)
    {
        var hasBinderLogInBaseType = binderData.HasBinderLogInBaseType;
        
        if (!hasBinderLogInBaseType)
            code.AppendFieldsAndProperties();

        foreach (var method in binderData.BinderLogMethods)
            code.AppendMethod(method);

        if (!hasBinderLogInBaseType)
            code.AppendAddLog();
    }
    
    private static void AppendFieldsAndProperties(this CodeWriter code)
    {
        code.AppendMultiline(
            $"""
             {General.GeneratedCodeBinderModelAttribute}
             [{Classes.SerializeFieldAttribute.AttributeGlobal}] private bool {IsDebugVar};

             // TODO Add Custom Property
             {General.GeneratedCodeBinderModelAttribute}
             [{Classes.SerializeFieldAttribute.AttributeGlobal}] private {Classes.List.Global}<string> {LogVar};

             {General.GeneratedCodeBinderModelAttribute}
             protected bool {IsDebugProperty} => {IsDebugVar};
             """);

        code.AppendLine();
    }

    private static void AppendMethod(this CodeWriter code, IMethodSymbol method)
    {
        var parameterName = method.Parameters[0].Name;
        var parameterType = method.Parameters[0].Type.ToDisplayString();

        code.AppendMultiline(
            $$"""
              {{General.GeneratedCodeBinderModelAttribute}}
              void {{Classes.IBinder.Global}}<{{parameterType}}>.{{method.Name}}({{parameterType}} {{parameterName}})
              {
                  if ({{IsDebugProperty}})
                  {
                      try
                      {
                          {{SetValueMethod}}({{parameterName}});
                          {{AddLogMethod}}($"Set Value: {{{parameterName}}}");
                      }
                      catch ({{Classes.Exception.Global}} e)
                      {
                          {{AddLogMethod}}($"<color=red>Exception: {e.Message}. {nameof({{parameterName}})}: {{parameterName}}</color>");
                          throw;
                      }
                  }
                  else {{SetValueMethod}}({{parameterName}});
              }
              """);

        code.AppendLine();
    }

    private static void AppendAddLog(this CodeWriter code)
    {
        code.AppendMultiline(
            $$"""
              {{General.GeneratedCodeBinderModelAttribute}}
              protected void {{AddLogMethod}}(string log)
              {
                  {{LogVar}} ??= new {{Classes.List.Global}}<string>();
                  {{LogVar}}.Add(log);
              }
              """);
    }
}