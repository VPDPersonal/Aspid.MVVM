using System.Text;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Generators.CreateFrom.Data;

namespace MVVMGenerators.Generators.CreateFrom.Body;

// ReSharper disable InconsistentNaming
public static class CreateFromBody
{
    private static readonly string List = Classes.List.Global;
    private static readonly string IEnumerable = Classes.IEnumerable.Global;
    
    public static CodeWriter AppendCreateFromBody(this CodeWriter code, CreateFromDataSpan data)
    {
        var toName = data.Declaration.Identifier.Text;
        var fromTypeFullName = data.FromType.ToDisplayString();

        foreach (var constructor in data.Constructors)
        {
            code.AppendMethods(constructor, toName, fromTypeFullName)
                .AppendLine();
        }
        
        return code;
    }

    private static CodeWriter AppendMethods(this CodeWriter code, IMethodSymbol constructor, string toName, string fromTypeFullName)
    {
        var parameters = GetParameters(constructor, fromTypeFullName);
        fromTypeFullName = $"global::{fromTypeFullName}";

        var methodName = $"To{toName}";
        var fromName = parameters.FromName;
        var parameterNames = parameters.ParameterNames;
        var methodParameters = $"{fromName}{parameters.ParametersEnum}";
        
        code.AppendMultiline(
            $$"""
            public static {{toName}} {{methodName}}<T>(this T {{methodParameters}})
                where T : {{fromTypeFullName}}
            {
                return new {{toName}}({{fromName}}{{parameterNames}});
            }
            
            public static {{toName}}[] {{methodName}}<T>(this T[] {{methodParameters}})
                where T : {{fromTypeFullName}}
            {
                var __to = new {{toName}}[{{fromName}}.Length];
            
                for (var i = 0; i < {{fromName}}.Length; i++)
                    __to[i] = new {{toName}}({{fromName}}[i]{{parameterNames}});
            
                return __to;
            }
            
            public static {{toName}}[] {{methodName}}AsArray<T>(this {{List}}<T> {{methodParameters}})
                where T : {{fromTypeFullName}}
            {
                var __to = new {{toName}}[{{fromName}}.Count];
            
                for (var i = 0; i < {{fromName}}.Count; i++)
                    __to[i] = new {{toName}}({{fromName}}[i]{{parameterNames}});
            
                return __to;
            }
            
            public static {{List}}<{{toName}}> {{methodName}}AsList<T>(this T[] {{methodParameters}})
                where T : {{fromTypeFullName}}
            {
                var __to = new {{List}}<{{toName}}>({{fromName}}.Length);
            
                foreach(var __{{fromName}}Item in {{fromName}})
                    __to.Add(new {{toName}}(__{{fromName}}Item{{parameterNames}}));
            
                return __to;
            }
            
            public static {{List}}<{{toName}}> {{methodName}}<T>(this {{List}}<T> {{methodParameters}})
                where T : {{fromTypeFullName}}
            {
                var __to = new {{List}}<{{toName}}>({{fromName}}.Count);
            
                foreach(var __{{fromName}}Item in {{fromName}})
                    __to.Add(new {{toName}}(__{{fromName}}Item{{parameterNames}}));
            
                return __to;
            }
            
            public static {{IEnumerable}}<{{toName}}> {{methodName}}<T>(this {{IEnumerable}}<T> {{methodParameters}})
                where T : {{fromTypeFullName}}
            {
                foreach (var __{{fromName}}Item in {{fromName}})
                    yield return new {{toName}}(__{{fromName}}Item{{parameterNames}});
            }
            """);

        return code;
    }

    private static Parameters GetParameters(IMethodSymbol constructor, string fromTypeFullName)
    {
        var fromName = constructor.Parameters[0].Name;
        if (constructor.Parameters.Length == 1) return new Parameters(fromName);
        
        var isFromTypeSkip = false;
        var parametersEnum = new StringBuilder();
        var parameterNames = new StringBuilder();

        foreach (var parameter in constructor.Parameters)
        {
            var parameterType = parameter.Type.ToDisplayString();
            
            if (!isFromTypeSkip && parameterType == fromTypeFullName)
            {
                isFromTypeSkip = true;
                continue;
            }

            var parameterName = $"{parameter.Name}";
            parameterNames.Append($", {parameterName}");
            parametersEnum.Append($", {parameterType} {parameterName}");
        }

        return new Parameters(fromName, parameterNames, parametersEnum);
    }

    private readonly ref struct Parameters(
        string fromName,
        StringBuilder? parameterNames = null,
        StringBuilder? parametersEnum = null)
    {
        public readonly string FromName = fromName;
        public readonly string ParameterNames = parameterNames?.ToString() ?? string.Empty;
        public readonly string ParametersEnum = parametersEnum?.ToString() ?? string.Empty;
    }
}