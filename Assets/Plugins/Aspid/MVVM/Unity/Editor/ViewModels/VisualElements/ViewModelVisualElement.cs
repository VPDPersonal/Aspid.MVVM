#nullable enable
using UnityEditor;
using Aspid.UnityFastTools;
using UnityEngine.UIElements;
using System.Collections.Generic;
using Aspid.UnityFastTools.Editors;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public class ViewModelVisualElement<TViewModel, TEditor> : VisualElement
        where TViewModel : Object, IViewModel
        where TEditor : ViewModelEditor<TViewModel, TEditor>
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
        
        public ViewModelVisualElement(TEditor editor)
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
        }
        
        protected virtual void OnUpdate() { }

        private void Build()
        {
            Add(BuildHeader());
            
            var onBuildHeader = OnBuiltHeader();
            if (onBuildHeader is not null)
            {
                Add(onBuildHeader
                    .SetMargin(top: 10));
            }

            var baseInspector = BuildBaseInspector();
            if (baseInspector.style.display != DisplayStyle.None)
            {
                baseInspector.SetMargin(top: 10);
                Add(BuildBaseInspector());
            }

            var onBuiltBaseInspector = OnBuiltBaseInspector();
            if (onBuiltBaseInspector is not null)
            {
                Add(onBuiltBaseInspector
                    .SetMargin(top: 10));
            }
            
            Add(BuiltCommands()
                .SetMargin(top: 10));
            
            var onBuiltCommands = OnBuiltCommands();
            if (onBuiltCommands is not null)
            {
                Add(onBuiltCommands
                    .SetMargin(top: 10));
            }
        }

        private InspectorHeaderPanel BuildHeader() => 
            new(GetScriptName(), Editor.TargetAsViewModel, IconPath);

        protected virtual VisualElement? OnBuiltHeader() => null;

        private BaseInspectorVisualElement BuildBaseInspector() =>
            new(SerializedObject, PropertiesExcluding);

        protected virtual VisualElement? OnBuiltBaseInspector() => null;

        private CommandsContainer BuiltCommands() =>
            new(Editor.TargetAsViewModel);
        
        protected virtual VisualElement? OnBuiltCommands() => null;
        
        protected virtual string GetScriptName() =>
            Editor.TargetAsViewModel.GetScriptName();
    }
}