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
        var dataSpan = new ViewDataSpan(data);
        
        var declaration = dataSpan.Declaration;
        var @namespace = declaration.GetNamespaceName();
        var declarationText = declaration.GetDeclarationText();
        
        GenerateIView(@namespace, dataSpan, declarationText, context);
        GenerateAsBinder(@namespace, dataSpan, declarationText, context);
        IdBodyGenerator.GenerateViewId(@namespace, dataSpan, declarationText, context);
    }

    private static void GenerateAsBinder(
        string @namespace,
        in ViewDataSpan data,
        DeclarationText declaration,
        SourceProductionContext context)
    {
        if (data.AsBinderMembers.Length + data.ViewProperties.Length == 0) return;
        
        var code = new CodeWriter();
        var baseTypes = GetBaseTypes(data);

        code.AppendClassBegin(@namespace, declaration, baseTypes)
            .AppendCachedBinders(data)
            .AppendClassEnd(@namespace);
            
        context.AddSource(declaration.GetFileName(@namespace, "CachedBinders"), code.GetSourceText());
    }
    
    private static void GenerateIView(
        string @namespace,
        in ViewDataSpan data,
        DeclarationText declaration,
        SourceProductionContext context)
    {
        if (data.IsInitializeOverride 
            && data.IsDeinitializeOverride
            && data.ViewProperties.Length + data.AsBinderMembers.Length == 0) return;
        
        var code = new CodeWriter();
        var baseTypes = GetBaseTypes(data);

        code.AppendClassBegin([Namespaces.Aspid_MVVM], @namespace, declaration, baseTypes)
            .AppendIView(data)
            .AppendClassEnd(@namespace);
        
        context.AddSource(declaration.GetFileName(@namespace, "IView"), code.GetSourceText());
    }

    private static string[]? GetBaseTypes(in ViewDataSpan data) =>
        data.Inheritor == Inheritor.None ? [Classes.IView.Global] : null;
}