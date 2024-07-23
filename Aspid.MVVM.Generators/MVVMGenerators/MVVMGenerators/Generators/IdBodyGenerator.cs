using System;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using System.Collections.Generic;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Helpers.Extensions.Writer;
using MVVMGenerators.Helpers.Extensions.Symbols;

namespace MVVMGenerators.Generators;

public static class IdBodyGenerator
{
    public static void GenerateViewId(
        SourceProductionContext context, string namespaceName, 
        DeclarationText declarationText, IReadOnlyCollection<IFieldSymbol> fields)
    {
        Generate(General.GeneratedCodeViewAttribute, field => $"\"{field.GetPropertyName()}\"", 
            context, namespaceName, declarationText, fields);
        
        // Generation Example
        // private const string MyNameId = "MyName";
    }

    public static void GenerateViewModelId(
        SourceProductionContext context, string namespaceName, 
        DeclarationText declarationText, IReadOnlyCollection<IFieldSymbol> fields)
    {
        Generate(General.GeneratedCodeViewModelAttribute, field => $"nameof({field.GetPropertyName()})", 
            context, namespaceName, declarationText, fields);
        
        // Generation Example
        // private const string MyNameId = nameof(MyName);
    }

    private static void Generate(
        string generatorAttribute, Func<IFieldSymbol, string> value,
        SourceProductionContext context, string namespaceName, 
        DeclarationText declarationText, IReadOnlyCollection<IFieldSymbol> fields)
    {
        var code = new CodeWriter();
        code.AppendClass(namespaceName, declarationText,
            body: () =>
            {
                code.AppendLoop(fields, field =>
                {
                    var propertyName = field.GetPropertyName();
                    code.AppendLine(generatorAttribute)
                        .AppendLine($"private const string {propertyName}Id = {value(field)};");
                });
            });
        
        context.AddSource(declarationText.GetFileName("Id.Generated"), code.GetSourceText());

        #region Generation Example
        /*  namespace MyNamespace
         *  {
         *  |   public partial class MyClassName
         *  |   {
         *  |   |   For View
         *  |   |   private const string MyNameId = "MyName";
         *  |   |   Other constants
         *  |   |
         *  |   |   For ViewModel
         *  |   |   private const string MyNameId = nameof(MyName);
         *  |   |   Other constants
         *  |   }
         *  }
         */
        #endregion
    }
}