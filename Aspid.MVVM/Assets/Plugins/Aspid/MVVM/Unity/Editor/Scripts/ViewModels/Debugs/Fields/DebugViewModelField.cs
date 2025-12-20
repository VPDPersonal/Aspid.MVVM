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
        private const BindingFlags BindingAttr = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic;
        
        public DebugViewModelField(string label, IFieldContext context) 
            : base(label, context) { }

        protected override void BuildContent(VisualElement content)
        {
            var type = Value.GetType();
            var fields = type.GetFieldInfosIncludingBaseClasses(BindingAttr).ToArray();
            
            var backingFieldNames = new HashSet<string>();
            
            var bindProperties = new List<PropertyInfo>();
            var autoProperties = new List<PropertyInfo>();
            
            foreach (var property in type.GetPropertyInfosIncludingBaseClasses(BindingAttr, type))
            {
                if (property.IsDefined(typeof(BaseBindAttribute)))
                {
                    bindProperties.Add(property);
                    
                    if (NameHelper.TryGetBackingFieldName(property, type, out var backingFieldName))
                        backingFieldNames.Add(backingFieldName);
                }
                else if (NameHelper.IsAutoProperty(property, type))
                {
                    autoProperties.Add(property);
                    
                    var backingFieldName = NameHelper.GetAutoPropertyBackingFieldName(property.Name);
                    backingFieldNames.Add(backingFieldName);
                }
            }
         
            var fieldsByGroup = new[]
            {
                new List<MemberInfo>(),
                new List<MemberInfo>(),
                new List<MemberInfo>(),
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
                else if (!field.IsDefined(typeof(GeneratedCodeAttribute)) && !backingFieldNames.Contains(field.Name))
                {
                    fieldsByGroup[2].Add(field);
                }
            }
            
            foreach (var property in bindProperties)
                fieldsByGroup[0].Add(property);
            
            foreach (var property in autoProperties)
                fieldsByGroup[2].Add(property);

            foreach (var group in fieldsByGroup)
            {
                foreach (var member in group)
                {
                    BuildDebugField(content, member);
                }
            }
        }
    }
}
