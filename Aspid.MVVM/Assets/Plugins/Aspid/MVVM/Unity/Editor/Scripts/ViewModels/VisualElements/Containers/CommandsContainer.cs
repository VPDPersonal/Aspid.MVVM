using System.Linq;
using UnityEditor;
using UnityEngine;
using System.Reflection;
using UnityEngine.UIElements;
using Aspid.FastTools.Reflection;
using Aspid.FastTools.UIElements;
using Aspid.FastTools.UIElements.Editors.Internal;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class CommandsContainer : VisualElement
    {
        private const string StyleSheetPath = "Styles/Aspid-MVVM-CommandContainer";
        private const BindingFlags BindingAttr = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
        
        public CommandsContainer(object value)
        {
            var type = value.GetType();
            var fields = type.GetMembersInfosIncludingBaseClasses(BindingAttr)
                .OfType<FieldInfo>()
                .Where(field => field.FieldType.IsRelayCommandType())
                .ToArray();
            
            styleSheets.Add(styleSheet: Resources.Load<StyleSheet>(StyleSheetPath));
            var prefsKey = type.Name + "Commands";
            
            var toggle = new Toggle()
                .SetValue(EditorPrefs.GetBool(prefsKey, true));
            
            var container = new VisualElement().SetName("container")
                .SetDisplay(toggle.value ? DisplayStyle.Flex : DisplayStyle.None);
            
            var title = new AspidLabel(text: "Commands").SetMarginBottom(5);
            var titleLabel = title[0];
            title.RemoveAt(0);
            title.Insert(index: 0, new VisualElement()
                .SetFlexDirection(FlexDirection.Row)
                .SetJustifyContent(Justify.SpaceBetween)
                .AddChild(titleLabel)
                .AddChild(toggle));
            
            toggle.RegisterValueChangedCallback(e =>
            {
                if (e.target != toggle) return;
                
                if (!string.IsNullOrWhiteSpace(prefsKey))
                    EditorPrefs.SetBool(prefsKey, e.newValue);
                
                container.style.display = e.newValue ? DisplayStyle.Flex : DisplayStyle.None;
            });
            
            this.AddChild(new AspidBox().SetName("Commands")
                .SetMargin(top: 5, left: -10f)
                .AddChild(title)
                .AddChild(container));

            if (fields.Length is 0)
            {
                var methods = type.GetMembersInfosIncludingBaseClasses(BindingAttr)
                    .OfType<MethodInfo>()
                    .Where(method => method.IsDefined(typeof(RelayCommandAttribute)))
                    .ToArray();

                if (methods.Length is 0)
                {
                    style.display = DisplayStyle.None;
                }
                else
                {
                    foreach (var method in methods)
                    {
                        var button = new Button(() => method.Invoke(value, new object[] { }))
                            .SetText(method.Name);
                        
                        container.AddChild(button);
                    }  
                }
            }
            else
            {
                foreach (var field in fields)
                {
                    var command = new RelayCommandField(field.GetGeneratedPropertyName(), value, field);
                    container.AddChild(command);
                }  
            }
        }
    }
}