#if !ASPID_MVVM_EDITOR_DISABLED
using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
#if !UNITY_6000_0_OR_NEWER
using Aspid.UnityFastTools;
#endif

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

        #region Unity Functions
        protected virtual void OnEnable() =>
            EditorApplication.update += Update;

        protected virtual void OnDisable() =>
            EditorApplication.update -= Update;

        protected virtual void Update() => Root?.Update();
        #endregion

        #region CreateInspectorGUI
        public sealed override VisualElement CreateInspectorGUI()
        {
            OnCreatingInspectorGUI();
            {
                Root = BuildVisualElement();
                Root.Initialize();
                
                Root.RegisterCallbackOnce<GeometryChangedEvent>(OnGeometryChangedEventOnce);
                Root.RegisterCallback<SerializedPropertyChangeEvent>(OnSerializedPropertyChangedInternal);
            }
            OnCreatedInspectorGUI();
            
            return Root;
        }
        
        protected virtual void OnCreatingInspectorGUI() { }
        
        protected virtual void OnCreatedInspectorGUI() { }
        #endregion

        protected abstract ViewVisualElement<T, TEditor> BuildVisualElement();

        #region Event Handlers
        private void OnSerializedPropertyChangedInternal(SerializedPropertyChangeEvent e)
        {
            var property = e.changedProperty;
            
            // If property is last element in an array then return.
            if (property.propertyPath[^1] is ']') return;

            OnSerializedPropertyChanged(e);
        }
        
        protected virtual void OnSerializedPropertyChanged(SerializedPropertyChangeEvent e) { }
        
        protected virtual void OnGeometryChangedEventOnce(GeometryChangedEvent e) { }
        #endregion
    }
}
#endif