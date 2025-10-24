using System.Linq;
using UnityEditor;
using System.Reflection;
using Aspid.CustomEditors;
using Aspid.UnityFastTools;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    // TODO Aspid Refactor
    internal sealed class CommandsContainer : VisualElement
    {
        private const BindingFlags BindingAttr = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
        
        public CommandsContainer(object value)
        {
            var type = value.GetType();
            
            var properties = type.GetMembersInfosIncludingBaseClasses(BindingAttr)
                .OfType<PropertyInfo>()
                .Where(property => property.PropertyType.IsRelayCommandType())
                .ToArray();

            if (properties.Length is 0)
            {
                style.display = DisplayStyle.None;
            }
            else
            {
                var prefsKey = type + "Commands";
                
                var toggle = new Toggle().SetValue(EditorPrefs.GetBool(prefsKey, true));
                var container = new VisualElement()
                    .SetDisplay(toggle.value ? DisplayStyle.Flex : DisplayStyle.None);
                
                var title = Elements.CreateTitle(EditorColor.LightText, "Commands");
                title.Q<VisualElement>("TextContainer").AddChild(toggle);
                
                toggle.RegisterValueChangedCallback(e =>
                {
                    if (e.target != toggle) return;
                    
                    if (!string.IsNullOrWhiteSpace(prefsKey))
                        EditorPrefs.SetBool(prefsKey, e.newValue);
                    
                    container.style.display = e.newValue ? DisplayStyle.Flex : DisplayStyle.None;
                });
                
                this.AddChild(Elements.CreateContainer(EditorColor.LightContainer)
                        .SetName("Commands")
                        .AddChild(title)
                        .AddChild(container
                            .SetMargin(top: 5, bottom: 5)));

                var i = 0;
                
                foreach (var property in properties)
                {
                    var command = new RelayCommandField(value, property.GetValue(value), property.PropertyType, property.Name);
                    if (i++ is not 0) command.SetMargin(top: 10);
                    container.AddChild(command);
                }  
            }
        }
    }
}