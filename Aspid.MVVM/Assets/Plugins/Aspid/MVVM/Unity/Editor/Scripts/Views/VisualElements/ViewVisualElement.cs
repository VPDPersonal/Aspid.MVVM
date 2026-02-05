#if !ASPID_MVVM_EDITOR_DISABLED
#nullable enable
using UnityEditor;
using UnityEngine;
using System.Linq;
using Aspid.Internal;
using Aspid.UnityFastTools;
using UnityEngine.UIElements;
using System.Collections.Generic;
using Aspid.UnityFastTools.Editors;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    // TODO Aspid.MVVM Unity – Write summary
    public class ViewVisualElement<TView, TEditor> : VisualElement
        where TView : Object, IView
        where TEditor : ViewEditor<TView, TEditor>
    {
        private bool _isInitialized;
        protected readonly TEditor Editor;
        
        protected virtual string IconPath => EditorConstants.AspidIconGreen;
        
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
            
            this.Q<DebugViewModelPanel>()?.UpdateValue();
            this.Q<AspidInspectorHeader>().Icon.SetImageFromResource(IconPath);
            
            this.Q<VisualElement>(name: "view-model-debug-panel").style.display = Editor.TargetAsView?.ViewModel is not null 
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
                Add(onBuildHeader);
            }

            Add(BuildBaseInspector());

            var onBuiltBaseInspector = OnBuiltBaseInspector();
            if (onBuiltBaseInspector is not null)
            {
                Add(onBuiltBaseInspector);
            }
            
            Add(BuildViewModel());
        }

        private AspidInspectorHeader BuildHeader() => 
            new(GetScriptName(), Editor.TargetAsView, IconPath);

        protected virtual VisualElement? OnBuiltHeader() => null;
        
        private AspidBaseInspectorVisualElement BuildBaseInspector() =>
            new(SerializedObject, null, PropertiesExcluding.ToArray());

        protected virtual VisualElement? OnBuiltBaseInspector() => null;
        
        // TODO Aspid Aspid.MVVM Unity – Refactor
        private VisualElement BuildViewModel()
        {
            // TODO Aspid.MVVM Unity – Rename Name
            return new AspidContainer()
                .SetName("view-model-debug-panel")
                .AddChild(new DebugViewModelPanel(Editor.TargetAsView));
        }
        #endregion

        protected virtual string GetScriptName() =>
            Editor.TargetAsView.GetScriptName();
    }
}
#endif