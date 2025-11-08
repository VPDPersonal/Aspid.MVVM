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
        public T TargetAsSpecificView => target as T;
        
        protected ViewVisualElement<T, TEditor> Root { get; private set; }
        
        public sealed override VisualElement CreateInspectorGUI()
        {
            OnCreatingInspectorGUI();
            {
                Root = BuildVisualElement();
                Root.Initialize();

                Root.RegisterCallbackOnce<GeometryChangedEvent>(_ => Root.Update());
                Root.RegisterCallback<SerializedPropertyChangeEvent>(OnSerializedPropertyChange);
            }
            OnCreatedInspectorGUI();
            
            return Root;
        }

        protected abstract ViewVisualElement<T, TEditor> BuildVisualElement();

        protected virtual void OnCreatingInspectorGUI() { }
        
        protected virtual void OnCreatedInspectorGUI() { }

        protected virtual void OnSerializedPropertyChange(SerializedPropertyChangeEvent e) =>
            Root.Update();
    }
}