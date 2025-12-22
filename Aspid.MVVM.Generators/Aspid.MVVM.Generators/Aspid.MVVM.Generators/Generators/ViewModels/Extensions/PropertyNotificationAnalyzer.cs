using System.Linq;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Aspid.MVVM.Generators.Generators.ViewModels.Extensions;

public static class PropertyNotificationAnalyzer
{
    /// <summary>
    /// Анализирует класс и возвращает данные о вызовах OnPropertyChanged и SetField.
    /// </summary>
    public static Data.PropertyNotificationData Analyze(
        TypeDeclarationSyntax classDeclaration,
        IReadOnlyDictionary<string, string> bindableProperties) // propertyName -> type
    {
        var data = new Data.PropertyNotificationData();

        foreach (var invocation in classDeclaration.DescendantNodes().OfType<InvocationExpressionSyntax>())
        {
            var methodName = invocation.GetMethodName();
            
            switch (methodName)
            {
                case "OnPropertyChanged":
                    AnalyzeOnPropertyChanged(invocation, bindableProperties, data);
                    break;
                
                case "SetField":
                    AnalyzeSetField(invocation, bindableProperties, data);
                    break;
            }
        }

        return data;
    }

    private static void AnalyzeOnPropertyChanged(
        InvocationExpressionSyntax invocation,
        IReadOnlyDictionary<string, string> bindableProperties,
        Data.PropertyNotificationData data)
    {
        var line = invocation.GetLocation().GetLineSpan().StartLinePosition.Line + 1;
        var arguments = invocation.ArgumentList.Arguments;

        if (arguments.Count == 0)
        {
            // OnPropertyChanged() - определяем свойство по контексту (ищем свойство в котором вызов)
            var propertyName = FindContainingPropertyName(invocation);
            if (propertyName is not null && bindableProperties.ContainsKey(propertyName))
            {
                data.AddOnPropertyChangedCall(propertyName, line);
            }
        }
        else
        {
            // OnPropertyChanged(nameof(FirstName)) или OnPropertyChanged("FirstName")
            var propertyName = ExtractPropertyName(arguments[0].Expression);
            if (propertyName is not null && bindableProperties.ContainsKey(propertyName))
            {
                data.AddOnPropertyChangedCall(propertyName, line);
            }
        }
    }

    private static void AnalyzeSetField(
        InvocationExpressionSyntax invocation,
        IReadOnlyDictionary<string, string> bindableProperties,
        Data.PropertyNotificationData data)
    {
        var line = invocation.GetLocation().GetLineSpan().StartLinePosition.Line + 1;
        var arguments = invocation.ArgumentList.Arguments;

        // SetField(ref _field, value) - минимум 2 аргумента
        // SetField(ref _field, value, nameof(Property)) - 3 аргумента
        if (arguments.Count < 2) return;

        string? propertyName;
        
        if (arguments.Count >= 3)
        {
            // Третий аргумент - имя свойства
            propertyName = ExtractPropertyName(arguments[2].Expression);
        }
        else
        {
            // Определяем по контексту свойства (вызов внутри свойства)
            propertyName = FindContainingPropertyName(invocation);
        }

        if (propertyName is not null && bindableProperties.TryGetValue(propertyName, out var type))
        {
            data.AddSetFieldCall(propertyName, line, type);
        }
    }

    private static string? FindContainingPropertyName(SyntaxNode node)
    {
        var current = node.Parent;
        while (current is not null)
        {
            if (current is PropertyDeclarationSyntax property)
            {
                return property.Identifier.Text;
            }
            current = current.Parent;
        }
        return null;
    }

    private static string? ExtractPropertyName(ExpressionSyntax expression)
    {
        return expression switch
        {
            // nameof(FirstName)
            InvocationExpressionSyntax { Expression: IdentifierNameSyntax { Identifier.Text: "nameof" } } nameofExpr
                when nameofExpr.ArgumentList.Arguments.Count > 0
                => GetIdentifierFromExpression(nameofExpr.ArgumentList.Arguments[0].Expression),
            
            // "FirstName"
            LiteralExpressionSyntax literal => literal.Token.ValueText,
            
            _ => null
        };
    }

    private static string? GetIdentifierFromExpression(ExpressionSyntax expression)
    {
        return expression switch
        {
            IdentifierNameSyntax identifier => identifier.Identifier.Text,
            MemberAccessExpressionSyntax memberAccess => memberAccess.Name.Identifier.Text,
            _ => null
        };
    }
}

