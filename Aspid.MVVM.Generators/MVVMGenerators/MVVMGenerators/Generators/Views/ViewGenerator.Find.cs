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
using MVVMGenerators.Helpers.Extensions.Declarations;

using Field = Microsoft.CodeAnalysis.IFieldSymbol;
using Property = Microsoft.CodeAnalysis.IPropertySymbol;

namespace MVVMGenerators.Generators.Views;

public partial class ViewGenerator
{
    private static FoundForGenerator<ViewData> FindView(GeneratorSyntaxContext context, 
        CancellationToken cancellationToken)
    {
        Debug.Assert(context.Node is TypeDeclarationSyntax);
        var candidate = Unsafe.As<TypeDeclarationSyntax>(context.Node);
        if (!candidate.HasAttribute(context.SemanticModel, Classes.ViewAttribute.FullName)) return default;

        var symbol = context.SemanticModel.GetDeclaredSymbol(candidate, cancellationToken);
        if (symbol is null) return default;

        var symbolMembers = symbol.GetMembers();
        
        var members = GetMembers(symbolMembers);
        var inheritor = RecognizeInheritor(symbol, symbolMembers);

        return new FoundForGenerator<ViewData>(true, new ViewData(
            inheritor,
            candidate,
            members.ViewFields,
            members.BinderFields,
            members.AsBinderFields,
            members.ViewProperties,
            members.BinderProperties,
            members.AsBinderProperties));
    }

    private static Members GetMembers(ImmutableArray<ISymbol> members)
    {
        List<Field> viewFields = [];
        List<Field> binderFields = [];
        List<AsBinderMember<Field>> asBinderFields = [];
        
        List<Property> viewProperties = [];
        List<Property> binderProperties = [];
        List<AsBinderMember<Property>> asBinderProperties = [];
        
        foreach (var member in members)
        {
            switch (member)
            {
                case Field field:
                    AddToList(field,viewFields, binderFields, asBinderFields);
                    break;
                
                case Property property:
                    AddToList(property,viewProperties, binderProperties, asBinderProperties);
                    break;
            }
        }

        return new Members(
            viewFields,
            binderFields,
            asBinderFields,
            viewProperties,
            binderProperties,
            asBinderProperties);

        void AddToList<T>(T member, IList<T> viewList, IList<T> binderList, List<AsBinderMember<T>> list)
            where T : ISymbol
        {
            if (member.HasAttribute(Classes.AsBinderAttribute, out var attribute))
            {
                if (attribute?.ConstructorArguments[0].Value is INamedTypeSymbol type 
                    && type.HasInterface(Classes.IBinder))
                {
                    list.Add(new AsBinderMember<T>(member, type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)));
                }
            }
            else
            {
                var type = GetType(member);
                
                if (type == null) return;
                if (!type.HasInterface(Classes.IBinder)) return;
            
                if (type.HasInterface(Classes.IView)) viewList.Add(member);
                else binderList.Add(member);
            }
        }
        
        ITypeSymbol? GetType<T>(T member)
            where T : ISymbol
        {
            return member switch
            {
                Field field => field.Type is IArrayTypeSymbol arrayTypeSymbol ? arrayTypeSymbol.ElementType : field.Type,
                Property property => property.Type is IArrayTypeSymbol arrayTypeSymbol ? arrayTypeSymbol.ElementType : property.Type,
                _ => null
            };
        }
    }

    private static Inheritor RecognizeInheritor(INamedTypeSymbol symbol, ImmutableArray<ISymbol> members)
    {
        if (HasViewModeAttributeInBase())
            return Inheritor.InheritorViewAttribute;
        
        if (symbol.HasBaseType(Classes.MonoView))
        {
            return members.OfType<IMethodSymbol>().Any(HasOverrideInitializeIternal)
                ? Inheritor.OverrideMonoView
                : Inheritor.InheritorMonoView;
        }
        
        return symbol.HasInterface(Classes.IView) ? Inheritor.HasInterface : Inheritor.None;

        // TODO Delete?
        bool HasViewModeAttributeInBase()
        {
            for (var type = symbol.BaseType; type != null; type = type.BaseType)
            {
                if (!type.HasAttribute(Classes.ViewAttribute.FullName)) continue;
                return true;
            }

            return false;
        }
        
        bool HasOverrideInitializeIternal(IMethodSymbol method)
        {
            if (!method.IsOverride) return false;
            if (method.DeclaredAccessibility != Accessibility.Protected) return false;
            if (method.Parameters.Length != 1) return false;
            
            if (method.Name != "InitializeIternal") return false;
            if (method.Parameters[0].Type.ToDisplayString() != Classes.IViewModel.FullName) return false;
            if (method.ReturnType.ToDisplayString() != "void") return false;

            return true;
        }
    }
    
    private readonly ref struct Members(
        IReadOnlyList<Field> viewFields,
        IReadOnlyList<Field> binderFields,
        IReadOnlyList<AsBinderMember<Field>> asBinderFields,
        IReadOnlyList<Property> viewProperties,
        IReadOnlyList<Property> binderProperties,
        IReadOnlyList<AsBinderMember<Property>> asBinderProperties)
    {
        public readonly Field[] ViewFields = viewFields.ToArray();
        public readonly Field[] BinderFields = binderFields.ToArray();
        public readonly AsBinderMember<Field>[] AsBinderFields = asBinderFields.ToArray();
        
        public readonly Property[] ViewProperties = viewProperties.ToArray();
        public readonly Property[] BinderProperties = binderProperties.ToArray();
        public readonly AsBinderMember<Property>[] AsBinderProperties = asBinderProperties.ToArray();
    }
}