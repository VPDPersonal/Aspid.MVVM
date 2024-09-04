using UltimateUI.MVVM.Unity.Views;
using UnityEditor;
using UnityEngine;

namespace UltimateUI.MVVM.Views
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