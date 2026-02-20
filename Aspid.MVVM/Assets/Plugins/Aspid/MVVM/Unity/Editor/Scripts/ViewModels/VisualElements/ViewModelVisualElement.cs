#if !ASPID_MVVM_EDITOR_DISABLED
#nullable enable
using UnityEditor;
using System.Linq;
using Aspid.Internal;
using UnityEngine.UIElements;
using System.Collections.Generic;
using Aspid.UnityFastTools.Editors;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    // TODO Aspid.MVVM Unity – Write summary
    public class ViewModelVisualElement<TViewModel, TEditor> : VisualElement
        where TViewModel : Object, IViewModel
        where TEditor : ViewModelEditor<TViewModel, TEditor>
    {
        protected readonly TEditor Editor;
        private bool _isInitialized;
        
        protected virtual string IconPath => EditorConstants.AspidIconGreen;
        
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
                Add(onBuildHeader);
            }

            var baseInspector = BuildBaseInspector();
            if (baseInspector.style.display != DisplayStyle.None)
            {
                Add(baseInspector);
            }

            var onBuiltBaseInspector = OnBuiltBaseInspector();
            if (onBuiltBaseInspector is not null)
            {
                Add(onBuiltBaseInspector);
            }
            
            Add(BuiltCommands());
            
            var onBuiltCommands = OnBuiltCommands();
            if (onBuiltCommands is not null)
            {
                Add(onBuiltCommands);
            }
        }

        private AspidInspectorHeader BuildHeader() => 
            new(GetScriptName(), Editor.TargetAsViewModel, IconPath);

        protected virtual VisualElement? OnBuiltHeader() => null;

        private AspidBaseInspectorVisualElement BuildBaseInspector() =>
            new(SerializedObject, null, PropertiesExcluding.ToArray());

        protected virtual VisualElement? OnBuiltBaseInspector() => null;

        private CommandsContainer BuiltCommands() =>
            new(Editor.TargetAsViewModel);
        
        protected virtual VisualElement? OnBuiltCommands() => null;
        
        protected virtual string GetScriptName() =>
            Editor.TargetAsViewModel.GetScriptName();
    }
}
#endif