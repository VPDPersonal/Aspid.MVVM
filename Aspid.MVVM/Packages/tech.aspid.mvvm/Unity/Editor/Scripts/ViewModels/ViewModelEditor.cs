#if !ASPID_MVVM_EDITOR_DISABLED
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// <see cref="Editor"/> that builds a ViewModel Inspector with lifecycle hooks.
    /// </summary>
    /// <typeparam name="T">The inspected Unity ViewModel type.</typeparam>
    /// <typeparam name="TEditor">The derived editor type.</typeparam>
    public abstract class ViewModelEditor<T, TEditor> : Editor
        where T : Object, IViewModel
        where TEditor : ViewModelEditor<T, TEditor>
    {
        /// <summary>
        /// Gets the inspected object as the ViewModel type.
        /// </summary>
        public T TargetAsViewModel => target as T;
        
        /// <summary>
        /// Gets the Inspector root after it is created.
        /// </summary>
        protected ViewModelVisualElement<T, TEditor> Root { get; private set; }
        
        /// <summary>
        /// Called when Unity creates the Inspector to build and initialize its root.
        /// </summary>
        /// <returns>The initialized Inspector root.</returns>
        public sealed override VisualElement CreateInspectorGUI()
        {
            OnCreatingInspectorGUI();
            {
                Root = BuildVisualElement();
                Root.Initialize();

                Root.RegisterCallback<GeometryChangedEvent>(OnGeometryChangedOnceInternal);
                Root.RegisterCallback<SerializedPropertyChangeEvent>(OnSerializedPropertyChanged);
            }
            OnCreatedInspectorGUI();
            
            return Root;
        }

        /// <summary>
        /// Called while creating the Inspector. Override to create its root element.
        /// </summary>
        /// <returns>The uninitialized Inspector root.</returns>
        protected abstract ViewModelVisualElement<T, TEditor> BuildVisualElement();

        /// <summary>
        /// Called before the Inspector root is built. Override to prepare editor state.
        /// </summary>
        protected virtual void OnCreatingInspectorGUI() { }
        
        /// <summary>
        /// Called after the Inspector root is initialized. Override to finish editor setup.
        /// </summary>
        protected virtual void OnCreatedInspectorGUI() { }

        private void OnGeometryChangedOnceInternal(GeometryChangedEvent e)
        {
            OnGeometryChangedOnce(e);
            Root.UnregisterCallback<GeometryChangedEvent>(OnGeometryChangedOnceInternal);
        }
        
        /// <summary>
        /// Called after the first layout to update the root. Override to customize the initial refresh.
        /// </summary>
        /// <param name="e">The first geometry change event.</param>
        protected virtual void OnGeometryChangedOnce(GeometryChangedEvent e) =>
            Root.Update();
        
        /// <summary>
        /// Called after a serialized property changes to update the root. Override to customize refreshing.
        /// </summary>
        /// <param name="e">The serialized property change event.</param>
        protected virtual void OnSerializedPropertyChanged(SerializedPropertyChangeEvent e) =>
            Root.Update();
    }
}
#endif