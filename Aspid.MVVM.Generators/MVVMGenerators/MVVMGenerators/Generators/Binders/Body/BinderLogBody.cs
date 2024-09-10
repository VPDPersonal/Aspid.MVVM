using System;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Generators.Binders.Data;
using MVVMGenerators.Helpers.Extensions.Writer;

namespace MVVMGenerators.Generators.Binders.Body;

// ReSharper disable InconsistentNaming
public static class BinderLogBody
{
    private const string GeneratedAttribute = General.GeneratedCodeBinderModelAttribute;
    
    private static readonly string List = Classes.List.Global;
    private static readonly string IBinder = Classes.IBinder.Global;
    private static readonly string Exception = Classes.Exception.Global;
    private static readonly string SerializeFieldAttribute = Classes.SerializeFieldAttribute.AttributeGlobal;

    public static CodeWriter AppendBinderLogBody(this CodeWriter code, in BinderDataSpan data)
    {
        var hasBinderLogInBaseType = data.HasBinderLogInBaseType;

        if (!hasBinderLogInBaseType)
        {
            code.AppendProfilerMarkers(data)
                .AppendProperties();
        }
        
        code.AppendSetValueMethods(data.Methods);

        if (!hasBinderLogInBaseType)
            code.AppendAddLogMethod();
        
        return code;
    }

    private static CodeWriter AppendProfilerMarkers(this CodeWriter code, in BinderDataSpan data)
    {
        var className = data.Declaration.Identifier.Text;
        code.AppendLine($"protected static readonly {Classes.ProfilerMarker.Global} SetValueMarker = new(\"{className}.SetValue\");");
        return code.AppendLine();
    }

    private static CodeWriter AppendProperties(this CodeWriter code)
    {
        code.AppendMultiline(
            $"""
            {GeneratedAttribute}
            [{SerializeFieldAttribute}] private bool _isDebug;
            
            // TODO Add Custom Property
            {GeneratedAttribute}
            [{SerializeFieldAttribute}] private {Classes.List.Global}<string> _log;
            
            {GeneratedAttribute}
            protected bool IsDebug => _isDebug;
            """)
            .AppendLine();
         
        return code;
    }

    private static CodeWriter AppendSetValueMethods(this CodeWriter code, in ReadOnlySpan<IMethodSymbol> methods)
    {
        code.AppendLoop(methods, method =>
        {
            var parameterName = method.Parameters[0].Name;
            var parameterType = method.Parameters[0].Type;
            
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
                    else SetValue({{parameterName}});
                }
                """)
                .AppendLine();
        });

        return code;
    }

    private static CodeWriter AppendAddLogMethod(this CodeWriter code)
    {
        code.AppendMultiline(
            $$"""
            {{GeneratedAttribute}}
            protected void AddLog(string log)
            {
                _log ??= new {{List}}<string>();
                _log.Add(log);
            }
            """);

        return code;
    }
}