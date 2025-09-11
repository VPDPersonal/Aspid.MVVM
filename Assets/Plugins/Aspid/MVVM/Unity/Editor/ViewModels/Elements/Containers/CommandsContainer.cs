using System.Linq;
using UnityEditor;
using System.Reflection;
using Aspid.CustomEditors;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class CommandsContainer : VisualElement
    {
        private const BindingFlags BindingAttr = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
        
        public CommandsContainer(object value)
        {
            var type = value.GetType();
            var prefsKey = type + "Commands";
                
            var foldout = new Foldout()
                {
                    text = "Commands",
                    value = EditorPrefs.GetBool(prefsKey, true)
                }
                .SetMargin(left: 10);
                
            foldout.RegisterValueChangedCallback(e =>
            {
                if (e.target != foldout) return;
                    
                if (!string.IsNullOrWhiteSpace(prefsKey))
                    EditorPrefs.SetBool(prefsKey, e.newValue);
            });
                
            this.AddChild(Elements.CreateContainer(EditorColor.LightContainer)
                .SetName("Commands")
                .SetMargin(top: 10)
                .AddChild(foldout));
                
            var properties = type.GetMembersInfosIncludingBaseClasses(BindingAttr)
                .OfType<PropertyInfo>()
                .Where(property => property.PropertyType.IsRelayCommandType());

            var i = 0;
                
            foreach (var property in properties)
            {
                var command = new RelayCommandField(value, property.GetValue(value), property.PropertyType, property.Name);
                if (i++ is not 0) command.SetMargin(top: 10);
                foldout.AddChild(command);
            }
        }
    }
}