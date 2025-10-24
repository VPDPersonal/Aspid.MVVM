using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public abstract class ViewEditor<T, TEditor> : Editor
        where T : Object, IView
        where TEditor : ViewEditor<T, TEditor>
    {
        public T TargetAsSpecific => target as T;
        
        protected ViewVisualElement<T, TEditor> Root { get; private set; }
        
        public sealed override VisualElement CreateInspectorGUI()
        {
            Root = BuildVisualElement();
            Root.Initialize();
            
            OnCreatedInspectorGUI(Root);
            return Root;
        }

        protected abstract ViewVisualElement<T, TEditor> BuildVisualElement();

        protected virtual void OnCreatedInspectorGUI(ViewVisualElement<T, TEditor> root)
        {
            root.RegisterCallbackOnce<GeometryChangedEvent>(_ =>
            {
                root.Update();
            });
            
            root.RegisterCallback<SerializedPropertyChangeEvent>(_ =>
            {
                root.Update();
            });
        }
    }
}