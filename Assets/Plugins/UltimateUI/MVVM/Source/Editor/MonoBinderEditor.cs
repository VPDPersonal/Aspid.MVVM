using UnityEditor;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Unity
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(MonoBinder), true)]
    public class MonoBinderEditor : Editor
    {
        protected MonoBinder Binder => (MonoBinder)target;
        
        public sealed override void OnInspectorGUI() =>
            DrawInspector();
        
        protected virtual void DrawInspector()
        {
            DrawBaseInspector();
            DrawRebindButton();
        }
        
        protected void DrawBaseInspector() =>
            base.OnInspectorGUI();

        protected void DrawRebindButton()
        {
#if !ULTIMATE_UI_EDITOR_DISABLED
            if (!GUILayout.Button("Rebind")) return;

            serializedObject.UpdateIfRequiredOrScript();
            Binder.RebindOnlyEditor();
            serializedObject.ApplyModifiedProperties();
#endif
        }
    }
}