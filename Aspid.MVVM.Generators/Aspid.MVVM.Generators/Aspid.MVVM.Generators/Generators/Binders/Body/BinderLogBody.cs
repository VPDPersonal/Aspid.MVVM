using System;
using Microsoft.CodeAnalysis;
using Aspid.Generators.Helper;
using Aspid.MVVM.Generators.Generators.Binders.Data;
using Aspid.MVVM.Generators.Generators.Descriptions;
using static Aspid.Generators.Helper.Classes;
using static Aspid.Generators.Helper.Unity.UnityClasses;
using Classes = Aspid.MVVM.Generators.Generators.Descriptions.Classes;

namespace Aspid.MVVM.Generators.Generators.Binders.Body;

// ReSharper disable InconsistentNaming
public static class BinderLogBody
{
    private const string GeneratedAttribute = General.GeneratedCodeLogBinderAttribute;
    
    private static readonly string IBinder = Classes.IBinder;
    private static readonly string Exception = Aspid.Generators.Helper.Classes.Exception;

    public static CodeWriter AppendBinderLogBody(this CodeWriter code, in BinderDataSpan data)
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

    private static CodeWriter AppendProfilerMarkers(this CodeWriter code, in BinderDataSpan data)
    {
        var modifier = data.Symbol.IsSealed ? "private" : "protected";
        var className = data.Declaration.Identifier.Text;
        
        code.AppendLine($"{modifier} static readonly {ProfilerMarker} SetValueMarker = new(\"{className}.SetValue\");");
        return code.AppendLine();
    }

    private static CodeWriter AppendProperties(this CodeWriter code, in BinderDataSpan data)
    {
        var modifier = data.Symbol.IsSealed ? "private" : "protected";
        
        code.AppendMultiline(
            $"""
            {GeneratedAttribute}
            [{SerializeField}] private bool _isDebug;
            
            // TODO Add Custom Property
            {GeneratedAttribute}
            [{SerializeField}] private {List_1}<string> _log;
            
            {GeneratedAttribute}
            {modifier} bool IsDebug => _isDebug;
            """)
            .AppendLine();
         
        return code;
    }

    private static CodeWriter AppendSetValueMethods(this CodeWriter code, in ReadOnlySpan<IMethodSymbol> methods)
    {
        foreach (var method in methods)
        {
            var parameterName = method.Parameters[0].Name;
            var parameterType = method.Parameters[0].Type.ToDisplayStringGlobal();
            
            code.AppendMultiline(
                $$"""
                {{GeneratedAttribute}}
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
                """)
                .AppendLine();
        }

        return code;
    }

    private static CodeWriter AppendAddLogMethod(this CodeWriter code, in BinderDataSpan data)
    {
        var modifier = data.Symbol.IsSealed ? "private" : "protected";
        
        code.AppendMultiline(
            $$"""
            {{GeneratedAttribute}}
            {{modifier}} void AddLog(string log)
            {
                _log ??= new {{List_1}}<string>();
                _log.Add(log);
            }
            """);

        return code;
    }
}