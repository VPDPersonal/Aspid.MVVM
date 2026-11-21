using System.Linq;
using Microsoft.CodeAnalysis;
using Aspid.Generators.Helper;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Aspid.MVVM.Generators.Generators.Ids.Data;
using Aspid.MVVM.Generators.Generators.Views.Helpers;
using Aspid.MVVM.Generators.Generators.Views.Data.Members;
using Classes = Aspid.MVVM.Generators.Generators.Descriptions.Classes;

namespace Aspid.MVVM.Generators.Generators.Views.Factories;

public static class BinderMembersFactory
{
    /// <summary>
    /// Collects the binder ids every base View already owns — the members it declares and the binder fields the
    /// generator emits for it — so a derived View does not emit a field Unity would see serialized twice.
    /// </summary>
    public static ImmutableArray<string> CollectInheritedIds(
        INamedTypeSymbol symbol,
        TypeDeclarationSyntax declaration,
        SemanticModel semanticModel)
    {
        var ids = new HashSet<string>();

        for (var baseType = symbol.BaseType; baseType is not null; baseType = baseType.BaseType)
        {
            if (!baseType.HasAnyAttributeInSelf(Classes.ViewAttribute)) continue;

            foreach (var member in Create(baseType, semanticModel))
                ids.Add(member.Id.SourceValue);

            CollectIgnoredIds(baseType, ids);
            VirtualBinderFields.CollectGeneratedIds(baseType, semanticModel.Compilation, declaration, ids);
        }

        return ids.ToImmutableArray();
    }

    /// <summary>
    /// Adds the ids of the members marked <c>[IgnoreBind]</c>: the View binds them by hand, so they are left out of
    /// the generated binding but still count as declared, and no auto binder field is emitted under the same name.
    /// </summary>
    public static void CollectIgnoredIds(INamedTypeSymbol symbol, ISet<string> ids)
    {
        foreach (var member in symbol.GetMembers())
        {
            if (member.HasAnyAttributeInSelf(Classes.IgnoreAttribute))
                ids.Add(new IdData(member).SourceValue);
        }
    }

    public static ImmutableArray<BinderMember> Create(INamedTypeSymbol symbolClass, SemanticModel semanticModel) =>
        Create(symbolClass, semanticModel, out _);

    public static ImmutableArray<BinderMember> Create(
        INamedTypeSymbol symbolClass,
        SemanticModel semanticModel,
        out ImmutableArray<Diagnostic> diagnostics)
    {
        var errors = ImmutableArray.CreateBuilder<Diagnostic>();
        var binderMembers = new List<BinderMember>();
        
        foreach (var member in symbolClass.GetMembers())
        {
            if (member.HasAnyAttributeInSelf(Classes.IgnoreAttribute)) continue;

            var type = GetType(member);
            if (type is null) continue;

            if (member.TryGetAnyAttributeInSelf(out var asBinderAttribute, Classes.AsBinderAttribute))
            {
                var binder = AsBinderFactory.Create(member, type, asBinderAttribute!, semanticModel, out var diagnostic);
                if (binder is not null)
                    binderMembers.Add(binder);
                if (diagnostic is not null)
                    errors.Add(diagnostic);
            }
            else if (type.HasAnyAttributeInSelf(Classes.ViewAttribute) || type.HasAnyInterfaceInSelfAndBases(Classes.IView))
            {
                binderMembers.Add(new AsBinderMember(member, Classes.ViewBinder, null));
            }
            else if (IsBinder(type))
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

        diagnostics = errors.ToImmutable();
        return binderMembers.ToImmutableArray();
    }
    
    /// <summary>
    /// A member is a binder when its type implements <c>IBinder</c> or is the <c>IBinder</c> interface itself —
    /// the helper looks only through the implemented interfaces, so the bare interface has to be matched by name,
    /// without the nullable annotation an <c>IBinder?</c> member carries.
    /// </summary>
    private static bool IsBinder(ITypeSymbol type) =>
        type.HasAnyInterfaceInSelfAndBases(Classes.IBinder)
        || type.WithNullableAnnotation(NullableAnnotation.NotAnnotated).ToDisplayString() == Classes.IBinder.FullName;
    
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