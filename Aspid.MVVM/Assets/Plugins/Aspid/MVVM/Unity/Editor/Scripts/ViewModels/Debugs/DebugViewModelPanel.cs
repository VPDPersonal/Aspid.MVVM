using UnityEditor;
using UnityEngine;
using System.Reflection;
using Aspid.UnityFastTools;
using UnityEngine.UIElements;
using System.CodeDom.Compiler;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugViewModelPanel : VisualElement, IUpdatableDebugField
    {
        private const BindingFlags BindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        private readonly List<IUpdatableDebugField> _updatableFields = new();
        
        public DebugViewModelPanel(IView view)
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Styles/Debug/aspid-debug-view-model-panel"));
            
            var viewType = view.GetType();  
            var viewModelProperty = viewType.GetProperty(name: "ViewModel", BindingAttr);
            if (viewModelProperty?.GetValue(view) is not IViewModel viewModel) return;

            var titleText = viewModel.GetType().GetTypeDisplayName();
            var dataContainer = BuildDataContainer(viewModel).SetName("data-container");
            
            var showViewModelToggle = new Toggle().SetValue(EditorPrefs.GetBool(titleText, false));
            showViewModelToggle.RegisterValueChangedCallback(e =>
            {
                EditorPrefs.SetBool(titleText, e.newValue);
                SetDataContainerDisplay();
            });
            
            var title = new AspidTitle(titleText); 
            title.Q<VisualElement>("TextContainer").AddChild(showViewModelToggle);

            this.AddChild(title)
                .AddChild(dataContainer);
            
            SetDataContainerDisplay();
            return;
            
            void SetDataContainerDisplay() =>
                dataContainer.style.display = showViewModelToggle.value ? DisplayStyle.Flex : DisplayStyle.None;
        }

        public void UpdateValue()
        {
            foreach (var field in _updatableFields)
                field?.UpdateValue();
        }

        private VisualElement BuildDataContainer(IViewModel viewModel)
        {
            if (viewModel is null) return new VisualElement();
            
            var viewModelType = viewModel.GetType();
            var viewModelBaseType = viewModel switch
            {
                MonoBehaviour => typeof(MonoBehaviour),
                ScriptableObject => typeof(ScriptableObject),
                _ => typeof(object)
            };
            
            var prefsKeyPrefix = viewModelType.FullName + "_debugPanel";
            
            var bindSearchableFields = new List<ISearchableDebugField>();
            var otherSearchableFields = new List<ISearchableDebugField>();
            var commandsSearchableFields = new List<ISearchableDebugField>();
            
            var bindFieldsContainer = new VisualElement().SetName("BindFields");
            var otherFieldsContainer = new VisualElement().SetName("OtherFields");
            var commandFieldsContainer = new VisualElement().SetName("CommandFields");
            
            foreach (var field in viewModelType.GetFieldInfosIncludingBaseClasses(BindingAttr, viewModelBaseType))
            {
                if (field.FieldType.IsRelayCommandType())
                {
                    var debugField = new DebugField(viewModel, field);
                    _updatableFields.Add(debugField);
                    commandsSearchableFields.Add(debugField);
                    
                    commandFieldsContainer.AddChild(debugField);
                }
                else if (field.IsDefined(typeof(BaseBindAttribute)))
                {
                    var debugField = new DebugField(viewModel, field);
                    _updatableFields.Add(debugField);
                    bindSearchableFields.Add(debugField);
                    
                    bindFieldsContainer.AddChild(debugField);
                }
                else if (!field.IsDefined(typeof(GeneratedCodeAttribute)))
                {
                    var debugField = new DebugField(viewModel, field);
                    _updatableFields.Add(debugField);
                    otherSearchableFields.Add(debugField);
                    
                    otherFieldsContainer.AddChild(debugField);
                }
            }
            
            var tabView = new DebugViewModelTabView(prefsKeyPrefix, bindFieldsContainer, otherFieldsContainer, commandFieldsContainer);
            
            var searchField = new TextField(label: "Search").SetName("search-field");
#if UNITY_6000_0_OR_NEWER
            searchField.textEdition.placeholder = "Search fields... (e.g. hero.points)";
#endif
            
            searchField.RegisterValueChangedCallback(e =>
            {
                var searchQuery = e.newValue?.Trim();
                EditorPrefs.SetString(prefsKeyPrefix + "_search", searchQuery ?? string.Empty);
                Search(searchQuery, tabView, bindSearchableFields, otherSearchableFields, commandsSearchableFields);
            });
            
            var savedSearch = EditorPrefs.GetString(prefsKeyPrefix + "_search", string.Empty);
            if (!string.IsNullOrWhiteSpace(savedSearch))
            {
                searchField.SetValueWithoutNotify(savedSearch);
                Search(savedSearch, tabView, bindSearchableFields, otherSearchableFields, commandsSearchableFields);
            }
            
            return new VisualElement()
                .AddChild(tabView)
                .AddChild(searchField)
                .AddChild(bindFieldsContainer)
                .AddChild(commandFieldsContainer)
                .AddChild(otherFieldsContainer);
        }

        private static void Search(
            string searchQuery, 
            DebugViewModelTabView tabView,
            IReadOnlyCollection<ISearchableDebugField> bindFields,
            IReadOnlyCollection<ISearchableDebugField> otherFields,
            IReadOnlyCollection<ISearchableDebugField> commandsFields)
        {
            if (string.IsNullOrEmpty(searchQuery))
            {
                ClearSearch(bindFields);
                ClearSearch(otherFields);
                ClearSearch(commandsFields);
                
                return;
            }
            
            if (tabView.IsBindSelected)
                SearchInList(bindFields);
            
            if (tabView.IsOtherSelected)
                SearchInList(otherFields);
            
            if (tabView.IsCommandsSelected)
                SearchInList(commandsFields);
            
            return;
            
            void SearchInList(IReadOnlyCollection<ISearchableDebugField> fields)
            {
                foreach (var field in fields)
                {
                    field.Search(searchQuery);
                }
            }
            
            void ClearSearch(IReadOnlyCollection<ISearchableDebugField> fields)
            {
                foreach (var field in fields)
                {
                    field.ClearSearch();
                }
            }
        }
    }
}
