using Microsoft.CodeAnalysis;
using MVVMGenerators.Generators.ViewModels.Body;
using MVVMGenerators.Generators.ViewModels.Data;
using MVVMGenerators.Helpers.Extensions.Declarations;

namespace MVVMGenerators.Generators.ViewModels;

public partial class ViewModelGenerator
{
    private static void GenerateCode(SourceProductionContext context, ViewModelData data)
    {
        var dataSpan = new ViewModelDataSpan(data);
        var declaration = dataSpan.Declaration;
        var @namespace = declaration.GetNamespaceName();
        var declarationText = declaration.GetDeclarationText();

        PropertiesBody.Generate(@namespace, dataSpan, declarationText, context);
        RelayCommandBody.Generate(@namespace, dataSpan, declarationText, context);
        FindBindableMembersBody.Generate(@namespace, dataSpan, declarationText, context);
        BindableMembersBody.Generate(@namespace, dataSpan, declarationText, context);
    }
}