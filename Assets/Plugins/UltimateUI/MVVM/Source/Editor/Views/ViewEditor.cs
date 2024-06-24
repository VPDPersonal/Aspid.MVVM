using UnityEditor;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Views
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(MonoView), editorForChildClasses: true)]
    public class ViewEditor : Editor
    {
        protected MonoView View => (MonoView)target;
        
        public sealed override void OnInspectorGUI() =>
            DrawInspector();
        
        protected virtual void DrawInspector()
        {
            DrawBaseInspector();
            DrawFindAllBindersButton();
        }
        
        protected void DrawBaseInspector() =>
            base.OnInspectorGUI();
        
        protected void DrawFindAllBindersButton()
        {
            if (GUILayout.Button("Find All Binders"))
                ViewUtility.FindAllBinders(View, View.GetComponentsInChildren<MonoBinder>());
        }
    }
}