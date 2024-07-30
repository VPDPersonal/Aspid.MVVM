using UnityEditor;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Unity.Views
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(MonoView), editorForChildClasses: true)]
    public class MonoViewEditor : Editor
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
#if !ULTIMATE_UI_EDITOR_DISABLED
            if (!GUILayout.Button("Find All Binders")) return;
            
            serializedObject.UpdateIfRequiredOrScript();
            ViewUtility.FindAllBindersInChildren(View);
            serializedObject.ApplyModifiedProperties();
#endif
        }
    }
}