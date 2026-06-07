using System;
using Microsoft.CodeAnalysis;
using Aspid.Generators.Helper;
using Aspid.MVVM.Generators.Generators.Binders.Data;
using static Aspid.Generators.Helper.Classes;
using static Aspid.Generators.Helper.Unity.UnityClasses;
using static Aspid.MVVM.Generators.Generators.Descriptions.Constants;
using Classes = Aspid.MVVM.Generators.Generators.Descriptions.Classes;

namespace Aspid.MVVM.Generators.Generators.Binders.Body;

// ReSharper disable InconsistentNaming
public static class BinderLogBody
{
    private static readonly string IBinder = Classes.IBinder;
    private static readonly string IAnyBinder = Classes.IAnyBinder;
    private static readonly string Exception = Aspid.Generators.Helper.Classes.Exception;

    extension(CodeWriter code)
    {
        public CodeWriter AppendBinderLogBody(in BinderDataSpan data)
        {
            var hasBinderLogInBaseType = data.HasBinderLogInBaseType;

            if (!hasBinderLogInBaseType)
            {
                code.AppendProfilerMarkers(data)
                    .AppendProperties(data);
            }
        
            code.AppendSetValueMethods(data.Methods);

            if (!hasBinderLogInBaseType)
                code.AppendAddLogMethod(data);
        
            return code;
        }

        private CodeWriter AppendProfilerMarkers(in BinderDataSpan data)
        {
            var modifier = data.Symbol.IsSealed ? "private" : "protected";
            var className = data.Declaration.Identifier.Text;
        
            code.AppendLine($"{modifier} static readonly {ProfilerMarker} SetValueMarker = new(\"{className}.SetValue\");");
            return code.AppendLine();
        }

        private CodeWriter AppendProperties(in BinderDataSpan data)
        {
            var modifier = data.Symbol.IsSealed ? "private" : "protected";
        
            code.AppendMultiline(
                    $"""
                     {GeneratedCodeLogBinderAttribute}
                     [{SerializeField}] private bool _isDebug;

                     // TODO Add Custom Property
                     {GeneratedCodeLogBinderAttribute}
                     [{SerializeField}] private {List_1}<string> _log;

                     {GeneratedCodeLogBinderAttribute}
                     {modifier} bool IsDebug => _isDebug;
                     """)
                .AppendLine();
         
            return code;
        }

        private CodeWriter AppendSetValueMethods(in ReadOnlySpan<IMethodSymbol> methods)
        {
            foreach (var method in methods)
            {
                if (method.IsGenericMethod)
                    code.AppendIAnyBinderSetValueMethod(method);
                else
                    code.AppendIBinderSetValueMethod(method);

                code.AppendLine();
            }

            return code;
        }

        private void AppendIBinderSetValueMethod(IMethodSymbol method)
        {
            var parameterName = method.Parameters[0].Name;
            var parameterType = method.Parameters[0].Type.ToDisplayStringGlobal();

            code.AppendMultiline(
                $$"""
                  {{GeneratedCodeLogBinderAttribute}}
                  void {{IBinder}}<{{parameterType}}>.{{method.Name}}({{parameterType}} {{parameterName}})
                  {
                      if (IsDebug)
                      {
                          try
                          {
                              using (SetValueMarker.Auto())
                              {
                                  SetValue({{parameterName}});
                              }

                              AddLog($"SetValue: {{{parameterName}}}");
                          }
                          catch ({{Exception}} e)
                          {
                              AddLog($"<color=red>Exception: {e}. {nameof({{parameterName}})}: {{parameterName}}</color>");
                              throw;
                          }
                      }
                      else
                      {
                          using (SetValueMarker.Auto())
                          {
                              SetValue({{parameterName}});
                          }
                      }
                  }
                  """);
        }

        private void AppendIAnyBinderSetValueMethod(IMethodSymbol method)
        {
            var typeParamName = method.TypeParameters[0].Name;
            var parameterName = method.Parameters[0].Name;

            code.AppendMultiline(
                $$"""
                  {{GeneratedCodeLogBinderAttribute}}
                  void {{IAnyBinder}}.{{method.Name}}<{{typeParamName}}>({{typeParamName}} {{parameterName}})
                  {
                      if (IsDebug)
                      {
                          try
                          {
                              using (SetValueMarker.Auto())
                              {
                                  SetValue({{parameterName}});
                              }

                              AddLog($"SetValue: {{{parameterName}}}");
                          }
                          catch ({{Exception}} e)
                          {
                              AddLog($"<color=red>Exception: {e}. {nameof({{parameterName}})}: {{parameterName}}</color>");
                              throw;
                          }
                      }
                      else
                      {
                          using (SetValueMarker.Auto())
                          {
                              SetValue({{parameterName}});
                          }
                      }
                  }
                  """);
        }

        private CodeWriter AppendAddLogMethod(in BinderDataSpan data)
        {
            var modifier = data.Symbol.IsSealed ? "private" : "protected";
        
            code.AppendMultiline(
                $$"""
                  {{GeneratedCodeLogBinderAttribute}}
                  {{modifier}} void AddLog(string log)
                  {
                      _log ??= new {{List_1}}<string>();
                      _log.Add(log);
                  }
                  """);

            return code;
        }
    }

}