using System.Linq;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using System.Collections.Generic;
using MVVMGenerators.Generators.ViewModels.Data;
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
        var capacity = data.FieldMembers.Length + data.PropertyMembers.Length + data.AsBinderMembers.Length;
        var propertyNames = new List<(string, string)>(capacity);
        
        propertyNames.AddRange(data.FieldMembers.Select(member => (FieldSymbolExtensions.GetPropertyNameFromFieldName(member.Name), member.Id)));
        propertyNames.AddRange(data.PropertyMembers.Select(member => (member.Name, member.Id)));
        propertyNames.AddRange(data.AsBinderMembers.Select(member => (FieldSymbolExtensions.GetPropertyNameFromFieldName(member.Name), member.Id)));

        const bool isNameOf = false;
        Generate(context, declarationText, namespaceName, General.GeneratedCodeViewAttribute, propertyNames, isNameOf);
        
        // Generation Example
        // private const string MyNameId = "MyName";
    }

    public static void GenerateViewModelId(
        SourceProductionContext context, 
        DeclarationText declarationText, 
        string namespaceName, IEnumerable<FieldData> fields)
    {
        var propertyNames = new List<(string, string)>(fields.Select(field =>
        {
            var name = field.PropertyName;
            return (name, $"{name}Id");
        }));
        
        const bool isNameOf = true;
        Generate(context, declarationText, namespaceName, General.GeneratedCodeViewAttribute, propertyNames, isNameOf);
        
        // Generation Example
        // private const string MyNameId = nameof(MyName);
    }
    
    
    private static void Generate(
        SourceProductionContext context,
        DeclarationText declarationText,
        string namespaceName, string generatorAttribute,
        IEnumerable<(string, string)> names, bool isNameOf)
    {
        var code = new CodeWriter();

        code.AppendClass(namespaceName, declarationText,
            body: () =>
            {
                code.AppendLoop(names, (tuple) =>
                {
                    var value = isNameOf ? $"nameof({tuple.Item1})" : $"\"{tuple.Item1}\"";

                    code.AppendLine(generatorAttribute)
                        .AppendLine($"private const string {tuple.Item2} = {value};");
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