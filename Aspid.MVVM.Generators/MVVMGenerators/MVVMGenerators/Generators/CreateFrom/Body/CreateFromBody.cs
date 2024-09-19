using System.Text;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Generators.CreateFrom.Data;

namespace MVVMGenerators.Generators.CreateFrom.Body;

public static class CreateFromBody
{
    private static readonly string List = Classes.List.Global;
    private static readonly string Span = Classes.Span.Global;
    private static readonly string IEnumerable = Classes.IEnumerable.Global;
    
    public static CodeWriter AppendCreateFromBody(this CodeWriter code, CreateFromDataSpan data)
    {
        var className = data.Declaration.Identifier.Text;
        var fromTypeFullName = data.FromType.ToDisplayString() ?? string.Empty;

        foreach (var constructor in data.Constructors)
        {
            code.AppendMethods(constructor, className, fromTypeFullName)
                .AppendLine();
        }
        
        return code;
    }

    private static CodeWriter AppendMethods(this CodeWriter code, IMethodSymbol constructor, string className, string fromTypeFullName)
    {
        var (thisName, parametersNames, parameterEnum) = GetParameters(fromTypeFullName, constructor);
        var fromTypeGlobalName = $"global::{fromTypeFullName}";
        
        code.AppendMultiline(
            $$"""
            public static {{className}} To{{className}}<T>(this T {{thisName}}{{parameterEnum}})
                where T : {{fromTypeGlobalName}}
            {
                return new {{className}}({{thisName}}{{parametersNames}});
            }
            
            public static {{className}}[] To{{className}}<T>(this T[] {{thisName}}{{parameterEnum}})
                where T : {{fromTypeGlobalName}}
            {
                var to = new {{className}}[{{thisName}}.Length];
            
                for (var i = 0; i < {{thisName}}.Length; i++)
                    to[i] = new {{className}}({{thisName}}[i]{{parametersNames}});
            
                return to;
            }
            
            public static {{className}}[] To{{className}}AsArray<T>(this {{List}}<T> {{thisName}}{{parameterEnum}})
                where T : {{fromTypeGlobalName}}
            {
                var to = new {{className}}[{{thisName}}.Count];
            
                for (var i = 0; i < {{thisName}}.Count; i++)
                    to[i] = new {{className}}({{thisName}}[i]{{parametersNames}});
            
                return to;
            }
            
            public static {{Span}}<{{className}}> To{{className}}AsSpan<T>(this {{List}}<T> {{thisName}}{{parameterEnum}})
                where T : {{fromTypeGlobalName}}
            {
                var to = new {{className}}[{{thisName}}.Count];
            
                for (var i = 0; i < {{thisName}}.Count; i++)
                    to[i] = new {{className}}({{thisName}}[i]{{parametersNames}});
            
                return to;
            }
            
            public static {{List}}<{{className}}> To{{className}}AsList<T>(this T[] {{thisName}}{{parameterEnum}})
                where T : {{fromTypeGlobalName}}
            {
                var to = new {{List}}<{{className}}>({{thisName}}.Length);
            
                for (var i = 0; i < {{thisName}}.Length; i++)
                    to.Add(new {{className}}({{thisName}}[i]{{parametersNames}}));
            
                return to;
            }
            
            public static {{List}}<{{className}}> To{{className}}<T>(this {{List}}<T> {{thisName}}{{parameterEnum}})
                where T : {{fromTypeGlobalName}}
            {
                var to = new {{List}}<{{className}}>({{thisName}}.Count);
            
                for (var i = 0; i < {{thisName}}.Count; i++)
                    to.Add(new {{className}}({{thisName}}[i]{{parametersNames}}));
            
                return to;
            }
            
            public static {{IEnumerable}}<{{className}}> To{{className}}<T>(this {{IEnumerable}}<T> {{thisName}}{{parameterEnum}})
                where T : {{fromTypeGlobalName}}
            {
                foreach (var __{{thisName}}Item in {{thisName}})
                    yield return new {{className}}(__{{thisName}}Item{{parametersNames}});
            }
            """);

        return code;
    }

    private static (string thisName, string parameterNames, string parameterEnum) GetParameters(string fromTypeFullName, IMethodSymbol constructor)
    {
        var thisName = constructor.Parameters[0].Name;
        var parametersLength = constructor.Parameters.Length;
        if (parametersLength == 1) return (thisName, string.Empty, string.Empty);
        
        var isFromTypeSkip = false;
        
        var parameterEnum = new StringBuilder();
        var parameterNames = new StringBuilder();
        
        for (var i = 0; i < parametersLength; i++)
        {
            var parameterType = constructor.Parameters[i].Type.ToDisplayString();
            
            if (!isFromTypeSkip && parameterType == fromTypeFullName)
            {
                isFromTypeSkip = true;
                continue;
            }

            var parameterName = $"{constructor.Parameters[i].Name}";
            parameterNames.Append($", {parameterName}");
            parameterEnum.Append($", {parameterType} {parameterName}");
        }

        return (thisName, parameterNames.ToString(), parameterEnum.ToString());
    }
}