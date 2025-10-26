using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public abstract class ViewModelEditor<T, TEditor> : Editor
        where T : Object, IViewModel
        where TEditor : ViewModelEditor<T, TEditor>
    {
        public T TargetAsViewModel => target as T;
        
        protected ViewModelVisualElement<T, TEditor> Root { get; private set; }
        
        public sealed override VisualElement CreateInspectorGUI()
        {
            Root = BuildVisualElement();
            Root.Initialize();
            
            OnCreatedInspectorGUI(Root);
            return Root;
        }

        protected abstract ViewModelVisualElement<T, TEditor> BuildVisualElement();

        protected virtual void OnCreatedInspectorGUI(ViewModelVisualElement<T, TEditor> root)
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