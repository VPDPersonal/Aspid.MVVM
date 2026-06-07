#if !ASPID_MVVM_EDITOR_DISABLED
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Abstract base Unity Editor for <see cref="IViewModel"/> objects.
    /// Manages the UIElements inspector lifecycle and delegates visual element construction to derived editors.
    /// </summary>
    /// <typeparam name="T">The concrete <see cref="IViewModel"/> type being inspected.</typeparam>
    /// <typeparam name="TEditor">The derived editor type (self-referencing).</typeparam>
    public abstract class ViewModelEditor<T, TEditor> : Editor
        where T : Object, IViewModel
        where TEditor : ViewModelEditor<T, TEditor>
    {
        public T TargetAsViewModel => target as T;
        
        protected ViewModelVisualElement<T, TEditor> Root { get; private set; }
        
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

        protected abstract ViewModelVisualElement<T, TEditor> BuildVisualElement();

        protected virtual void OnCreatingInspectorGUI() { }
        
        protected virtual void OnCreatedInspectorGUI() { }

        private void OnGeometryChangedOnceInternal(GeometryChangedEvent e)
        {
            OnGeometryChangedOnce(e);
            Root.UnregisterCallback<GeometryChangedEvent>(OnGeometryChangedOnceInternal);
        }
        
        protected virtual void OnGeometryChangedOnce(GeometryChangedEvent e) =>
            Root.Update();
        
        protected virtual void OnSerializedPropertyChanged(SerializedPropertyChangeEvent e) =>
            Root.Update();
    }
}
#endif