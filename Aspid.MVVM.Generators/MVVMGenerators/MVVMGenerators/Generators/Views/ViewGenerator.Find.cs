using System.Threading;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using System.Collections.Generic;
using System.Collections.Immutable;
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
    private static FoundForGenerator<ViewData> FindView(GeneratorAttributeSyntaxContext context, CancellationToken cancellationToken)
    {
        if (context.TargetSymbol is not INamedTypeSymbol symbol) return default;
        
        var inheritor = RecognizeInheritor(symbol);
        var members = symbol.GetMembers();
        var viewMembers = GetViewMembers(members);
        
        var isInitializeOverride = false;
        var isDeinitializeOverride = false;

        if (inheritor != Inheritor.None && inheritor != Inheritor.HasInterface)
        {
            foreach (var method in members.OfType<IMethodSymbol>())
            {
                if (isInitializeOverride && isDeinitializeOverride) break;

                if (!isInitializeOverride && HasOverrideViewIternalMethod(method, "InitializeIternal"))
                {
                    isInitializeOverride = true;
                    continue;
                }

                if (!isDeinitializeOverride && HasOverrideViewIternalMethod(method, "DeinitializeIternal"))
                {
                    isDeinitializeOverride = true;
                }
            }
        }

        Debug.Assert(context.TargetNode is TypeDeclarationSyntax);
        var candidate = Unsafe.As<TypeDeclarationSyntax>(context.TargetNode);
        
        var viewData = new ViewData(inheritor, viewMembers, isInitializeOverride, isDeinitializeOverride, candidate);
        return new FoundForGenerator<ViewData>(true, viewData);
        
        bool HasOverrideViewIternalMethod(IMethodSymbol method, string methodName)
        {
            if (!method.IsOverride) return false;
            if (method.DeclaredAccessibility != Accessibility.Protected) return false;
            if (method.Parameters.Length != 1) return false;
            
            if (method.Name != methodName) return false;
            if (method.Parameters[0].Type.ToDisplayString() != Classes.IViewModel.FullName) return false;
            if (method.ReturnType.ToDisplayString() != "void") return false;

            return true;
        }
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
                    if (!argumentType.HasInterface(Classes.IBinder)) continue;
                    
                    asBinderMembers.Add(new AsBinderMember(member, argumentType));
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

    private static Inheritor RecognizeInheritor(INamedTypeSymbol symbol)
    {
        if (symbol.BaseType?.HasAttribute(Classes.ViewAttribute) ?? false) return Inheritor.InheritorViewAttribute;
        if (symbol.HasBaseType(Classes.MonoView)) return Inheritor.InheritorMonoView;
        return symbol.HasInterface(Classes.IView) ? Inheritor.HasInterface : Inheritor.None;
    }
}