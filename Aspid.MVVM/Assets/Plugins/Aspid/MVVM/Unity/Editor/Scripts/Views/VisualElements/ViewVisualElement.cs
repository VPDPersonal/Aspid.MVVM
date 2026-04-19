#if !ASPID_MVVM_EDITOR_DISABLED
#nullable enable
using UnityEditor;
using UnityEngine;
using System.Linq;
using UnityEngine.UIElements;
using Aspid.FastTools.Editors;
using Aspid.FastTools.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Base UIElements visual element for <see cref="IView"/> inspectors.
    /// Renders header, base inspector fields, and supports incremental updates.
    /// </summary>
    /// <typeparam name="TView">The concrete <see cref="IView"/> type being inspected.</typeparam>
    /// <typeparam name="TEditor">The corresponding <see cref="ViewEditor{T,TEditor}"/> type.</typeparam>
    public class ViewVisualElement<TView, TEditor> : VisualElement
        where TView : Object, IView
        where TEditor : ViewEditor<TView, TEditor>
    {
        private bool _isInitialized;
        protected readonly TEditor Editor;
        
        protected virtual MessageType MessageType => MessageType.None;
        
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
            
            var header = this.Q<AspidInspectorHeader>();
            header.SetMessageType(MessageType);
            header.Label.text = GetScriptName();
            
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
            
            Add(new CommandsContainer(Editor.TargetAsView));
            Add(BuildViewModel());
        }

        private AspidInspectorHeader BuildHeader() => 
            new(label: GetScriptName(), Editor.TargetAsView, MessageType);

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