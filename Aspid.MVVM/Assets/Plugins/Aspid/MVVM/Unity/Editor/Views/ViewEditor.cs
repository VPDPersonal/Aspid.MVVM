#if !ASPID_MVVM_EDITOR_DISABLED
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

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

        protected virtual void OnEnable() =>
            EditorApplication.update += Update;

        protected virtual void OnDisable() =>
            EditorApplication.update -= Update;

        protected virtual void Update() => Root?.Update();

        public sealed override VisualElement CreateInspectorGUI()
        {
            OnCreatingInspectorGUI();
            {
                Root = BuildVisualElement();
                Root.Initialize();
            }
            OnCreatedInspectorGUI();
            
            return Root;
        }

        protected abstract ViewVisualElement<T, TEditor> BuildVisualElement();

        protected virtual void OnCreatingInspectorGUI() { }
        
        protected virtual void OnCreatedInspectorGUI() { }
    }
}
#endif