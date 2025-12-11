using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using System.Reflection;
using Aspid.UnityFastTools;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    // TODO Aspid.MVVM Unity – Write summary
    // TODO Aspid.MVVM Unity – Refactor this class
    internal static class ViewModelDebugPanel
    {
        
        private const BindingFlags BindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        
        public static VisualElement Build<T>(T view, out List<IUpdatableField> updatableFields)
            where T : Object, IView
        {
            updatableFields = new List<IUpdatableField>();
            var viewType = view.GetType();
            
            var viewModelProperty = viewType.GetProperty("ViewModel", BindingAttr);
            if (viewModelProperty!.GetValue(view) is not IViewModel viewModel) return new VisualElement();
            
            var titleText = viewModel.GetType().FullName;
            
            var dataContainer = BuildDataContainer(viewModel, updatableFields).SetName("Data");
            var showViewModelToggle = new Toggle().SetValue(EditorPrefs.GetBool(titleText, false));
            showViewModelToggle.RegisterValueChangedCallback(e =>
            {
                EditorPrefs.SetBool(titleText, e.newValue);
                SetDataContainerDisplay();
            });
            
            var title = new AspidTitle(titleText); 
            title.Q<VisualElement>("TextContainer").AddChild(showViewModelToggle);
            
            SetDataContainerDisplay();
            
            return new VisualElement()
                .AddChild(title)
                .AddChild(dataContainer);

            void SetDataContainerDisplay()
            {
                dataContainer.style.display = showViewModelToggle.value ? DisplayStyle.Flex : DisplayStyle.None;
            }
        }

        private static VisualElement BuildDataContainer(IViewModel viewModel, List<IUpdatableField> updatableFields)
        {
            var viewModelType = viewModel.GetType();
            var viewModelBaseType = viewModel switch
            {
                MonoBehaviour => typeof(MonoBehaviour),
                ScriptableObject => typeof(ScriptableObject),
                _ => typeof(object)
            };
            
            var members = viewModelType.GetMembersInfosIncludingBaseClasses(BindingAttr, viewModelBaseType);
            var fields = members.OfType<FieldInfo>().ToArray();
            
            var container = new VisualElement()
                .AddChild(CreateTabContainer());

            foreach (var field in fields)
            {
                if (field.IsDefined(typeof(BaseBindAttribute)))
                {
                    var fieldElement = DebugViewModelField.Create(viewModel, field);
                    if (fieldElement is not null)
                    {
                        container.AddChild(fieldElement);
                        updatableFields.Add(fieldElement);
                    }
                }
            }
            
            foreach (var field in fields)
            {
                if (!field.IsDefined(typeof(BaseBindAttribute)) && !field.IsDefined(typeof(GeneratedCodeAttribute)))
                {
                    var fieldElement = DebugViewModelField.Create(viewModel, field);
                    if (fieldElement is not null)
                    {
                        updatableFields.Add(fieldElement);
                        container.AddChild(fieldElement);
                    }
                }
            }
            
            return container;
        }

        private static VisualElement CreateTabContainer()
        {
            return new ViewModelDebugTabView();
        }
        
        public class ViewModelDebugTabView : VisualElement
        {
            private readonly Color _normalColor = new(0.15f, 0.15f, 0.15f, 1f);
            private readonly Color _selectedColor = new(0.05f, 0.55f , 0.37f, 1f);

            public ViewModelDebugTabView()
            {
                this.SetSize(height: 25)
                    .SetFlexDirection(FlexDirection.Row)
                    .AddChild(new Button()
                        .SetText("All")
                        .SetFlexGrow(1))
                    .AddChild(new Button()
                        .SetText("Bind")
                        .SetFlexGrow(1)
                        .SetBackgroundColor(_selectedColor))
                    .AddChild(new Button()
                        .SetText("Bindable")
                        .SetFlexGrow(1));
            }
        }
    }
}