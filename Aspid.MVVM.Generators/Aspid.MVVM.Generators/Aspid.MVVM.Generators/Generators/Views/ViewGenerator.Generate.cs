using Microsoft.CodeAnalysis;
using Aspid.MVVM.Generators.Helpers;
using Aspid.MVVM.Generators.Shared;
using Aspid.Generators.Helper;
using Aspid.MVVM.Generators.Generators.Views.Body;
using Aspid.MVVM.Generators.Generators.Views.Data;

namespace Aspid.MVVM.Generators.Generators.Views;

public partial class ViewGenerator
{
    private static void GenerateCode(SourceProductionContext context, ViewData data)
    {
        var dataSpan = new ViewDataSpan(data);
        
        var declarationText = new AspidTypeDeclaration(dataSpan.Symbol);
        
        InitializeBody.Generate(dataSpan, declarationText, context);
        BinderCachedBody.Generate(dataSpan, declarationText, context);
        BinderFieldsBody.Generate(dataSpan, declarationText, context);
        GenericInitializeView.Generate(dataSpan, declarationText, context);
    }
}