#if !ASPID_MVVM_EDITOR_DISABLED
using System.Linq;
using UnityEditor;
using System.Reflection;
using Aspid.CustomEditors;
using UnityEngine.UIElements;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace Aspid.MVVM.Unity
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(MonoViewModel), editorForChildClasses: true)]
    public class MonoViewModelEditor : ViewModelEditor<MonoViewModel>
    {
        protected static readonly BindingFlags BindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
        
        protected override VisualElement Build()
        {
            return base.Build()
                .AddChild(BuildCommands());
        }

        protected VisualElement BuildCommands()
        {
            var commandsContainer = Elements.CreateContainer(EditorColor.LightContainer)
                .SetName("Commands")
                .SetMargin(top: 10);

            var viewModelType = ViewModel.GetType();
            var members = viewModelType.GetMembersInfosIncludingBaseClasses(BindingFlags)
                .Where(member => member is FieldInfo or PropertyInfo);

            var interfaceType = typeof(IRelayCommand);
            var commands = new List<(string, object)>();
            
            foreach (var member in members)
            {
                switch (member)
                {
                    case FieldInfo field:
                        {
                            if (!interfaceType.IsAssignableFrom(field.FieldType)) continue;
                            if (field.GetCustomAttribute<GeneratedCodeAttribute>() is not null) continue; 
                            
                            commands.Add((field.Name, field.GetValue(ViewModel)));
                            break;
                        }
                    case PropertyInfo property:
                        {
                            if (!interfaceType.IsAssignableFrom(property.PropertyType)) continue;
                            commands.Add((property.Name, property.GetValue(ViewModel)));
                            break;
                        }
                }
            }

            if (commands.Any())
            {
                foreach (var command in commands)
                {
                    if (command.Item2 is IRelayCommand relayCommand)
                    {
                        commandsContainer.AddChild(new Button(() =>
                        {
                            relayCommand.Execute();
                        })
                        {
                            text = command.Item1,
                        });
                    }
                }
            }

            return commandsContainer;
        }
    }
}
#endif