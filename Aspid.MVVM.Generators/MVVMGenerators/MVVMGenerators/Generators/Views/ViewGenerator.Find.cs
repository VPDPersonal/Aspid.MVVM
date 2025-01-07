using System;
using System.Linq;
using System.Threading;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp;
using System.Runtime.CompilerServices;
using MVVMGenerators.Helpers.Descriptions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MVVMGenerators.Generators.Views.Data;
using MVVMGenerators.Helpers.Extensions.Symbols;
using MVVMGenerators.Generators.Views.Data.Members;
using Field = Microsoft.CodeAnalysis.IFieldSymbol;
using Property = Microsoft.CodeAnalysis.IPropertySymbol;

namespace MVVMGenerators.Generators.Views;

public partial class ViewGenerator
{
    private static FoundForGenerator<ViewData> FindView(
        GeneratorAttributeSyntaxContext context,
        CancellationToken cancellationToken)
    {
        if (context.TargetSymbol is not INamedTypeSymbol symbol) return default;

        var inheritor = RecognizeInheritor(symbol);
        var members = symbol.GetMembers();
        var viewMembers = GetViewMembers(members, context.SemanticModel);

        RecognizeOverriddenMethods(
            inheritor,
            members.OfType<IMethodSymbol>(),
            out var isInitializeOverride,
            out var isDeinitializeOverride);

        Debug.Assert(context.TargetNode is TypeDeclarationSyntax);
        var candidate = Unsafe.As<TypeDeclarationSyntax>(context.TargetNode);

        var viewData = new ViewData(inheritor, viewMembers, isInitializeOverride, isDeinitializeOverride, candidate);
        return new FoundForGenerator<ViewData>(true, viewData);
    }

    private static Inheritor RecognizeInheritor(INamedTypeSymbol symbol)
    {
        var baseType = symbol.BaseType;
        
        // Strictly defined order
        for (var type = baseType; type is not null; type = type.BaseType)
        {
            if (type.HasAttribute(Classes.ViewAttribute)) 
                return Inheritor.InheritorViewAttribute;
        }
        
        if (symbol.HasBaseType(Classes.View, Classes.MonoView, Classes.ScriptableView)) return Inheritor.InheritorView;
        if (baseType is not null && baseType.HasInterface(Classes.IView)) return Inheritor.HasInterface;

        return symbol.HaseDirectInterface(Classes.IView) ? Inheritor.HasInterface : Inheritor.None;
    }

    private static ViewMembers GetViewMembers(ImmutableArray<ISymbol> members, SemanticModel semanticModel)
    {
        List<BinderFieldInView> otherMembers = [];
        List<PropertyBinderInView> propertyMembers = [];
        List<AsBinderMemberInView> asBinderMembers = [];

        foreach (var member in members)
        {
            var type = GetType(member);
            if (type == null) continue;
            
            if (member.HasAttribute(Classes.IgnoreAttribute)) continue;

            // Strictly defined order
            if (member.HasAttribute(Classes.AsBinderAttribute, out var asBinderAttribute))
            {
                if (asBinderAttribute!.ConstructorArguments[0].Value is not INamedTypeSymbol argumentType) continue;
                
                if (argumentType.IsAbstract) continue;
                if (!argumentType.HasInterface(Classes.IBinder)) continue;

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
                            // Проверяем, является ли это вызов nameof
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
                
                asBinderMembers.Add(new AsBinderMemberInView(member, argumentType, arguments));
            }
            else if (type.HasInterface(Classes.IView))
            {
                asBinderMembers.Add(new AsBinderMemberInView(member, Classes.ViewBinder.Global, null));
            }
            else if (type.HasInterface(Classes.IBinder))
            {
                switch (member)
                {
                    case Field field:
                        if (field.Name.EndsWith(">k__BackingField")) continue;
                        
                        otherMembers.Add(new BinderFieldInView(field));
                        break;
                    case Property property:
                        var symbols = GetPropertyReturnSymbols(property, semanticModel);
                        
                        if (symbols is null) continue;
                        if (symbols.Any(symbol => symbol is Field or Property && !symbol.HasAttribute(Classes.IgnoreAttribute))) continue;

                        propertyMembers.Add(new PropertyBinderInView(property));
                        break;
                }
            }
        }

        return new ViewMembers(otherMembers, propertyMembers, asBinderMembers);

        ITypeSymbol? GetType(ISymbol member) => member switch
        {
            Field field => field.Type is IArrayTypeSymbol arrayTypeSymbol
                ? arrayTypeSymbol.ElementType
                : field.Type,

            Property property => property.Type is IArrayTypeSymbol arrayTypeSymbol
                ? arrayTypeSymbol.ElementType
                : property.Type,

            _ => null
        };
    }

    private static IReadOnlyList<ISymbol?>? GetPropertyReturnSymbols(Property property, SemanticModel semanticModel)
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

        ISymbol? GetSymbol(IdentifierNameSyntax node) => semanticModel.GetSymbolInfo(node).Symbol;
    }

    private static void RecognizeOverriddenMethods(
        Inheritor inheritor,
        IEnumerable<IMethodSymbol> methods,
        out bool isInitializeOverride,
        out bool isDeinitializeOverride)
    {
        isInitializeOverride = false;
        isDeinitializeOverride = false;
        if (inheritor is Inheritor.None or Inheritor.HasInterface) return;

        foreach (var method in methods)
        {
            if (!isInitializeOverride && HasOverrideInitializeInternalMethod(method))
            {
                isInitializeOverride = true;
            }
            else if (!isDeinitializeOverride && HasOverrideDeinitializeInternalMethod(method))
            {
                isDeinitializeOverride = true;
            }
            else continue;
            
            if (isInitializeOverride && isDeinitializeOverride) return;
        }

        return;

        bool HasOverrideInitializeInternalMethod(IMethodSymbol method) =>
            HasOverrideMethod(method, "InitializeInternal", Classes.IViewModel.FullName);

        bool HasOverrideDeinitializeInternalMethod(IMethodSymbol method) =>
            HasOverrideMethod(method, "DeinitializeInternal");

        bool HasOverrideMethod(IMethodSymbol method, string methodName, params string[] parameters)
        {
            if (!method.IsOverride) return false;
            if (method.Parameters.Length != parameters.Length) return false;
            if (method.DeclaredAccessibility != Accessibility.Protected) return false;

            if (method.Name != methodName) return false;
            if (method.ReturnType.ToDisplayString() != "void") return false;

            for (var i = 0; i < parameters.Length; i++)
            {
                if (method.Parameters[i].Type.ToDisplayString() != parameters[i]) return false;
            }

            return true;
        }
    }
}