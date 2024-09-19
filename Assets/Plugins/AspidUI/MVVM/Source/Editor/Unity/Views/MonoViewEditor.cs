using System.Linq;
using UnityEditor;
using UnityEngine;

namespace AspidUI.MVVM.Unity.Views
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(MonoView), editorForChildClasses: true)]
    public class MonoViewEditor : Editor
    {
        private bool _isShowOtherPosition;
        
        protected MonoView View => (MonoView)target;

        private void OnEnable()
        {
            ViewUtility.FindAllMonoBinderValidableInChildren(View);
        }

        public sealed override void OnInspectorGUI() =>
            DrawInspector();
        
        protected virtual void DrawInspector()
        {
            serializedObject.Update();
            var oldBindersDictionary = ViewUtility.GetMonoBinderValidableWithFieldName(View);
            
            DrawBaseInspector();
            
            var newBindersDictionary = ViewUtility.GetMonoBinderValidableWithFieldName(View);
            ViewUtility.ValidateMonoBinderValidablesInView(View, oldBindersDictionary, newBindersDictionary);

            var binders = View.GetComponentsInChildren<IMonoBinderValidable>(true)
                .Where(binder => string.IsNullOrEmpty(binder.Id)).ToArray();

            if (binders.Length > 0)
            {
                EditorGUILayout.Space();
                _isShowOtherPosition = EditorGUILayout.Foldout(_isShowOtherPosition, "Other Binders");
            
                GUI.enabled = false;
                if (_isShowOtherPosition)
                {
                    foreach (var binder in binders)
                        EditorGUILayout.ObjectField((Component)binder, binder.GetType(), false);
                }
                GUI.enabled = true;
            }
            serializedObject.ApplyModifiedProperties();
        }
        
        protected void DrawBaseInspector() =>
            base.OnInspectorGUI();
    }
}