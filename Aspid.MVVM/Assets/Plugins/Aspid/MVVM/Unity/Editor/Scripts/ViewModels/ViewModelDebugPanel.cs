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
        
        public static VisualElement Build<T>(T view, out List<IUpdatableDebugField> updatableFields)
            where T : Object, IView
        {
            updatableFields = new List<IUpdatableDebugField>();
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

        private static VisualElement BuildDataContainer(IViewModel viewModel, List<IUpdatableDebugField> updatableFields)
        {
            if (viewModel is null) return new VisualElement();
            
            var viewModelType = viewModel.GetType();
            var viewModelBaseType = viewModel switch
            {
                MonoBehaviour => typeof(MonoBehaviour),
                ScriptableObject => typeof(ScriptableObject),
                _ => typeof(object)
            };
            
            var members = viewModelType.GetMembersInfosIncludingBaseClasses(BindingAttr, viewModelBaseType);
            var fields = members.OfType<FieldInfo>().ToArray();
            
            var bindFieldsContainer = new VisualElement().SetName("BindFields");
            var otherFieldsContainer = new VisualElement().SetName("OtherFields");
            var commandsContainer = new VisualElement().SetName("Commands");
            
            var prefsKeyPrefix = viewModelType.FullName + "_DebugTab_";
            var tabView = new ViewModelDebugTabView(prefsKeyPrefix, bindFieldsContainer, otherFieldsContainer, commandsContainer);
            
            var container = new VisualElement()
                .AddChild(tabView)
                .AddChild(bindFieldsContainer)
                .AddChild(commandsContainer)
                .AddChild(otherFieldsContainer);

            foreach (var field in fields)
            {
                // Skip RelayCommand fields - they go to Commands tab
                if (field.FieldType.IsRelayCommandType())
                    continue;
                    
                if (field.IsDefined(typeof(BaseBindAttribute)))
                {
                    var fieldElement = new DebugField(viewModel, field);
                    
                    bindFieldsContainer.AddChild(fieldElement);
                    updatableFields.Add(fieldElement);
                }
            }
            
            foreach (var field in fields)
            {
                // Skip RelayCommand fields - they go to Commands tab
                if (field.FieldType.IsRelayCommandType())
                    continue;
                    
                if (!field.IsDefined(typeof(BaseBindAttribute)) && !field.IsDefined(typeof(GeneratedCodeAttribute)))
                {
                    var fieldElement = new DebugField(viewModel, field);
                    
                    updatableFields.Add(fieldElement);
                    otherFieldsContainer.AddChild(fieldElement);
                }
            }
            
            foreach (var field in fields)
            {
                if (field.FieldType.IsRelayCommandType())
                {
                    var fieldElement = new DebugField(viewModel, field);
                    commandsContainer.AddChild(fieldElement);
                }
            }
            
            return container;
        }
        
        public class ViewModelDebugTabView : VisualElement
        {
            private readonly Color _selectedColor = new(0.05f, 0.55f, 0.37f, 1f);
            private readonly Color _normalColor = new(0.25f, 0.25f, 0.25f, 1f);
            
            private readonly Button _bindButton;
            private readonly Button _otherButton;
            private readonly Button _commandsButton;
            private readonly VisualElement _bindContainer;
            private readonly VisualElement _otherContainer;
            private readonly VisualElement _commandsContainer;
            private readonly string _prefsKeyPrefix;
            
            private bool _bindSelected;
            private bool _otherSelected;
            private bool _commandsSelected;

            public ViewModelDebugTabView(string prefsKeyPrefix, VisualElement bindContainer, VisualElement otherContainer, VisualElement commandsContainer)
            {
                _prefsKeyPrefix = prefsKeyPrefix;
                _bindContainer = bindContainer;
                _otherContainer = otherContainer;
                _commandsContainer = commandsContainer;
                
                _bindSelected = EditorPrefs.GetBool(_prefsKeyPrefix + "Bind", true);
                _otherSelected = EditorPrefs.GetBool(_prefsKeyPrefix + "Other", false);
                _commandsSelected = EditorPrefs.GetBool(_prefsKeyPrefix + "Commands", false);
                
                _bindButton = new Button(OnBindClicked)
                    .SetText("Bind")
                    .SetFontSize(14)
                    .SetUnityFontStyleAndWeight(FontStyle.Bold)
                    // .SetColor(Color.black)
                    .SetFlexGrow(1);
                    
                _otherButton = new Button(OnOtherClicked)
                    .SetText("Other")
                    .SetFontSize(14)
                    // .SetColor(Color.black)
                    .SetUnityFontStyleAndWeight(FontStyle.Bold)
                    .SetFlexGrow(1);
                
                _commandsButton = new Button(OnCommandsClicked)
                    .SetText("Commands")
                    .SetFontSize(14)
                    // .SetColor(Color.black)
                    .SetUnityFontStyleAndWeight(FontStyle.Bold)
                    .SetFlexGrow(1);
                
                this.SetSize(height: 25)
                    .SetFlexDirection(FlexDirection.Row)
                    .AddChild(_bindButton)
                    .AddChild(_commandsButton)
                    .AddChild(_otherButton);
                    
                UpdateVisuals();
            }
            
            private void OnBindClicked()
            {
                _bindSelected = !_bindSelected;
                EditorPrefs.SetBool(_prefsKeyPrefix + "Bind", _bindSelected);
                UpdateVisuals();
            }
            
            private void OnOtherClicked()
            {
                _otherSelected = !_otherSelected;
                EditorPrefs.SetBool(_prefsKeyPrefix + "Other", _otherSelected);
                UpdateVisuals();
            }
            
            private void OnCommandsClicked()
            {
                _commandsSelected = !_commandsSelected;
                EditorPrefs.SetBool(_prefsKeyPrefix + "Commands", _commandsSelected);
                UpdateVisuals();
            }
            
            private void UpdateVisuals()
            {
                _bindButton.SetBackgroundColor(_bindSelected ? _selectedColor : _normalColor);
                _commandsButton.SetBackgroundColor(_commandsSelected ? _selectedColor : _normalColor);
                _otherButton.SetBackgroundColor(_otherSelected ? _selectedColor : _normalColor);
                
                _bindContainer.style.display = _bindSelected ? DisplayStyle.Flex : DisplayStyle.None;
                _otherContainer.style.display = _otherSelected ? DisplayStyle.Flex : DisplayStyle.None;
                _commandsContainer.style.display = _commandsSelected ? DisplayStyle.Flex : DisplayStyle.None;
            }
        }
    }
}