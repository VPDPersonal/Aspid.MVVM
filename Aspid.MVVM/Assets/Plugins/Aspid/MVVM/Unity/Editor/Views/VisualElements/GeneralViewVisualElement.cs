#nullable enable
using System.CodeDom.Compiler;
using Aspid.UnityFastTools;
using Aspid.MVVM.StarterKit;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public class GeneralViewVisualElement : MonoViewVisualElement<GeneralView, GeneralViewEditor>
    {
        protected override IEnumerable<string> PropertiesExcluding
        {
            get
            {
                foreach (var property in base.PropertiesExcluding)
                    yield return property;
                
                yield return "_designViewModel";
                yield return "_bindersList";
            }
        }
        
        public GeneralViewVisualElement(GeneralViewEditor editor) 
            : base(editor) { }

        protected override VisualElement? OnBuiltHeader()
        {
            var container = new AspidContainer();
            container.AddChild(new AspidPropertyField(Editor.DesignViewModel));

            if (Editor.DesignViewModel.objectReferenceValue is null)
            {
                Editor.BindersList.arraySize = 0;
                Editor.BindersList.serializedObject.ApplyModifiedProperties();
                return container;
            }

            if (Editor.BindersList.arraySize is 0)
            {
                var type = (Editor.DesignViewModel.objectReferenceValue as MonoScript)?.GetClass();
                if (type is null) return container;
                
                var fields = type.GetFieldInfosIncludingBaseClasses(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                    .Where(field => field.IsDefined(typeof(BaseBindAttribute)))
                    .Where(field => !field.IsDefined(typeof(GeneratedCodeAttribute)))
                    .ToArray();

                var methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                    .Where(method => method.IsDefined(typeof(RelayCommandAttribute)))
                    .ToArray();

                Editor.BindersList.arraySize = fields.Length + methods.Length;
                Editor.BindersList.serializedObject.ApplyModifiedProperties();
                if (Editor.BindersList.arraySize is 0) return container;
                
                for (var i = 0; i < fields.Length; i++)
                {
                    var element = Editor.BindersList.GetArrayElementAtIndex(i);
                    var assemblyQualifiedName = element.FindPropertyRelative("_assemblyQualifiedName");
                    var id =  element.FindPropertyRelative("_name");
                    var monoBinders = element.FindPropertyRelative("_monoBinders");

                    assemblyQualifiedName.stringValue = fields[i].FieldType.AssemblyQualifiedName;
                    id.stringValue = fields[i].GetGeneratedPropertyName();
                }
                
                for (var i = 0; i < methods.Length; i++)
                {
                    var property = type.GetProperty(methods[i].Name + "Command", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    if (property is null) continue;
                    
                    var element = Editor.BindersList.GetArrayElementAtIndex(fields.Length + i);
                    var assemblyQualifiedName = element.FindPropertyRelative("_assemblyQualifiedName");
                    var id =  element.FindPropertyRelative("_name");
                    var monoBinders = element.FindPropertyRelative("_monoBinders");

                    assemblyQualifiedName.stringValue = property.PropertyType.AssemblyQualifiedName;//methods[fields.Length + i].FieldType.AssemblyQualifiedName;
                    id.stringValue = property.Name;
                }
                
                Editor.BindersList.serializedObject.ApplyModifiedProperties();
            }
            
            for (var i = 0; i < Editor.BindersList.arraySize; i++)
            {
                var element = Editor.BindersList.GetArrayElementAtIndex(i);
                var assemblyQualifiedName = element.FindPropertyRelative("_assemblyQualifiedName");
                var id =  element.FindPropertyRelative("_name");
                var monoBinders = element.FindPropertyRelative("_monoBinders");

                var property = new AspidPropertyField(monoBinders, id.stringValue); 
                property.SetMargin(top: 3);
                
                container.AddChild(property);
            }
            
            return container;
        }
    }
}