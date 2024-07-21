using System.Linq;
using System.Threading;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using System.Collections.Generic;
using MVVMGenerators.Descriptions;
using Microsoft.CodeAnalysis.CSharp;
using System.Runtime.CompilerServices;
using MVVMGenerators.Extensions.Symbols;
using MVVMGenerators.Helpers.Extensions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MVVMGenerators.Extensions.Declarations;

namespace MVVMGenerators.Generators.Views;

[Generator(LanguageNames.CSharp)]
public class ViewGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var provider = context.SyntaxProvider.CreateSyntaxProvider(SyntacticPredicate, FindViews)
            .Where(foundForSourceGenerator => foundForSourceGenerator.IsNeed)
            .Select((foundForSourceGenerator, _) => foundForSourceGenerator.Container);
        
        context.RegisterSourceOutput(
            source: provider,
            action: GenerateCode);
    }

    private static bool SyntacticPredicate(SyntaxNode node, CancellationToken cancellationToken)
    {
        var candidate = node switch
        {
            ClassDeclarationSyntax or StructDeclarationSyntax => node as TypeDeclarationSyntax,
            _ => null
        };

        return candidate is not null
            && candidate.Modifiers.Any(SyntaxKind.PartialKeyword)
            && !candidate.Modifiers.Any(SyntaxKind.StaticKeyword)
            && candidate.AttributeLists.Count > 0;
    }

    private static FoundForGenerator<ViewData> FindViews(GeneratorSyntaxContext context, 
        CancellationToken cancellationToken)
    {
        Debug.Assert(context.Node is TypeDeclarationSyntax);
        var candidate = Unsafe.As<TypeDeclarationSyntax>(context.Node);
        if (!candidate.HasAttribute(context.SemanticModel, Classes.ViewAttribute.FullName)) return default;

        var symbol = context.SemanticModel.GetDeclaredSymbol(candidate, cancellationToken);
        if (symbol is null) return default;

        var members = symbol.GetMembers();

        var binderFields = GetBinderFields().ToArray();
        if (!binderFields.Any()) return default;
        
        if (HasViewModeAttributeInBase())
            return new FoundForGenerator<ViewData>(true, 
                new ViewData(ViewInheritor.InheritorViewAttribute, candidate, binderFields));
        
        var inheritor = ViewInheritor.None;
        
        if (symbol.HasBaseType(Classes.MonoView))
        {
            inheritor = members.OfType<IMethodSymbol>().Any(HasOverrideInitializeIternal)
                ? ViewInheritor.OverrideMonoView
                : ViewInheritor.InheritorMonoView;
        }
        else if (symbol.HasInterface(Classes.IView))
        {
            inheritor = ViewInheritor.HasInterface;
        }

        return new FoundForGenerator<ViewData>(true, new ViewData(inheritor, candidate, binderFields));
        
        IEnumerable<IFieldSymbol> GetBinderFields()
        {
            foreach (var field in members.OfType<IFieldSymbol>())
            {
                if (field.Type is IArrayTypeSymbol arrayTypeSymbol 
                    && arrayTypeSymbol.ElementType.HasInterface(Classes.IBinder)
                    || field.Type.HasInterface(Classes.IBinder))
                {
                    yield return field;
                }
            }
        }

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
            if (method.Parameters.Length == 1) return false;

            if (method.Name != "InitializeIternal") return false;
            if (method.Parameters[0].ToDisplayString() != Classes.IViewModel.FullName) return false;
            if (method.ReturnType.ToDisplayString() != "void") return false;

            return true;
        }
    }

    private static void GenerateCode(SourceProductionContext context, ViewData viewData)
    {
        var declaration = viewData.Declaration;
        var namespaceName = declaration.GetNamespaceName();
        var declarationText = declaration.GetDeclarationText();
        var fields = viewData.Fields;

        GenerateCode(context, namespaceName, declarationText, viewData);
        IdBodyGenerator.GenerateViewId(context, namespaceName, declarationText, fields);
    }
    
    private static void GenerateCode(SourceProductionContext context, string namespaceName,
        DeclarationText declarationText, ViewData viewData)
    {
        var code = new CodeWriter();
        string[]? baseTypes = null;
        if (viewData.Inheritor == ViewInheritor.None)
            baseTypes = [Classes.IView.Global];
        
        code.AppendClass(namespaceName, declarationText,
            body: () => code.AppendIView(viewData),
            baseTypes);
            
        context.AddSource($"{declarationText.Name}.IView.Generated.cs", code.GetSourceText());
    }
}