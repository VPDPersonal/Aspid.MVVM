using System.Linq;
using System.Reflection;
using UnityEngine.UIElements;
using System.CodeDom.Compiler;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugViewModelField : DebugCompositeField
    {
        public DebugViewModelField(string label, IFieldContext context) 
            : base(label, context) { }

        protected override void BuildContent(VisualElement content)
        {
            var type = Value.GetType();
            var fields = type.GetFieldInfosIncludingBaseClasses(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic).ToArray();
         
            var fieldsByGroup = new[]
            {
                new List<FieldInfo>(),
                new List<FieldInfo>(),
                new List<FieldInfo>(),
            };

            foreach (var field in fields)
            {
                if (field.FieldType.IsRelayCommandType())
                {
                    fieldsByGroup[1].Add(field);
                }
                else if (field.IsDefined(typeof(BaseBindAttribute)))
                {
                    fieldsByGroup[0].Add(field);
                }
                else if (!field.IsDefined(typeof(GeneratedCodeAttribute)))
                {
                    fieldsByGroup[2].Add(field);
                }
            }

            foreach (var group in fieldsByGroup)
            {
                foreach (var field in group)
                {
                    BuildDebugField(content, field);
                }
            }
        }
    }
}
