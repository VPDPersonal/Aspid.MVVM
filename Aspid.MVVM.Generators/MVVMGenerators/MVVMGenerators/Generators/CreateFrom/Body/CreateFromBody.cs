using System;
using System.Text;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Generators.CreateFrom.Data;
using MVVMGenerators.Helpers.Extensions.Symbols;

namespace MVVMGenerators.Generators.CreateFrom.Body;

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

        code.AppendMethodDeclaration(data, parameters, ParameterType.None, ParameterType.None)
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
        var constructor = parameters.Constructor($"{fromName}[__i]");

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
                      __to[__i] = new({{constructor}});
                      
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
        const string local = "__from";

        var capacity = GetLengthType(fromParameterType) switch
        {
            LengthType.None => string.Empty,
            LengthType.Count => $"{parameters.From}.Count",
            LengthType.Length => $"{parameters.From}.Length",
            _ => throw new ArgumentOutOfRangeException()
        };
        
        var fromName = parameters.From;
        var constructor = parameters.Constructor(local);

        return code
            .AppendMethodDeclaration(data, parameters, fromParameterType, ParameterType.List)
            .AppendMultiline(
            $$"""
              {
                  var __to = new {{List}}<{{data.ToTypeName}}>({{capacity}});
                  
                  foreach(var {{local}} in {{fromName}})
                      __to.Add(new({{constructor}}));
                      
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
        const string local = "__from";

        var fromName = parameters.From;
        var constructor = parameters.Constructor(local);

        return code
            .AppendMethodDeclaration(data, parameters, fromParameterType, ParameterType.IList)
            .AppendMultiline(
            $$"""
              {
                  var __to = __createList();
                  
                  foreach(var {{local}} in {{fromName}})
                      __to.Add(new({{constructor}}));
                      
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
        const string local = "__from";

        var fromName = parameters.From;
        var constructor = parameters.Constructor(local);

        return code
            .AppendMethodDeclaration(data, parameters, fromParameterType, toParameterType)
            .AppendMultiline(
            $$"""
            {
                foreach (var {{local}} in {{fromName}})
                    yield return new({{constructor}});
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
        var methodName = $"To{data.ToName}";
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
        StringBuilder methodParameters = new();
        StringBuilder constructorParameters = new();
        var parameters = constructor.Parameters;

        foreach (var parameter in parameters)
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
                methodParameters.Append($", {parameter.Type.ToDisplayStringGlobal()} {parameter.Name}");
                constructorParameters.Append($"{parameter.Name}");
            }
        }

        return new Parameters(fromName!, methodParameters, constructorParameters);
    }
    
    private readonly ref struct Parameters(
        string from,
        StringBuilder method,
        StringBuilder constructor)
    {
        public readonly string From = from;

        private readonly string _method = from + method;
        private readonly string _constructor = constructor.ToString();

        public string Method(ParameterType type, string fromType) => type switch
        {
            ParameterType.None => $"this {fromType} {_method}",
            ParameterType.Array => $"this {fromType}[] {_method}",
            ParameterType.Span => $"this {Span}<{fromType}> {_method}",
            ParameterType.List => $"this {List}<{fromType}> {_method}",
            ParameterType.IList => $"this {IList}<{fromType}> {_method}",
            ParameterType.ReadOnlySpan => $"this {ReadOnlySpan}<{fromType}> {_method}",
            ParameterType.Enumerable => $"this {IEnumerable}<{fromType}> {_method}",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };

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