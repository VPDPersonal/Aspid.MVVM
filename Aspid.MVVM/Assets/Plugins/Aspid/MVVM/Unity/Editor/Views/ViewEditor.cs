#if !ASPID_MVVM_EDITOR_DISABLED
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    // TODO Aspid.MVVM Unity – Write summary
    public abstract class ViewEditor<T, TEditor> : Editor
        where T : Object, IView
        where TEditor : ViewEditor<T, TEditor>
    {
        public T TargetAsView => target as T;
        
        protected ViewVisualElement<T, TEditor> Root { get; private set; }
        
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

        protected abstract ViewVisualElement<T, TEditor> BuildVisualElement();

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