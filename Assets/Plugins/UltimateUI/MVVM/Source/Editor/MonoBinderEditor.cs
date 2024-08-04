using System;
using System.IO;
using System.Linq;
using UltimateUI.MVVM.Unity.Views;
using UnityEditor;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Unity
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(MonoBinder), true)]
    public class MonoBinderEditor : MonoBinderEditorBase<MonoBinder>
    {
        private const string ScriptPropertyName = "m_Script";
        
        private SerializedProperty _id;
        private SerializedProperty _view;
        
        private SerializedProperty _log;
        private SerializedProperty _isDebug;

        private bool _showLog;
        private Vector2 _scrollPosition;
        
        protected MonoBinder Binder => (MonoBinder)target;

        protected virtual void OnEnable()
        {
            _id = serializedObject.FindProperty(nameof(_id));
            _view = serializedObject.FindProperty(nameof(_view));
            
            _log = serializedObject.FindProperty(nameof(_log));
            _isDebug = serializedObject.FindProperty(nameof(_isDebug));
        }
        
        public sealed override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawSource();
            DrawInspector();
            DrawLog();
            DrawRebindButton();
            
            serializedObject.ApplyModifiedProperties();
        }
        
        protected virtual void DrawInspector()
        {
            DrawBaseInspector();
        }

        private void DrawSource()
        {
            EditorGUILayout.BeginHorizontal();
            {
                var drawnView = ViewPopup();
                _view.objectReferenceValue = drawnView;

                var id = IdPopup(drawnView);
                _id.stringValue = id;
            }
            EditorGUILayout.EndHorizontal();
            return;

            MonoView ViewPopup()
            {
                var views = GetViewList();
                if (views.Length == 0) return null;
                
                var viewNames = views.Select(view => view.name).ToArray();
                var index = Popup(_view.objectReferenceValue, viewNames, true);

                return index == null ? null : views[index.Value];
            }
            
            string IdPopup(MonoView view)
            {
                if (!view) return null;
                
                var binderNames = GetIdList(view);
                var index = Popup(_id.stringValue, binderNames, true);
                
                return index == null ? null : binderNames[index.Value];
            }
        }
        
        private void DrawLog()
        {
            if (_log == null) return;
            _showLog = EditorGUILayout.Foldout(_showLog, "Log");

            if (_showLog)
            {
                var logCount = _log.arraySize;

                var lineHeight = EditorGUIUtility.singleLineHeight;
                float visibleLines = Mathf.Min(logCount, 10);
                var height = lineHeight * visibleLines + 20; // Увеличиваем высоту для скроллбара

                // Отображаем ScrollView с фиксированной высотой
                _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, GUILayout.Height(height));

                for (var i = 0; i < logCount; i++)
                {
                    EditorGUILayout.BeginHorizontal(GUI.skin.box);
                    EditorGUILayout.HelpBox(i + ": " + _log.GetArrayElementAtIndex(i).stringValue, MessageType.Info);
                    EditorGUILayout.EndHorizontal();
                }

                EditorGUILayout.EndScrollView();
            }
        }
        
        protected virtual void DrawBaseInspector()
        {
            DrawPropertiesExcluding(serializedObject, ScriptPropertyName, _view.name, _id.name, _log?.name);
        }

        protected void DrawRebindButton()
        {
#if !ULTIMATE_UI_EDITOR_DISABLED
            if (!GUILayout.Button("Rebind")) return;

            serializedObject.UpdateIfRequiredOrScript();
            Binder.RebindOnlyEditor();
            serializedObject.ApplyModifiedProperties();
#endif
        }
        
        protected static int? Popup<T>(T value, string[] menus, bool isNoneExists = false)
        {
            if (menus.Length == 0)
            {
                EditorGUILayout.Popup(0, isNoneExists ? new[] { "None" } : Array.Empty<string>());
                return null;
            }
            
            var index = Array.IndexOf(menus, value);
            if (index < 0) index = 0;
            
            index = EditorGUILayout.Popup(index, menus);
            return index;
        }
    }
}