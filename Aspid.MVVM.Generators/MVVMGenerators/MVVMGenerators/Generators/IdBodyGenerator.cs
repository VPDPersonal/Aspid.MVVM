using System.Linq;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using System.Collections.Generic;
using MVVMGenerators.Generators.Views.Data;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Helpers.Extensions.Writer;
using MVVMGenerators.Helpers.Extensions.Symbols;

namespace MVVMGenerators.Generators;

public static class IdBodyGenerator
{
    public static void GenerateViewId(
        SourceProductionContext context, 
        DeclarationText declarationText, 
        string namespaceName, in ViewData data)
    {
        var capacity = data.ViewFields.Length
            + data.BinderFields.Length
            + data.ViewProperties.Length
            + data.BinderProperties.Length;
        
        var propertyNames = new List<string>(capacity);
        
        propertyNames.AddRange(data.ViewFields.Select(field => field.GetPropertyName()));
        propertyNames.AddRange(data.BinderFields.Select(field => field.GetPropertyName()));
        propertyNames.AddRange(data.AsBinderFields.Select(field 
            => field.Member.GetPropertyName()));
        
        propertyNames.AddRange(data.ViewProperties.Select(property => property.Name));
        propertyNames.AddRange(data.BinderProperties.Select(property => property.Name));
        propertyNames.AddRange(data.AsBinderProperties.Select(property
            => FieldSymbolExtensions.GetPropertyNameFromFieldName(property.Member.Name)));

        const bool isNameOf = false;
        Generate(context, declarationText, namespaceName, General.GeneratedCodeViewAttribute, propertyNames, isNameOf);
        
        // Generation Example
        // private const string MyNameId = "MyName";
    }

    public static void GenerateViewModelId(
        SourceProductionContext context, 
        DeclarationText declarationText, 
        string namespaceName, IEnumerable<IFieldSymbol> fields)
    {
        var propertyNames = new List<string>(fields.Select(field => field.GetPropertyName()));
        
        const bool isNameOf = true;
        Generate(context, declarationText, namespaceName, General.GeneratedCodeViewAttribute, propertyNames, isNameOf);
        
        // Generation Example
        // private const string MyNameId = nameof(MyName);
    }
    
    private static void Generate(
        SourceProductionContext context,
        DeclarationText declarationText,
        string namespaceName, string generatorAttribute,
        IEnumerable<string> propertyNames, bool isNameOf)
    {
        var code = new CodeWriter();

        code.AppendClass(namespaceName, declarationText,
            body: () =>
            {
                code.AppendLoop(propertyNames, propertyName =>
                {
                    var value = isNameOf ? $"nameof({propertyName})" : $"\"{propertyName}\"";

                    code.AppendLine(generatorAttribute)
                        .AppendLine($"private const string {propertyName}Id = {value};");
                });
            });
        
        context.AddSource(declarationText.GetFileName(namespaceName, "Id"), code.GetSourceText());
        
        #region Generation Example
        /*  namespace MyNamespace<
         *  {
         *  |   public partial class MyClassName
         *  |   {
         *  |   |   // Without nameof
         *  |   |   private const string MyNameId = "MyName";
         *  |   |   Other constants
         *  |   |
         *  |   |   // With nameof
         *  |   |   private const string MyNameId = nameof(MyName);
         *  |   |   Other constants
         *  |   }
         *  }
         */
        #endregion
    }
}