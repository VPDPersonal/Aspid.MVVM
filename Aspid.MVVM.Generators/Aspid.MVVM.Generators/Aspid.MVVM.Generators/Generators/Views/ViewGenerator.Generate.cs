using Microsoft.CodeAnalysis;
using Aspid.Generator.Helpers;
using Aspid.MVVM.Generators.Views.Body;
using Aspid.MVVM.Generators.Views.Data;

namespace Aspid.MVVM.Generators.Views;

public partial class ViewGenerator
{
    private static void GenerateCode(SourceProductionContext context, ViewData data)
    {
        var dataSpan = new ViewDataSpan(data);
        
        var declaration = dataSpan.Declaration;
        var @namespace = declaration.GetNamespaceName();
        var declarationText = declaration.GetDeclarationText();
        
        InitializeBody.Generate(@namespace, dataSpan, declarationText, context);
        BinderCachedBody.Generate(@namespace, dataSpan, declarationText, context);
        GenericInitializeView.Generate(@namespace, dataSpan, declarationText, context);
    }
}