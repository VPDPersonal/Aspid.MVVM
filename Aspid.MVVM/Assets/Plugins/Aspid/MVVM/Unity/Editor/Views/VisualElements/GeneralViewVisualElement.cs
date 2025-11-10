#nullable enable
using System;
using Aspid.UnityFastTools;
using Aspid.MVVM.StarterKit;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Collections.Generic;
using UnityEditor;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    // TODO Aspid.MVVM Unity – Write summary
    public class GeneralViewVisualElement : MonoViewVisualElement<GeneralView, GeneralViewEditor>
    {
        private const string NoneChoice = "Null";
        private const string GeneralBindersId = "general-binders";
        private const string GlobalNamespace = "<Global>";
        
        protected override IEnumerable<string> PropertiesExcluding
        {
            get
            {
                foreach (var property in base.PropertiesExcluding)
                    yield return property;
                
                yield return Editor.BindersList.Property.propertyPath;
                yield return Editor.DesignViewModel.propertyPath;
            }
        }
        
        public GeneralViewVisualElement(GeneralViewEditor editor) 
            : base(editor) { }

        protected override VisualElement? OnBuiltHeader()
        {
            return new VisualElement()
                .AddChild(BuildDesignViewModel())
                .AddChild(BuiltGeneralBinders());
        }

        protected VisualElement BuildDesignViewModel()
        {
            var container = new AspidContainer(AspidContainer.StyleType.Dark);
            container.AddChild(new AspidTitle("Design ViewModel"));
            
            // Hidden bound field to ensure SerializedPropertyChangeEvent is emitted
            var propertyField = new PropertyField(Editor.DesignViewModel);
            propertyField.Bind(Editor.serializedObject);
            propertyField.SetDisplay(DisplayStyle.None);
            container.AddChild(propertyField);
            
            var currentAqn = Editor.DesignViewModel != null ? Editor.DesignViewModel.stringValue : string.Empty;
            
            var button = new Button()
                .SetText(GetCaptionForAqn(currentAqn))
                .SetMargin(0, 0, 0, 0);
            
            button.style.unityTextAlign = TextAnchor.MiddleLeft;
            button.tooltip = string.IsNullOrEmpty(currentAqn) 
                ? NoneChoice
                : currentAqn;
            
            button.clicked += () =>
            {
                var window = EditorWindow.focusedWindow;
                var buttonWorldBound = button.worldBound;
                
                var screenRect = window is not null
                    ? new Rect(window.position.x + buttonWorldBound.xMin, window.position.y + buttonWorldBound.yMax, buttonWorldBound.width, buttonWorldBound.height)
                    : new Rect(buttonWorldBound.xMin, buttonWorldBound.yMax, buttonWorldBound.width, buttonWorldBound.height);

                var current = Editor.DesignViewModel is not null 
                    ? Editor.DesignViewModel.stringValue 
                    : string.Empty;

                ViewModelPickerWindow.Show(screenRect, current, (aqn, caption) =>
                {
                    if (Editor.DesignViewModel == null) return;
                    Editor.DesignViewModel.stringValue = aqn ?? string.Empty;
                    Editor.serializedObject.ApplyModifiedProperties();
                    button.text = string.IsNullOrEmpty(aqn) ? NoneChoice : caption;
                    button.tooltip = string.IsNullOrEmpty(aqn) ? NoneChoice : aqn;
                });
            };
            
            return container.AddChild(button);
        }
        
        protected VisualElement BuiltGeneralBinders()
        {
            var container = new AspidContainer();
            
            return container
                .AddChild(BuildGeneralBinders());
        }

        private static string GetCaptionForAqn(string aqn)
        {
            if (string.IsNullOrEmpty(aqn)) return NoneChoice;
            try
            {
                var t = Type.GetType(aqn, false);
                if (t == null) return "<Missing>";
                var ns = string.IsNullOrEmpty(t.Namespace) ? GlobalNamespace : t.Namespace;
                return ns == GlobalNamespace ? t.Name : $"{ns}.{t.Name}";
            }
            catch { return "<Missing>"; }
        }


        private VisualElement BuildGeneralBinders()
        {
            var container = new VisualElement().SetName(GeneralBindersId);
            
            for (var i = 0; i < Editor.BindersList.ArraySize; i++)
            {
                var element = Editor.BindersList.GetArrayElementAtIndex(i);

                var property = new AspidPropertyField(element.MonoBindersProperty, element.Id)
                    .SetMargin(top: 3);
                
                property.Bind(Editor.serializedObject);
                container.AddChild(property);
            }

            return container;
        }

        public void UpdateGeneralBinders()
        {
            var generalBinders = this.Q<VisualElement>(GeneralBindersId);
            if (generalBinders is null) return;
            
            var generalBindersParent = generalBinders.parent;
            generalBindersParent.Remove(this.Q<VisualElement>(GeneralBindersId));
            generalBindersParent.AddChild(BuildGeneralBinders());

            generalBindersParent.SetDisplay(this.Q<VisualElement>(GeneralBindersId).childCount > 0 ? DisplayStyle.Flex : DisplayStyle.None);
        }

        protected override string GetScriptName()
        {
            if (Editor.TargetAsView.GetType() != typeof(GeneralView)) return base.GetScriptName();

            if (!string.IsNullOrWhiteSpace(Editor.DesignViewModel.stringValue))
            {
                return Type.GetType(Editor.DesignViewModel.stringValue).Name + " (General View)";
            }

            return "General View";
        }
    }
}