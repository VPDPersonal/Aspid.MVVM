#if !ASPID_MVVM_EDITOR_DISABLED
#nullable enable
using UnityEditor;
using System.Linq;
using UnityEngine.UIElements;
using Aspid.FastTools.Editors;
using Aspid.FastTools.UIElements;
using System.Collections.Generic;
using Aspid.FastTools.UIElements.Editors.Internal;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// <see cref="VisualElement"/> with a ViewModel header, serialized fields and commands.
    /// </summary>
    /// <typeparam name="TViewModel">The inspected Unity ViewModel type.</typeparam>
    /// <typeparam name="TEditor">The corresponding ViewModel editor type.</typeparam>
    public class ViewModelVisualElement<TViewModel, TEditor> : VisualElement
        where TViewModel : Object, IViewModel
        where TEditor : ViewModelEditor<TViewModel, TEditor>
    {
        /// <summary>
        /// The editor owning this Inspector.
        /// </summary>
        protected readonly TEditor Editor;
        private bool _isInitialized;
        
        /// <summary>
        /// Gets the serialized property names excluded from the Inspector; excludes <c>m_Script</c> by default.
        /// </summary>
        protected virtual IEnumerable<string> PropertiesExcluding
        {
            get
            {
                yield return "m_Script";
            }
        }
        
        /// <summary>
        /// Gets the serialized object inspected by the editor.
        /// </summary>
        protected SerializedObject SerializedObject => Editor.serializedObject;
        
        /// <summary>
        /// Creates an uninitialized Inspector for an editor.
        /// </summary>
        /// <param name="editor">The editor owning the Inspector.</param>
        public ViewModelVisualElement(TEditor editor)
        {
            Editor = editor;
        }

        /// <summary>
        /// Builds the Inspector once; later calls do nothing.
        /// </summary>
        public void Initialize()
        {
            if (_isInitialized) return;

            Build();
            _isInitialized = true;
        }
        
        /// <summary>
        /// Refreshes the Inspector through <see cref="OnUpdate"/>.
        /// </summary>
        public void Update()
        {
            OnUpdate();
        }
        
        /// <summary>
        /// Called on each Inspector refresh. Override to update custom elements.
        /// </summary>
        protected virtual void OnUpdate() { }

        private void Build()
        {
            this.AddStyleSheetFromResources(AspidStyles.DefaultStyleSheet)
                .AddClass(AspidStyles.InspectorStyleClass);

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
            new AspidInspectorHeader(GetScriptName(), Editor.TargetAsViewModel) { Subtext = GetScriptSubtext() }
                .SetMargin(top: 3);

        /// <summary>
        /// Called after building the header. Override to append an element.
        /// </summary>
        /// <returns>The element to append; <see langword="null"/> for none.</returns>
        protected virtual VisualElement? OnBuiltHeader() => null;

        private AspidBaseInspectorVisualElement BuildBaseInspector() =>
            new AspidBaseInspectorVisualElement(SerializedObject, null, PropertiesExcluding.ToArray())
                .SetMargin(top: 5);

        /// <summary>
        /// Called after building the serialized fields. Override to append an element.
        /// </summary>
        /// <returns>The element to append; <see langword="null"/> for none.</returns>
        protected virtual VisualElement? OnBuiltBaseInspector() => null;

        private CommandsContainer BuiltCommands() =>
            new(Editor.TargetAsViewModel);
        
        /// <summary>
        /// Called after building the commands. Override to append an element.
        /// </summary>
        /// <returns>The element to append; <see langword="null"/> for none.</returns>
        protected virtual VisualElement? OnBuiltCommands() => null;
        
        /// <summary>
        /// Called while building the header. Override to supply the header name.
        /// </summary>
        /// <returns>The header name; the ViewModel display name by default.</returns>
        protected virtual string GetScriptName() =>
            Editor.TargetAsViewModel.GetDisplayName();

        /// <summary>
        /// Called while building the header. Override to supply the header subtext.
        /// </summary>
        /// <returns>The header subtext; an empty string by default.</returns>
        protected virtual string GetScriptSubtext() =>
            string.Empty;
    }
}
#endif