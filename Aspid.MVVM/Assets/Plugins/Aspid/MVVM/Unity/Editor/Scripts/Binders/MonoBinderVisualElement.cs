#nullable enable
using UnityEngine;
using UnityEditor;
using System.Linq;
using Aspid.Internal;
using Aspid.UnityFastTools;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    // TODO Aspid.MVVM Unity – Refactor
    // TODO Aspid.MVVM Unity – Write summary
    public class MonoBinderVisualElement : VisualElement
    {
        private bool _isInitialized;
        private readonly MonoBinderEditor _editor;
        
        public DropdownField? IdDropdown { get; private set; }
        
        public DropdownField? ViewDropdown { get; private set; }
        
        protected virtual IEnumerable<string> PropertiesExcluding
        {
            get
            {
                yield return "m_Script";
                yield return _editor.IdProperty.name;
                yield return _editor.ViewProperty.name;
                yield return _editor.ModeProperty.name;
                
                if (_editor.LogsProperty is not null)
                    yield return _editor.LogsProperty.name;
                
                if (_editor.IsDebugProperty is not null)
                    yield return _editor.IsDebugProperty.name;
            }
        }
        
        protected SerializedObject SerializedObject => _editor.serializedObject; 
        
        protected virtual string IconPath => _editor.HasBinderId
            ? EditorConstants.AspidIconGreen
            : EditorConstants.AspidIconRed;

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
            
            // Update Header
            this.Q<HelpBox>().SetDisplay(_editor.HasBinderId ? DisplayStyle.None : DisplayStyle.Flex);
            this.Q<AspidInspectorHeader>().Icon.SetImageFromResource(IconPath);
        }

        protected virtual VisualElement Build()
        {
            return new VisualElement()
                .AddChild(BuildHeader())
                .AddChild(BuildIdSelector())
                .AddChild(new PropertyField(_editor.IdProperty).SetDisplay(DisplayStyle.None))
                .AddChild(new PropertyField(_editor.ViewProperty).SetDisplay(DisplayStyle.None))
                .AddChild(BuildBaseInspector())
                .AddChild(BuildLogsContainer());
        }

        protected virtual VisualElement BuildHeader()
        {
            var binder = _editor.TargetAsMonoBinder;
            return new AspidInspectorHeader(binder, IconPath);
        }

        protected virtual VisualElement BuildIdSelector()
        {
            ViewDropdown = CreateDropdownView();
            IdDropdown = CreateDropdownId();
            
            var dropdowns = new VisualElement()
                .SetMargin(bottom: 1)
                .SetAlignItems(Align.Center)
                .SetFlexDirection(FlexDirection.Row)
                .AddChild(CreateFieldElement(ViewDropdown, "View"))
                .AddChild(CreateFieldElement(IdDropdown, "ID"));
             
             var helpBox = new AspidHelpBox("View and ID must be assigned", HelpBoxMessageType.Error)
                 .SetDisplay(_editor.HasBinderId ? DisplayStyle.None : DisplayStyle.Flex);

             var modeField = new PropertyField(_editor.ModeProperty, string.Empty)
                 .SetMargin(top: 1);
             
             return
                 new AspidContainer(AspidContainer.StyleType.Dark)
                 .SetFlexDirection(FlexDirection.Column)
                 .AddChild(dropdowns)
                 .AddChild(helpBox)
                 .AddChild(modeField);

             DropdownField CreateDropdownId()
             {
                 var data = DropdownData.CreateIdDropdownData(_editor);
                 return new DropdownField(data.Choices, data.Index)
                     .SetMargin(0, 0, 1, 0);
             }
             
             DropdownField CreateDropdownView()
             {
                 var data = DropdownData.CreateViewDropdownData(_editor);
                 return new DropdownField(data.Choices, data.Index)
                     .SetMargin(0, 0, 0, 1);
             }
             
             VisualElement CreateFieldElement(DropdownField dropdown, string label) => new VisualElement()
                 .SetFlexGrow(1)
                 .SetFlexDirection(FlexDirection.Column)
                 .SetSize(width: new StyleLength(new Length(50, LengthUnit.Percent)))
                 .AddChild(new Label(label)
                     .SetFontSize(13)
                     .SetMargin(bottom: 1)
                     .SetAlignSelf(Align.FlexStart)
                     .SetColor(new Color(0.75f, 0.75f, 0.75f))
                     .SetUnityFontStyleAndWeight(FontStyle.Bold))
                 .AddChild(dropdown
                     .SetFlexGrow(1));
        }

        private AspidBaseInspectorVisualElement BuildBaseInspector() =>
            new(SerializedObject, "Parameters", PropertiesExcluding.ToArray());

        protected virtual VisualElement BuildLogsContainer()
        {
            if (_editor.LogsProperty is null || _editor.IsDebugProperty is null) return new VisualElement();

            var isDebugPropertyField = new PropertyField(_editor.IsDebugProperty);

            var title = new AspidTitle("Logs");
            title.Q<VisualElement>("TextContainer").AddChild(isDebugPropertyField);

            var container = new AspidContainer().SetName("Logs")
                .AddChild(title);
            
            isDebugPropertyField.RegisterValueChangeCallback(e =>
            {
                if (e.changedProperty.name is not "_isDebug") return;

                var scrollViewIndex = container.IndexOf(container.Q<ScrollView>());
                if (scrollViewIndex > -1) container.RemoveAt(scrollViewIndex);
                
                if (!e.changedProperty.boolValue) return;
                
                var logs = new List<string>();

                for (var i = 0; i < _editor.LogsProperty.arraySize; i++)
                {
                    logs.Add(_editor.LogsProperty.GetArrayElementAtIndex(i).stringValue);
                }
                
                var list = new ListView(logs)
                {
                    makeItem = () => new HelpBox(string.Empty, HelpBoxMessageType.None),
                    bindItem = (element, index) => ((HelpBox)element).text = logs[index]
                };

                var height = logs.Count < 6
                    ? list.style.height
                    : new StyleLength(110);
                
                container.Add(new ScrollView()
                    .SetSize(height: height)
                    .AddChild(list));
            });

            return container;
        }
    }
}