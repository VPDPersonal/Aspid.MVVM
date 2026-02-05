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
            
            // Collect backing fields from bind properties to hide them
            var backingFieldNames = new HashSet<string>();
            
            var bindProperties = new List<PropertyInfo>();
            var autoProperties = new List<PropertyInfo>();

            foreach (var property in viewModelType.GetPropertyInfosIncludingBaseClasses(BindingAttr, viewModelBaseType))
            {
                if (property.IsDefined(typeof(BaseBindAttribute)))
                {
                    bindProperties.Add(property);
                    
                    if (NameHelper.TryGetBackingFieldName(property, viewModelType, out var backingFieldName))
                        backingFieldNames.Add(backingFieldName);
                }
                else if (NameHelper.IsAutoProperty(property, viewModelType))
                {
                    autoProperties.Add(property);
                    
                    var backingFieldName = NameHelper.GetAutoPropertyBackingFieldName(property.Name);
                    backingFieldNames.Add(backingFieldName);
                }
            }
            
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
                else if (!field.IsDefined(typeof(GeneratedCodeAttribute)) && !backingFieldNames.Contains(field.Name))
                {
                    var debugField = new DebugField(viewModel, field);
                    _updatableFields.Add(debugField);
                    otherSearchableFields.Add(debugField);
                    
                    otherFieldsContainer.AddChild(debugField);
                }
            }
            
            foreach (var property in bindProperties)
            {
                var debugField = new DebugField(viewModel, property);
                _updatableFields.Add(debugField);
                bindSearchableFields.Add(debugField);
                
                bindFieldsContainer.AddChild(debugField);
            }
            
            foreach (var property in autoProperties)
            {
                var debugField = new DebugField(viewModel, property);
                _updatableFields.Add(debugField);
                otherSearchableFields.Add(debugField);
                
                otherFieldsContainer.AddChild(debugField);
            }
            
            var tabView = new DebugViewModelTabView(prefsKeyPrefix, bindFieldsContainer, otherFieldsContainer, commandFieldsContainer);
            
            var searchField = new TextField(label: "Search").SetName("search-field");
#if UNITY_6000_0_OR_NEWER
            searchField.textEdition.placeholder = "Search fields... (e.g. hero.points, t:int, _todos.0. t:string)";
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
            
            // Parse the search query to extract name and type filter
            ParseSearchQuery(searchQuery, out var nameQuery, out var typeFilter);
            
            if (tabView.IsBindSelected)
                SearchInList(bindFields, nameQuery, typeFilter);
            
            if (tabView.IsOtherSelected)
                SearchInList(otherFields, nameQuery, typeFilter);
            
            if (tabView.IsCommandsSelected)
                SearchInList(commandsFields, nameQuery, typeFilter);
            
            return;
            
            void SearchInList(IReadOnlyCollection<ISearchableDebugField> fields, string name, string type)
            {
                foreach (var field in fields)
                {
                    field.Search(name, type);
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
        
        /// <summary>
        /// Parses a search query to extract name and type filter.
        /// Supports formats like:
        /// - "t: string" or "T: string" or "t:string" or "T:string"
        /// - "name t: string" or "t: string name"
        /// - "hero.points t: int"
        /// </summary>
        private static void ParseSearchQuery(string query, out string nameQuery, out string typeFilter)
        {
            nameQuery = string.Empty;
            typeFilter = null;
            
            if (string.IsNullOrWhiteSpace(query))
                return;
            
            // Pattern to match "t:" or "T:" (with optional space after colon)
            var typeMarkerIndex = -1;
            var typeMarkerLength = 0;
            
            for (var i = 0; i < query.Length - 1; i++)
            {
                if ((query[i] == 't' || query[i] == 'T') && query[i + 1] == ':')
                {
                    typeMarkerIndex = i;
                    typeMarkerLength = 2;
                    
                    // Check if there's a space after colon
                    if (i + 2 < query.Length && query[i + 2] == ' ')
                    {
                        typeMarkerLength = 3;
                    }
                    break;
                }
            }
            
            if (typeMarkerIndex == -1)
            {
                // No type filter found
                nameQuery = query;
                return;
            }
            
            // Extract parts before and after type marker
            var beforeType = query.Substring(0, typeMarkerIndex).Trim();
            var afterTypeStart = typeMarkerIndex + typeMarkerLength;
            var afterType = afterTypeStart < query.Length ? query.Substring(afterTypeStart).Trim() : string.Empty;
            
            // Determine which part is the type and which is the name
            // The part immediately after "t:" is the type
            var typePart = string.Empty;
            var namePart = beforeType;
            
            if (!string.IsNullOrEmpty(afterType))
            {
                // Find where the type ends (at next space or end of string)
                var spaceIndex = afterType.IndexOf(' ');
                if (spaceIndex > 0)
                {
                    typePart = afterType.Substring(0, spaceIndex).Trim();
                    var remainingAfterType = afterType.Substring(spaceIndex).Trim();
                    
                    // Combine name parts
                    if (!string.IsNullOrEmpty(namePart) && !string.IsNullOrEmpty(remainingAfterType))
                        namePart = namePart + " " + remainingAfterType;
                    else if (!string.IsNullOrEmpty(remainingAfterType))
                        namePart = remainingAfterType;
                }
                else
                {
                    typePart = afterType;
                }
            }
            
            nameQuery = namePart;
            typeFilter = string.IsNullOrEmpty(typePart) ? null : typePart;
        }
    }
}