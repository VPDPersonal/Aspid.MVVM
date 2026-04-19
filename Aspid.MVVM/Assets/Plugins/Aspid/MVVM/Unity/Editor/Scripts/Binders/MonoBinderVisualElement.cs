#if !ASPID_MVVM_EDITOR_DISABLED
using System;
using UnityEngine;
using UnityEditor;
using System.Linq;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using Aspid.FastTools.Editors;
using System.Collections.Generic;
using Aspid.FastTools.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// UIElements visual element that renders the inspector UI for a <see cref="MonoBinder"/>,
    /// including header, ID selector, View selector, and logs.
    /// </summary>
    public class MonoBinderVisualElement : VisualElement
    {
        private static readonly StyleSheet _idSelectorStyleSheet = Resources.Load<StyleSheet>("Styles/Aspid-MVVM-MonoBinder");

        private int _logsSize;
        private bool _isInitialized;
        private ListView _logsListView;
        private VisualElement _logsContainer;
        private readonly MonoBinderEditor _editor;
        
        private Button RestoreIdButton { get; set; }
        
        private Button SelectViewButton { get; set; }
        
        private Button RestoreViewButton { get; set; }
        
        public DropdownField IdDropdown { get; private set; }

        public DropdownField ViewDropdown { get; private set; }
        
        protected virtual IEnumerable<string> PropertiesExcluding
        {
            get
            {
                yield return "m_Script";
                yield return _editor.ModeProperty.name;
                yield return _editor.IdProperty.ValueProperty.name;
                yield return _editor.ViewProperty.ValueProperty.name;
                yield return _editor.IdProperty.PreviousProperty.name;
                yield return _editor.ViewProperty.PreviousProperty.name;
                
                if (_editor.LogsProperty is not null)
                    yield return _editor.LogsProperty.name;
                
                if (_editor.IsDebugProperty is not null)
                    yield return _editor.IsDebugProperty.name;
            }
        }
        
        protected SerializedObject SerializedObject => _editor.serializedObject; 
        
        protected virtual MessageType MessageType => _editor.HasBinderId
            ? MessageType.None
            : MessageType.Error;

        public MonoBinderVisualElement(MonoBinderEditor editor)
        {
            _editor = editor;
        }

        public void Initialize()
        {
            if (_isInitialized) return;
            
            Add(Build());
            _isInitialized = true;
        }

        public void Update()
        {
            if (!_isInitialized) return;

            FillLogList();

            // Update Header
            this.Q<HelpBox>().SetDisplay(_editor.HasBinderId ? DisplayStyle.None : DisplayStyle.Flex);
            this.Q<AspidInspectorHeader>().SetMessageType(MessageType);

            RestoreIdButton?.SetDisplay(_editor.CanRestoreId() ? DisplayStyle.Flex : DisplayStyle.None);
            RestoreViewButton?.SetDisplay(_editor.CanRestoreView() ? DisplayStyle.Flex : DisplayStyle.None);
            SelectViewButton?.SetDisplay(_editor.ViewProperty?.Value is not null ? DisplayStyle.Flex : DisplayStyle.None);
        }

        protected virtual VisualElement Build()
        {
            return new VisualElement()
                .AddChild(BuildHeader())
                .AddChild(BuildIdSelector())
                .AddChild(new PropertyField(_editor.IdProperty.ValueProperty).SetDisplay(DisplayStyle.None))
                .AddChild(new PropertyField(_editor.ViewProperty.ValueProperty).SetDisplay(DisplayStyle.None))
                .AddChild(BuildBaseInspector())
                .AddChild(BuildLogsContainer());
        }

        protected virtual VisualElement BuildHeader()
        {
            var binder = _editor.TargetAsMonoBinder;
            return new AspidInspectorHeader(label: GetScriptName(), binder, MessageType);
        }

        protected virtual VisualElement BuildIdSelector()
        {
            ViewDropdown = CreateDropdownView();
            IdDropdown = CreateDropdownId();
            SelectViewButton = CreateSelectViewButton();
            RestoreIdButton = CreateRestoreButton(_editor.RestoreId);
            RestoreViewButton = CreateRestoreButton(_editor.RestoreView);

            var dropdowns = new VisualElement();
            dropdowns.AddToClassList("aspid-mono-binder-id-selector-dropdowns");
            dropdowns.style.flexGrow = 1;
            dropdowns.AddChild(CreateFieldContainer("View", ViewDropdown, SelectViewButton, RestoreViewButton))
                .AddChild(CreateFieldContainer("ID", IdDropdown, RestoreIdButton));
            
             var helpBox = new AspidHelpBox(message: "View and ID must be assigned", HelpBoxMessageType.Error)
                 .SetDisplay(_editor.HasBinderId ? DisplayStyle.None : DisplayStyle.Flex);

             var modeField = new PropertyField(_editor.ModeProperty, label: string.Empty);
             modeField.AddToClassList("aspid-mono-binder-id-selector-mode");

             var container = new AspidContainer(AspidContainer.StyleType.Dark);
             container.styleSheets.Add(_idSelectorStyleSheet);
             container.AddToClassList("aspid-mono-binder-id-selector");

             return container
                 .AddChild(dropdowns)
                 .AddChild(helpBox)
                 .AddChild(modeField);

             DropdownField CreateDropdownId()
             {
                 var data = BinderDropdownData.CreateIdDropdownData(_editor);
                 var dropdown = new DropdownField(data.Choices, data.Index)
                 {
                     formatSelectedValueCallback = value =>
                     {
                         var previousId = _editor.IdProperty.PreviousValue;
                         return value is BinderEditorConstants.NoId or null or "" && !string.IsNullOrWhiteSpace(previousId)
                             ? previousId
                             : value;
                     }
                 };

                 dropdown.AddToClassList("aspid-mono-binder-id-selector-dropdown");
                 dropdown.AddToClassList("aspid-mono-binder-id-selector-dropdown-id");
                 return dropdown;
             }

             DropdownField CreateDropdownView()
             {
                 var data = BinderDropdownData.CreateViewDropdownData(_editor);
                 var dropdown = new DropdownField(data.Choices, data.Index)
                 {
                     formatSelectedValueCallback = value =>
                     {
                         var previousName = _editor.ViewProperty.PreviousName;

                         return value is BinderEditorConstants.NoView or null or "" && !string.IsNullOrWhiteSpace(previousName)
                             ? previousName
                             : value;
                     }
                 };

                 dropdown.AddToClassList("aspid-mono-binder-id-selector-dropdown");
                 dropdown.AddToClassList("aspid-mono-binder-id-selector-dropdown-view");
                 return dropdown;
             }

             Button CreateSelectViewButton()
             {
                 var button = new Button(clickEvent: () =>
                     {
                         if (_editor.ViewProperty.Value is not Component view) return;
                         EditorGUIUtility.PingObject(view.gameObject);
                     })
                     .AddChild(new Image()
                         .SetImage(EditorGUIUtility.IconContent(name: "d_pick_uielements@2x").image as Texture2D));
            
                 button.SetDisplay(_editor.ViewProperty.Value is not null ? DisplayStyle.Flex : DisplayStyle.None);
                 button.AddToClassList("aspid-mono-binder-id-selector-view-select-button");
                 button.RegisterCallback<ClickEvent>(clickEvent =>
                 {
                     if (clickEvent.clickCount < 2) return;
                     if (_editor.ViewProperty.Value is not Component view) return;
                
                     Selection.activeGameObject = view.gameObject;
                 });

                 return button;
             }
             
             Button CreateRestoreButton(Action restore)
             {
                 var button = new Button(restore)
                     .AddChild(new Image()
                         .SetImage(EditorGUIUtility.IconContent(name: "d_Refresh").image as Texture2D));

                 button.AddToClassList("aspid-mono-binder-id-selector-restore-button");
                 return button;
             }

             static VisualElement CreateFieldContainer(string labelText, DropdownField dropdown, params Button[] buttons)
             {
                 var label = new Label(text: labelText);
                 label.AddToClassList("aspid-mono-binder-id-selector-dropdown-label");

                 var row = new VisualElement().AddChild(dropdown);
                 foreach (var button in buttons)
                     row.AddChild(button);
                 row.AddToClassList("aspid-mono-binder-id-selector-row");

                 var column = new VisualElement()
                     .AddChild(label)
                     .AddChild(row);
                 column.AddToClassList("aspid-mono-binder-id-selector-dropdown-column");

                 return column;
             }
        }

        private AspidBaseInspectorVisualElement BuildBaseInspector() =>
            new(SerializedObject, "Parameters", PropertiesExcluding.ToArray());

        protected virtual VisualElement BuildLogsContainer()
        {
            if (_editor.LogsProperty is null || _editor.IsDebugProperty is null) 
                return new VisualElement();
            
            var isDebugPropertyField = new PropertyField(_editor.IsDebugProperty);
            isDebugPropertyField.Bind(_editor.serializedObject);
            
            var title = new AspidTitle(text: "Logs");
            title.Q<VisualElement>(name: "TextContainer").AddChild(isDebugPropertyField);

            _logsContainer = new AspidContainer().SetName("Logs")
                .AddChild(title);
            
            isDebugPropertyField.RegisterValueChangeCallback(e =>
            {
                if (e.changedProperty.name is not "_isDebug") return;

                if (_logsListView is not null)
                {
                    _logsListView?.SetDisplay(e.changedProperty.boolValue ? DisplayStyle.Flex : DisplayStyle.None);
           
                    _logsListView.schedule.Execute(() =>
                    {
                        _logsListView.ScrollToItem(_logsSize - 1);
                
                    }).ExecuteLater(-1);
                }
            });

            return _logsContainer;
        }
        
        private void FillLogList()
        {
            if (_editor?.LogsProperty is null) return;
            
            if (_logsSize != _editor.LogsProperty.arraySize)
            {
                _logsSize = _editor.LogsProperty.arraySize;
                var logs = new string[_logsSize];

                for (var i = 0; i < logs.Length; i++)
                {
                    logs[i] = _editor.LogsProperty.GetArrayElementAtIndex(i).stringValue;
                }

                if (_logsListView is not null)
                {
                    _logsContainer.Remove(_logsListView);
                }
                
                _logsListView = new ListView(logs)
                {
                    makeItem = () => new TextField(string.Empty) 
                    { 
                        isReadOnly = true 
                    }
                    .SetPadding(bottom: 2, right: 4),

                    bindItem = (element, index) => ((TextField)element).value = logs[index],
                };
                
                var height = logs.Length < 6
                    ? _logsListView.style.height
                    : new StyleLength(110);
                
                _logsContainer
                    .AddChild(_logsListView
                        .SetSize(height: height));
                
                // Scroll to the bottom after the layout is ready
                _logsListView.schedule.Execute(() =>
                {
                    _logsListView.ScrollToItem(logs.Length - 1);
                }).ExecuteLater(-1);
            }
        }
        
        public static void UpdateDropdownColor(DropdownField dropdown, bool hasPrevious)
        {
            const string previousClass = "aspid-mono-binder-id-selector-dropdown--previous";

            if (hasPrevious)
                dropdown.AddToClassList(previousClass);
            else
                dropdown.RemoveFromClassList(previousClass);
        }

        protected virtual string GetScriptName()
        {
            var monoBinder = _editor.TargetAsMonoBinder;
            return !monoBinder ? string.Empty : monoBinder.GetScriptNameWithIndex();
        }
    }
}
#endif