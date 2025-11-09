#nullable enable
using UnityEditor;
using System.Linq;
using Aspid.UnityFastTools;
using UnityEngine.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    // TODO Aspid.MVVM Unity – Write summary
    public sealed class AspidBaseInspectorVisualElement : VisualElement
    {
        public AspidBaseInspectorVisualElement(SerializedObject serializedObject, string? title, IReadOnlyCollection<string>? propertiesExcluding = null)
        {
            var container = Build(serializedObject, title, propertiesExcluding);
            style.display = container.style.display;
            Add(container);
        }

        private static VisualElement Build(SerializedObject serializedObject, string? title, IReadOnlyCollection<string>? propertiesExcluding)
        {
            var container = new AspidContainer();
            
            if (!string.IsNullOrWhiteSpace(title))
                container.AddChild(new AspidTitle(title));
            
            var count = 0;
            var enterChildren = true;
            var iterator = serializedObject.GetIterator();

            while (iterator.NextVisible(enterChildren))
            {
                enterChildren = false;
                if (propertiesExcluding?.Contains(iterator.name) ?? false) continue;

                var marginTop = count++ > 0 ? 4 : 0;
                container.AddChild(new AspidPropertyField(iterator).SetMargin(top: marginTop));
            }
            
            container.style.display = count > 0 ? DisplayStyle.Flex : DisplayStyle.None;
            return container;
        }
    }
}