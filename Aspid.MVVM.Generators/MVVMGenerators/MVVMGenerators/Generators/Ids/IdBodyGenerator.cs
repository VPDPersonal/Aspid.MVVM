using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using System.Collections.Generic;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Generators.Views.Data;
using MVVMGenerators.Helpers.Extensions.Writer;

namespace MVVMGenerators.Generators.Ids;

public static class IdBodyGenerator
{
    public static void GenerateViewId(
        string @namespace,
        in ViewDataSpan data,
        DeclarationText declaration, 
        SourceProductionContext context)
    {
        const bool isNameOf = false;
        var capacity = data.FieldMembers.Length + data.ViewProperties.Length + data.AsBinderMembers.Length;
        var idList = new List<(string, string)>(capacity);

        foreach (var field in data.FieldMembers)
        {
            var id = field.Field.GetId(field.FieldName);
            idList.Add((id, field.Id));
        }
        
        foreach (var property in data.ViewProperties)
        {
            var id = property.Property.GetId(property.PropertyName);
            idList.Add((id, property.Id));
        }
        
        foreach (var member in data.AsBinderMembers)
        {
            var id = member.Member.GetId(member.Name);
            idList.Add((id, member.Id ?? member.Name + "Id"));
        }
        
        Generate(isNameOf, @namespace, General.GeneratedCodeViewAttribute, declaration, context, idList);
        
        // Generation Example
        // private const string MyNameId = "MyName";
    }

    public static void GenerateViewModelId(
        string @namespace,
        DeclarationText declaration, 
        SourceProductionContext context,
        IEnumerable<(string, string)> names)
    {
        const bool isNameOf = false;
        Generate(isNameOf, @namespace, General.GeneratedCodeViewAttribute, declaration, context, names);
        
        // Generation Example
        // private const string MyNameId = nameof(MyName);
    }

    private static void Generate(
        bool isNameOf,
        string @namespace, 
        string generatorAttribute,
        DeclarationText declaration,
        SourceProductionContext context,
        IEnumerable<(string, string)> names)
    {
        var code = new CodeWriter();

        code.AppendClass(@namespace, declaration,
            body: () =>
            {
                code.AppendLoop(names, (tuple) =>
                {
                    var value = isNameOf ? $"nameof({tuple.Item1})" : $"\"{tuple.Item1}\"";

                    code.AppendLine(generatorAttribute)
                        .AppendLine($"private const string {tuple.Item2} = {value};");
                });
            });
        
        context.AddSource(declaration.GetFileName(@namespace, "Id"), code.GetSourceText());
        
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