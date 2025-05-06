using Microsoft.CodeAnalysis;
using MVVMGenerators.Generators.Views.Body;
using MVVMGenerators.Generators.Views.Data;
using MVVMGenerators.Helpers.Extensions.Declarations;

namespace MVVMGenerators.Generators.Views;

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
    }
}