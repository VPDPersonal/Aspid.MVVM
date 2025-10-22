#nullable enable
using System.Linq;
using UnityEngine;
using UnityEditor;
using Aspid.CustomEditors;
using Aspid.UnityFastTools;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Collections.Generic;
using Aspid.UnityFastTools.Editors;
using Image = UnityEngine.UIElements.Image;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
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
        
        protected virtual string IconPath => _editor.HasBinderId ? "Aspid Icon" : "Aspid Icon Red";

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

        public void UpdateHeader()
        {
            if (!_isInitialized) return;
            
            // Update Header
            this.Q<HelpBox>().SetDisplay(_editor.HasBinderId ? DisplayStyle.None : DisplayStyle.Flex);
            this.Q<VisualElement>("Header").Q<Image>().SetImageFromResource(IconPath);
        }

        protected virtual VisualElement Build() => new VisualElement()
             .AddChild(BuildHeader())
             .AddChild(BuildIdSelector()
                 .SetMargin(top: 10))
             .AddChild(BuildBaseInspector()
                 .SetMargin(top: 10))
             .AddChild(BuildLogsContainer()
                 .SetMargin(top: 10));

        protected virtual VisualElement BuildHeader()
        {
            var binder = _editor.TargetAsMonoBinder;
            return new InspectorHeaderPanel(binder, IconPath);
        }

        protected virtual VisualElement BuildIdSelector()
        {
            ViewDropdown = CreateDropdownView();
            IdDropdown = CreateDropdownId();
            
            var dropdowns = new VisualElement()
                 .SetAlignItems(Align.Center)
                 .SetFlexDirection(FlexDirection.Row)
                 .AddChild(CreateFieldElement(ViewDropdown, "View"))
                 .AddChild(CreateFieldElement(IdDropdown, "ID"));
             
             var helpBox = Elements.CreateHelpBox("View and ID must be assigned", HelpBoxMessageType.Error)
                 .SetFontSize(14)
                 .SetDisplay(_editor.HasBinderId ? DisplayStyle.None : DisplayStyle.Flex);

             var modeField = new CorrectPropertyField(_editor.ModeProperty, string.Empty);
             
             return Elements.CreateContainer(EditorColor.DarkContainer)
                 .SetFlexDirection(FlexDirection.Column)
                 .AddChild(dropdowns)
                 .AddChild(helpBox)
                 .AddChild(modeField);

             DropdownField CreateDropdownId()
             {
                 var data = DropdownData.CreateIdDropdownData(_editor);
                 return new DropdownField(data.Choices, data.Index);
             }
             
             DropdownField CreateDropdownView()
             {
                 var data = DropdownData.CreateViewDropdownData(_editor);
                 return new DropdownField(data.Choices, data.Index);
             }
             
             VisualElement CreateFieldElement(DropdownField dropdown, string label) => new VisualElement()
                 .SetFlexGrow(1)
                 .SetFlexDirection(FlexDirection.Column)
                 .SetSize(width: new StyleLength(new Length(50, LengthUnit.Percent)))
                 .AddChild(new Label(label)
                     .SetFontSize(13)
                     .SetPadding(left: 5)
                     .SetAlignSelf(Align.FlexStart)
                     .SetColor(EditorColor.LightText)
                     .SetUnityFontStyleAndWeight(FontStyle.Bold))
                 .AddChild(dropdown
                     .SetFlexGrow(1));
        }

        protected VisualElement BuildBaseInspector()
        {
            var baseInspector = Elements.CreateContainer(EditorColor.LightContainer)
                .SetName("BaseInspector")
                .AddChild(Elements.CreateTitle(EditorColor.LightText, "Parameters")
                    .SetMargin(left: -5));
            
            var enterChildren = true;
            var iterator = SerializedObject.GetIterator();

            while (iterator.NextVisible(enterChildren))
            {
                enterChildren = false;
                if (PropertiesExcluding.Contains(iterator.name)) continue;
                baseInspector.AddChild(new CorrectPropertyField(iterator));
            }
            
            baseInspector.style.display = baseInspector.childCount > 1 ? DisplayStyle.Flex : DisplayStyle.None;
            return baseInspector;
        }

        protected virtual VisualElement BuildLogsContainer()
        {
            if (_editor.LogsProperty is null || _editor.IsDebugProperty is null) return new VisualElement();

            var isDebugPropertyField = new CorrectPropertyField(_editor.IsDebugProperty, string.Empty);
            
            var title = Elements.CreateTitle(EditorColor.LightText, "Logs");
            title.Q<VisualElement>("TextContainer").AddChild(isDebugPropertyField);

            var container = Elements.CreateContainer(EditorColor.LightContainer)
                .SetName("Logs")
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