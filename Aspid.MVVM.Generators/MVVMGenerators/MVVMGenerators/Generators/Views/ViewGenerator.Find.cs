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
using MVVMGenerators.Helpers.Extensions.Declarations;

using Field = Microsoft.CodeAnalysis.IFieldSymbol;
using Property = Microsoft.CodeAnalysis.IPropertySymbol;

namespace MVVMGenerators.Generators.Views;

public partial class ViewGenerator
{
    private static FoundForGenerator<ViewData> FindView(
        GeneratorSyntaxContext context, 
        CancellationToken cancellationToken)
    {
        Debug.Assert(context.Node is TypeDeclarationSyntax);
        var candidate = Unsafe.As<TypeDeclarationSyntax>(context.Node);
        if (!candidate.HasAttribute(context.SemanticModel, Classes.ViewAttribute.FullName)) return default;

        var symbol = context.SemanticModel.GetDeclaredSymbol(candidate, cancellationToken);
        if (symbol is null) return default;

        var members = symbol.GetMembers();
        var inheritor = RecognizeInheritor(symbol, members);
        var viewMembers = GetViewMembers(members);

        var viewData = new ViewData(inheritor, viewMembers, candidate);
        return new FoundForGenerator<ViewData>(true, viewData);
    }

    private static MembersContainer GetViewMembers(ImmutableArray<ISymbol> members)
    {
        List<FieldMember> otherMembers = [];
        List<PropertyMember> propertyMembers = [];
        List<AsBinderMember> asBinderMembers = [];
        
        foreach (var member in members)
        {
            var type = member switch
            {
                Field field => field.Type is IArrayTypeSymbol arrayTypeSymbol 
                    ? arrayTypeSymbol.ElementType 
                    : field.Type,
                
                Property property => property.Type is IArrayTypeSymbol arrayTypeSymbol 
                    ? arrayTypeSymbol.ElementType 
                    : property.Type,
                
                _ => null
            };
            
            if (type == null) continue;

            if (member.HasAttribute(Classes.AsBinderAttribute, out var asBinderAttribute) ||
                type.HasInterface(Classes.IView))
            {
                if (asBinderAttribute != null)
                {
                    if (asBinderAttribute.ConstructorArguments[0].Value is not INamedTypeSymbol argumentType) continue;
                    if (argumentType.IsAbstract) continue;
                    if (!type.HasInterface(Classes.IBinder)) continue;
                    
                    asBinderMembers.Add(new AsBinderMember(member, type));
                }
                else
                {
                    asBinderMembers.Add(new AsBinderMember(member, Classes.ViewBinder.Global));
                }
            }
            else if (type.HasInterface(Classes.IBinder))
            {
                switch (member)
                {
                    case Field field: otherMembers.Add(new FieldMember(field)); break;
                    case Property property: propertyMembers.Add(new PropertyMember(property)); break;
                }
            }
        }
        
        return new MembersContainer(otherMembers, propertyMembers, asBinderMembers);
    }

    private static Inheritor RecognizeInheritor(INamedTypeSymbol symbol, ImmutableArray<ISymbol> members)
    {
        if (symbol.BaseType?.HasAttribute(Classes.ViewAttribute) ?? false) 
            return Inheritor.InheritorViewAttribute;
        
        if (symbol.HasBaseType(Classes.MonoView))
        {
            return members.OfType<IMethodSymbol>().Any(HasOverrideInitializeIternal)
                ? Inheritor.OverrideMonoView
                : Inheritor.InheritorMonoView;
        }
        
        return symbol.HasInterface(Classes.IView) ? Inheritor.HasInterface : Inheritor.None;
        
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
}