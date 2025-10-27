using System.Linq;
using Microsoft.CodeAnalysis;
using Aspid.Generators.Helper;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Aspid.MVVM.Generators.Generators.Views.Data.Members;
using Classes = Aspid.MVVM.Generators.Generators.Descriptions.Classes;

namespace Aspid.MVVM.Generators.Generators.Views.Factories;

public static class BinderMembersFactory
{
    public static ImmutableArray<BinderMember> Create(INamedTypeSymbol symbolClass, SemanticModel semanticModel)
    {
        var binderMembers = new List<BinderMember>();
        
        foreach (var member in symbolClass.GetMembers())
        {
            if (member.HasAnyAttributeInSelf()) continue;

            var type = GetType(member);
            if (type is null) continue;

            if (member.TryGetAnyAttributeInSelf(out var asBinderAttribute, Classes.AsBinderAttribute))
            {
                if (asBinderAttribute!.ConstructorArguments[0].Value is not INamedTypeSymbol argumentType) continue;
                
                if (argumentType.IsAbstract) continue;
                if (!argumentType.HasAnyInterfaceInSelfAndBases(Classes.IBinder)) continue;

                var arguments = new List<string>();
                
                if (asBinderAttribute.ConstructorArguments.Length > 1)
                {
                    var nameOfIndexes = new HashSet<int>();
                    var applicationSyntaxReference = asBinderAttribute.ApplicationSyntaxReference!;

                    if (applicationSyntaxReference.SyntaxTree
                            .GetRoot()
                            .FindNode(applicationSyntaxReference.Span) is AttributeSyntax location)
                    {
                        var argumentSyntaxes = location.ArgumentList!.Arguments;
                            
                        for (var i = 1; i < argumentSyntaxes.Count; i++)
                        {
                            if (argumentSyntaxes[i].Expression is InvocationExpressionSyntax invocationExpression &&
                                invocationExpression.Expression is IdentifierNameSyntax identifier &&
                                identifier.Identifier.Text == "nameof")
                            {
                                nameOfIndexes.Add(i - 1);
                            }
                        }
                    }

                    var values = asBinderAttribute.ConstructorArguments[1].Values;
                    
                    for (var i = 0; i < values.Length; i++)
                    {
                        var value = values[i];

                        if (nameOfIndexes.Contains(i))
                        {
                            arguments.Add(value.Value?.ToString() ?? "null");
                            continue;
                        }

                        if (value.Kind == TypedConstantKind.Type)
                        {
                            arguments.Add($"typeof({value.Value})");
                            continue;
                        }

                        if (value.Kind == TypedConstantKind.Enum)
                        {
                            arguments.Add($"({value.Type!.ToDisplayStringGlobal()}){value.Value}");
                            continue;
                        }
                        
                        switch (value.Value)
                        {
                            case null: arguments.Add("null"); break;
                            case char: arguments.Add($"'{value.Value}'"); break;
                            case float: arguments.Add(value.Value + "f"); break;
                            case string: arguments.Add($"\"{value.Value}\""); break;
                            case bool: arguments.Add(value.Value.ToString().ToLower()); break;
                            
                            default: arguments.Add(value.Value.ToString()); break;
                        }
                    }
                }
                
                binderMembers.Add(new AsBinderMember(member, argumentType.ToDisplayStringGlobal(), arguments));
            }
            else if (type.HasAnyAttributeInSelf(Classes.ViewAttribute) || type.HasAnyInterfaceInSelfAndBases(Classes.IView))
            {
                binderMembers.Add(new AsBinderMember(member, Classes.ViewBinder, null));
            }
            else if (type.HasAnyInterfaceInSelfAndBases(Classes.IBinder))
            {
                switch (member)
                {
                    case IFieldSymbol field:
                        if (field.Name.EndsWith(">k__BackingField")) continue;
                        binderMembers.Add(new BinderMember(field));
                        break;
                    
                    case IPropertySymbol property:
                        var symbols = GetPropertyReturnSymbols(property, semanticModel);
                        
                        if (symbols is null) continue;
                        if (symbols.Any(symbol => symbol is IFieldSymbol or IPropertySymbol && !symbol.HasAnyAttributeInSelf(Classes.IgnoreAttribute))) continue;

                        binderMembers.Add(new CachedBinderMember(property));
                        break;
                }
            }
        }

        return binderMembers.ToImmutableArray();
    }
    
    private static ITypeSymbol? GetType(ISymbol member)
    {
        var type = member.GetSymbolType();

        if (type is IArrayTypeSymbol arrayType)
            return arrayType.ElementType;

        return type;
    }
    
    private static IReadOnlyList<ISymbol?>? GetPropertyReturnSymbols(IPropertySymbol property, SemanticModel semanticModel)
    {
        if (property.IsWriteOnly) return default;
        
        var syntaxReference = property.DeclaringSyntaxReferences.First();
        if (syntaxReference.GetSyntax() is not PropertyDeclarationSyntax syntax) return default;

        List<ISymbol?> returnSymbols = [];
        var propertyExpression = syntax.ExpressionBody?.Expression;

        if (propertyExpression is not null)
        {
            AddFromExpression(propertyExpression);
        }
        else
        {
            var getAccessor = syntax.AccessorList?.Accessors
                .FirstOrDefault(accessor => accessor.Kind() == SyntaxKind.GetAccessorDeclaration);
            if (getAccessor is null) return default;

            if (getAccessor.Body is null)
            {
                AddFromExpression(getAccessor.ExpressionBody?.Expression);
            }
            else
            {
                foreach (var statement in getAccessor.Body.Statements)
                {
                    AddFromStatement(statement);
                }
            }
        }

        return returnSymbols;

        void AddFromStatement(StatementSyntax statement)
        {
            if (statement is ReturnStatementSyntax returnStatement)
            {
                AddFromExpression(returnStatement.Expression);
                return;
            }

            foreach (var node in statement.ChildNodes())
            {
                if (node is StatementSyntax childStatement)
                    AddFromStatement(childStatement);
            }
        }

        void AddFromExpression(ExpressionSyntax? expression)
        {
            switch (expression)
            {
                case IdentifierNameSyntax identifier:
                    {
                        returnSymbols.Add(GetSymbol(identifier));
                        break;
                    }
                case ConditionalExpressionSyntax conditional:
                    {
                        AddFromExpression(conditional.WhenTrue);
                        AddFromExpression(conditional.WhenFalse);
  
                        break;
                    }
                case SwitchExpressionSyntax switchExpression:
                    {
                        foreach (var armExpression in switchExpression.Arms.Select(arm => arm.Expression))
                            AddFromExpression(armExpression);

                        break;
                    }
            }
        }

        ISymbol? GetSymbol(IdentifierNameSyntax node) => ModelExtensions.GetSymbolInfo(semanticModel, node).Symbol;
    }
}