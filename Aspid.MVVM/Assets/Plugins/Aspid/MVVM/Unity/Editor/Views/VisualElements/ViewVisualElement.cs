#nullable enable
using UnityEditor;
using UnityEngine;
using Aspid.CustomEditors;
using Aspid.UnityFastTools;
using UnityEngine.UIElements;
using System.Collections.Generic;
using Aspid.UnityFastTools.Editors;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public class ViewVisualElement<TView, TEditor> : VisualElement
        where TView : Object, IView
        where TEditor : ViewEditor<TView, TEditor>
    {
        protected readonly TEditor Editor;
        private bool _isInitialized;
        
        protected virtual string IconPath => "Aspid Icon";
        
        protected virtual IEnumerable<string> PropertiesExcluding
        {
            get
            {
                yield return "m_Script";
            }
        }
        
        protected SerializedObject SerializedObject => Editor.serializedObject;
        
        public ViewVisualElement(TEditor editor)
        {
            Editor = editor;
        }
        
        public void Initialize()
        {
            if (_isInitialized) return;

            Build();
            _isInitialized = true;
        }

        public void Update()
        {
            OnUpdate();
            
            this.Q<VisualElement>("ViewModelDebugPanel").style.display = Editor.TargetAsSpecific?.ViewModel is not null 
                ? DisplayStyle.Flex
                : DisplayStyle.None;
        }
        
        protected virtual void OnUpdate() { }

        #region Build Methods
        private void Build()
        {
            Add(BuildHeader());
            
            var onBuildHeader = OnBuiltHeader();
            if (onBuildHeader is not null)
            {
                Add(onBuildHeader
                    .SetMargin(top: 10));
            }

            Add(BuildBaseInspector()
                .SetMargin(top: 10));

            var onBuiltBaseInspector = OnBuiltBaseInspector();
            if (onBuiltBaseInspector is not null)
            {
                Add(onBuiltBaseInspector
                    .SetMargin(top: 10));
            }
            
            Add(BuildViewModel());
        }

        private InspectorHeaderPanel BuildHeader() => 
            new(GetScriptName(), Editor.TargetAsSpecific, IconPath);

        protected virtual VisualElement? OnBuiltHeader() => null;
        
        private BaseInspectorVisualElement BuildBaseInspector() =>
            new(SerializedObject, PropertiesExcluding);

        protected virtual VisualElement? OnBuiltBaseInspector() => null;
        
        // TODO Aspid Refactor
        private VisualElement BuildViewModel()
        {
            return Elements.CreateContainer(EditorColor.LightContainer)
                .SetName("ViewModelDebugPanel")
                .AddChild(ViewModelDebugPanel.Build(Editor.TargetAsSpecific)
                    .SetName("ViewModelContainer"))
                .SetMargin(top: 10);
        }
        #endregion

        protected virtual string GetScriptName() =>
            Editor.TargetAsSpecific.GetScriptName();
    }
}