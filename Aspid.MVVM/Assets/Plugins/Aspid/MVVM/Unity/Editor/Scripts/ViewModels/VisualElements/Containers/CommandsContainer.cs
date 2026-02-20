using System.Linq;
using UnityEditor;
using UnityEngine;
using System.Reflection;
using Aspid.UnityFastTools;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class CommandsContainer : VisualElement
    {
        private const string StyleSheetPath = "Styles/aspid-command-container";
        private const BindingFlags BindingAttr = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
        
        public CommandsContainer(object value)
        {
            var type = value.GetType();
            var fields = type.GetFieldInfosIncludingBaseClasses(BindingAttr)
                .Where(field => field.FieldType.IsRelayCommandType())
                .ToArray();
            
            styleSheets.Add(styleSheet: Resources.Load<StyleSheet>(StyleSheetPath));
            var prefsKey = type.Name + "Commands";
            
            var toggle = new Toggle()
                .SetValue(EditorPrefs.GetBool(prefsKey, true));
            
            var container = new VisualElement().SetName("container")
                .SetDisplay(toggle.value ? DisplayStyle.Flex : DisplayStyle.None);
            
            var title = new AspidTitle(text: "Commands");
            title.Q<VisualElement>(name: "TextContainer").AddChild(toggle);
            
            toggle.RegisterValueChangedCallback(e =>
            {
                if (e.target != toggle) return;
                
                if (!string.IsNullOrWhiteSpace(prefsKey))
                    EditorPrefs.SetBool(prefsKey, e.newValue);
                
                container.style.display = e.newValue ? DisplayStyle.Flex : DisplayStyle.None;
            });
            
            this.AddChild(new AspidContainer().SetName("Commands")
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