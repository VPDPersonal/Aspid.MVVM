using System;
using System.Text;
using Microsoft.CodeAnalysis;
using Aspid.Generator.Helpers;
using Aspid.MVVM.Generators.Descriptions;
using Aspid.MVVM.Generators.CreateFrom.Data;

namespace Aspid.MVVM.Generators.CreateFrom.Body;

// ReSharper disable InconsistentNaming
public static class CreateFromBody
{
    private const string GeneratedAttribute = General.GeneratedCodeCreateFromAttribute;
    
    private static readonly string List = Classes.List.Global;
    private static readonly string Func = Classes.Func.Global;
    private static readonly string Span = Classes.Span.Global;
    private static readonly string IList = Classes.IList.Global;
    private static readonly string IEnumerable = Classes.IEnumerable.Global;
    private static readonly string ReadOnlySpan = Classes.ReadOnlySpan.Global;
    private static readonly SymbolEqualityComparer Comparer = SymbolEqualityComparer.Default;

    public static CodeWriter AppendCreateFromBody(this CodeWriter code, in CreateFromDataSpan data)
    {
        if (data.Constructor.Parameters.Length == 0) return code;
        var parameters = GetParameters(data.Constructor, data.FromType);

        code.AppendLine($"[{Classes.MethodImplAttribute.Global}({Classes.MethodImplOptions.Global}.AggressiveInlining)]")
            .AppendMethodDeclaration(data, parameters, ParameterType.None, ParameterType.None)
            .AppendMultiline(
                $$"""
                {
                    return new({{parameters.Constructor(parameters.From)}});
                }
                """)
            .AppendLine();

        code.AppendArrayMethodBody(data, ParameterType.Array, parameters)
            .AppendArrayMethodBody(data, ParameterType.Span, parameters)
            .AppendArrayMethodBody(data, ParameterType.ReadOnlySpan, parameters)
            .AppendArrayMethodBody(data, ParameterType.List, parameters);

        code.AppendListMethodBody(data, ParameterType.Array, parameters)
            .AppendListMethodBody(data, ParameterType.List, parameters)
            .AppendListMethodBody(data, ParameterType.Span, parameters)
            .AppendListMethodBody(data, ParameterType.ReadOnlySpan, parameters)
            .AppendListMethodBody(data ,ParameterType.Enumerable, parameters);
            
        code.AppendIListMethodBody(data, ParameterType.Array, parameters)
            .AppendIListMethodBody(data, ParameterType.List, parameters)
            .AppendIListMethodBody(data, ParameterType.Span, parameters)
            .AppendIListMethodBody(data, ParameterType.ReadOnlySpan, parameters)
            .AppendIListMethodBody(data, ParameterType.Enumerable, parameters);

        code.AppendEnumerableMethodBody(data, ParameterType.Array, ParameterType.Enumerable, parameters)
            .AppendEnumerableMethodBody(data, ParameterType.List, ParameterType.Enumerable, parameters)
            .AppendEnumerableMethodBody(data, ParameterType.Enumerable, ParameterType.Enumerable, parameters);

        return code;
    }

    private static CodeWriter AppendArrayMethodBody(
        this CodeWriter code,
        in CreateFromDataSpan data,
        ParameterType fromParameterType,
        in Parameters parameters)
    {
        var fromName = parameters.From;

        var capacity = GetLengthType(fromParameterType) switch
        {
            LengthType.None => "0",
            LengthType.Count => $"{fromName}.Count",
            LengthType.Length => $"{fromName}.Length",
            _ => throw new ArgumentOutOfRangeException()
        };
        
        return code
            .AppendMethodDeclaration(data, parameters, fromParameterType, ParameterType.Array)
            .AppendMultiline(
            $$"""
              {
                  var __to = new {{data.ToTypeName}}[{{capacity}}];
                  
                  for (var __i = 0; __i < __to.Length; __i++)
                      __to[__i] = {{fromName}}[__i].{{data.MethodName}}({{parameters.EnumParameters}});
                      
                  return __to;
              }
              """)
            .AppendLine();
    }

    private static CodeWriter AppendListMethodBody(
        this CodeWriter code, 
        in CreateFromDataSpan data,
        ParameterType fromParameterType,
        in Parameters parameters)
    {
        var capacity = GetLengthType(fromParameterType) switch
        {
            LengthType.None => string.Empty,
            LengthType.Count => $"{parameters.From}.Count",
            LengthType.Length => $"{parameters.From}.Length",
            _ => throw new ArgumentOutOfRangeException()
        };
        
        return code
            .AppendMethodDeclaration(data, parameters, fromParameterType, ParameterType.List)
            .AppendMultiline(
            $$"""
              {
                  var __to = new {{List}}<{{data.ToTypeName}}>({{capacity}});
                  
                  foreach(var __from in {{parameters.From}})
                      __to.Add(__from.{{data.MethodName}}({{parameters.EnumParameters}}));
                      
                  return __to;
              }
              """)
            .AppendLine();
    }

    private static CodeWriter AppendIListMethodBody(
        this CodeWriter code, 
        in CreateFromDataSpan data,
        ParameterType fromParameterType,
        in Parameters parameters)
    {
        return code
            .AppendMethodDeclaration(data, parameters, fromParameterType, ParameterType.IList)
            .AppendMultiline(
            $$"""
              {
                  var __to = __createList();
                  
                  foreach(var __from in {{parameters.From}})
                      __to.Add(__from.{{data.MethodName}}({{parameters.EnumParameters}}));
                      
                  return __to;
              }
              """)
            .AppendLine();
    }

    private static CodeWriter AppendEnumerableMethodBody(
        this CodeWriter code, 
        in CreateFromDataSpan data,
        ParameterType fromParameterType,
        ParameterType toParameterType,
        in Parameters parameters)
    {
        return code
            .AppendMethodDeclaration(data, parameters, fromParameterType, toParameterType)
            .AppendMultiline(
            $$"""
            {
                foreach (var __from in {{parameters.From}})
                    yield return __from.{{data.MethodName}}({{parameters.EnumParameters}});
            }
            """)
            .AppendLine();
    }

    private static CodeWriter AppendMethodDeclaration(
        this CodeWriter code,
        in CreateFromDataSpan data,
        in Parameters parameters,
        ParameterType fromParameterType,
        ParameterType toParameterType)
    {
        var methodName = data.MethodName;
        var returnType = data.ToTypeName;
        var additionalParameter = string.Empty;
        
        switch (toParameterType)
        {
            case ParameterType.None: break;
            case ParameterType.List:
                methodName += "AsList";
                returnType = $"{List}<{returnType}>";
                break;
            
            case ParameterType.Span:
            case ParameterType.Array: 
            case ParameterType.ReadOnlySpan:
                methodName += "AsArray";
                returnType += "[]";
                break;
            
            case ParameterType.Enumerable:
                methodName += "AsEnumerable";
                returnType = $"{IEnumerable}<{returnType}>";
                break;

            case ParameterType.IList:
                methodName += "AsList";
                returnType = $"{IList}<{returnType}>";
                additionalParameter = $", {Func}<{IList}<{data.ToTypeName}>> __createList";
                break;
            
            default: throw new ArgumentOutOfRangeException(nameof(toParameterType), toParameterType, null);
        }

        if (!data.CanBeInherited)
        {
            return code.AppendMultiline(
                $"""
                 {GeneratedAttribute}
                 public static {returnType} {methodName}({parameters.Method(fromParameterType, data.FromTypeName)}{additionalParameter})
                 """);
        }

        return code.AppendMultiline(
            $"""
            {GeneratedAttribute}
            public static {returnType} {methodName}<T>({parameters.Method(fromParameterType, "T")}{additionalParameter})
                where T : {data.FromTypeName}
            """);
    }

    private static LengthType GetLengthType(ParameterType type) => type switch
    {
        ParameterType.None => LengthType.None,
        ParameterType.Array => LengthType.Length,
        ParameterType.List => LengthType.Count,
        ParameterType.IList => LengthType.Count,
        ParameterType.Span => LengthType.Length,
        ParameterType.ReadOnlySpan => LengthType.Length,
        ParameterType.Enumerable => LengthType.None,
        _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
    };
    
    private static Parameters GetParameters(IMethodSymbol constructor, ITypeSymbol fromType)
    {
        string? fromName = null;
        StringBuilder parameters = new();
        StringBuilder methodParameters = new();
        StringBuilder constructorParameters = new();
        
        foreach (var parameter in  constructor.Parameters)
        {
            if (constructorParameters.Length is not 0)
                constructorParameters.Append(", ");

            if (fromName is null && Comparer.Equals(parameter.Type, fromType))
            {
                fromName = parameter.Name;
                constructorParameters.Append("{0}");
            }
            else
            {
                if (parameters.Length is not 0)
                    parameters.Append(", ");
                
                parameters.Append($"{parameter.Name}");
                methodParameters.Append($", {parameter.Type.ToDisplayStringGlobal()} {parameter.Name}");
                constructorParameters.Append($"{parameter.Name}");
            }
        }

        return new Parameters(fromName!, methodParameters, parameters, constructorParameters);
    }
    
    private readonly ref struct Parameters(
        string from,
        StringBuilder method,
        StringBuilder parameters,
        StringBuilder constructor)
    {
        public readonly string From = from;
        public readonly string EnumParameters = parameters.ToString();
        
        private readonly string _methods = from + method;
        private readonly string _constructor = constructor.ToString();
        
        
        public string Method(ParameterType type, string fromType)
        {
            return type switch
            {
                ParameterType.None => $"this {fromType} {_methods}",
                ParameterType.Array => $"this {fromType}[] {_methods}",
                ParameterType.Span => $"this {Span}<{fromType}> {_methods}",
                ParameterType.List => $"this {List}<{fromType}> {_methods}",
                ParameterType.IList => $"this {IList}<{fromType}> {_methods}",
                ParameterType.ReadOnlySpan => $"this {ReadOnlySpan}<{fromType}> {_methods}",
                ParameterType.Enumerable => $"this {IEnumerable}<{fromType}> {_methods}",
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        public string Constructor(string fromName) => string.Format(_constructor, fromName);
    }

    private enum LengthType
    {
        None,
        Count,
        Length,
    }
    
    private enum ParameterType
    {
        None,
        List,
        IList,
        Array,
        Span,
        ReadOnlySpan,
        Enumerable,
    }
} 