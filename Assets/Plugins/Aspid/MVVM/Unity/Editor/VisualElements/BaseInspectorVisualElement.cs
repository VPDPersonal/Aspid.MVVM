using UnityEditor;
using System.Linq;
using Aspid.CustomEditors;
using Aspid.UnityFastTools;
using UnityEngine.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public sealed class BaseInspectorVisualElement : VisualElement
    {
        public BaseInspectorVisualElement(SerializedObject serializedObject, IEnumerable<string> propertiesExcluding)
            : this(serializedObject, propertiesExcluding.ToArray()) { }
        
        public BaseInspectorVisualElement(SerializedObject serializedObject, IReadOnlyCollection<string> propertiesExcluding = null)
        {
            var baseInspector = Elements.CreateContainer(EditorColor.LightContainer)
                .SetName("BaseInspector")
                .SetPadding(top: 5, bottom: 10)
                .AddTitle(EditorColor.LightText, "Parameters");
            
            var enterChildren = true;
            var iterator = serializedObject.GetIterator();

            while (iterator.NextVisible(enterChildren))
            {
                enterChildren = false;
                if (propertiesExcluding?.Contains(iterator.name) ?? false) continue;
                baseInspector.AddChild(new AspidPropertyField(iterator));
            }
            
            baseInspector.style.display = baseInspector.childCount > 1 ? DisplayStyle.Flex : DisplayStyle.None;
            Add(baseInspector);
        }
    }
}