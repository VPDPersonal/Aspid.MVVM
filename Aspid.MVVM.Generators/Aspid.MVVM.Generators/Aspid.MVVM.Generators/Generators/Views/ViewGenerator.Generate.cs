using Microsoft.CodeAnalysis;
using Aspid.Generators.Helper;
using Aspid.MVVM.Generators.Generators.Views.Body;
using Aspid.MVVM.Generators.Generators.Views.Data;

namespace Aspid.MVVM.Generators.Generators.Views;

public partial class ViewGenerator
{
    private static void GenerateCode(SourceProductionContext context, ViewData data)
    {
        var dataSpan = new ViewDataSpan(data);
        
        var declaration = dataSpan.Declaration;
        var @namespace = declaration.GetNamespaceName();
        var declarationText = new DeclarationText(declaration);
        
        InitializeBody.Generate(@namespace, dataSpan, declarationText, context);
        BinderCachedBody.Generate(@namespace, dataSpan, declarationText, context);
        GenericInitializeView.Generate(@namespace, dataSpan, declarationText, context);
    }
}