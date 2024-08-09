using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Generators.Views.Body;
using MVVMGenerators.Generators.Views.Data;
using MVVMGenerators.Helpers.Extensions.Writer;
using MVVMGenerators.Helpers.Extensions.Declarations;

namespace MVVMGenerators.Generators.Views;

public partial class ViewGenerator
{
    private static void GenerateCode(SourceProductionContext context, ViewData data)
    {
        var declaration = data.Declaration;
        var namespaceName = declaration.GetNamespaceName();
        var declarationText = declaration.GetDeclarationText();
        
        GenerateIView(context, namespaceName, declarationText, data);
        GenerateAsBinder(context, namespaceName, declarationText, data);
        IdBodyGenerator.GenerateViewId(context, declarationText, namespaceName, data);
    }

    private static void GenerateAsBinder(SourceProductionContext context, string namespaceName,
        DeclarationText declarationText, ViewData data)
    {
        if (data.AsBinderFields.Length + data.AsBinderProperties.Length == 0) return;
        
        var code = new CodeWriter();
        string[]? baseTypes = null;
        if (data.Inheritor == Inheritor.None)
            baseTypes = [Classes.IView.Global];

        code.AppendClass(namespaceName, declarationText,
            body: () => code.AppendViewBinderFields(data),
            baseTypes);
            
        context.AddSource(declarationText.GetFileName(namespaceName, "AsBinder"), code.GetSourceText());
    }
    
    private static void GenerateIView(SourceProductionContext context, string namespaceName,
        DeclarationText declarationText, ViewData viewData)
    {
        if (viewData.Inheritor == Inheritor.OverrideMonoView) return;
        
        var code = new CodeWriter();
        string[]? baseTypes = null;
        if (viewData.Inheritor == Inheritor.None)
            baseTypes = [Classes.IView.Global];

        code.AppendClass(namespaceName, declarationText,
            body: () => code.AppendIView(viewData),
            baseTypes);
            
        context.AddSource(declarationText.GetFileName(namespaceName, "IView"), code.GetSourceText());
    }
}