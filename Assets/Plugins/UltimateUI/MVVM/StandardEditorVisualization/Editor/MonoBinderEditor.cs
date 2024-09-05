using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UltimateUI.MVVM.Unity;
using UltimateUI.MVVM.Unity.Views;

namespace UltimateUI.MVVM.StandardEditorVisualization
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(MonoBinder), true)]
    public class MonoBinderEditor : BinderEditorBase<MonoBinder>
    {
        private const string IsShowLogKey = "IsShowLogKey";
        private const string ScrollLogPositionYKey = "ScrollLogPositionYKey";
    
        private const string ScriptPropertyName = "m_Script";

        private static readonly GUIContent _isDebugContent = new("Is Debug");
        private static readonly GUIContent _isShowLogContent = new("Is Show Log");

        private SerializedProperty _id;
        private SerializedProperty _view;

        private SerializedProperty _component;

        private SerializedProperty _log;
        private SerializedProperty _isDebug;

        protected virtual string[] PropertiesExcluding => new[]
        {
            _id.name,
            _view.name,
            _log?.name,
            _isDebug?.name,
            ScriptPropertyName,
        };

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
            {
                DrawId();
                DrawBase();

                DrawBeforeLog();
            
                DrawIsDebug();
                if (Application.isPlaying && _isDebug is { boolValue: true })
                    DrawLog();

                DrawAfterLog();
            }
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawId()
        {
            EditorGUILayout.BeginHorizontal();
            {
                var previousId = _id.stringValue;
                var previousView = _view.objectReferenceValue ? (MonoView)_view.objectReferenceValue : null;
                
                var drawnView = ViewPopup();
                _view.objectReferenceValue = drawnView;
                
                var id = IdPopup(drawnView);
                _id.stringValue = id;

                if (previousView?.GetInstanceID() != drawnView.GetInstanceID() || previousId != id)
                {
                    if (previousView)
                    {
                        ViewUtility.RemoveMonoBinderIfNotExist(previousView, Binder, previousId);
                    }
                    
                    ViewUtility.SetMonoBinderIfNotExist(drawnView, Binder, id);
                }
            }
            EditorGUILayout.EndHorizontal();
            return;

            MonoView ViewPopup()
            {
                var views = GetViewList();
                if (views.Length == 0) return null;
                
                var viewNames = views.Select(view => view.name).ToArray();
                var index = Popup(_view.objectReferenceValue, viewNames);

                return index == null ? null : views[index.Value];
            }
            
            string IdPopup(MonoView view)
            {
                if (!view) return null;
                
                var binderNames = GetIdList(view);
                var index = Popup(_id.stringValue, binderNames);
                
                return index == null ? null : binderNames[index.Value];
            }
        }
    
        private void DrawLog()
        {
            if (_log == null) return;

            var isShowLog = EditorPrefs.GetBool(IsShowLogKey);
            isShowLog = EditorGUILayout.Foldout(isShowLog, _isShowLogContent);
            EditorPrefs.SetBool(IsShowLogKey, isShowLog);
        
            if (!isShowLog) return;

            var count = _log.arraySize;
            var visibleLines = Mathf.Min(count, 5);
            var height = EditorGUIUtility.singleLineHeight * (visibleLines * 3);

            var scrollPosition = new Vector2(0, EditorPrefs.GetFloat(ScrollLogPositionYKey));
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(height));
            {
                for (var i = 0; i < count; i++)
                {
                    EditorGUILayout.BeginHorizontal(GUI.skin.box);
                    {
                        EditorGUILayout.HelpBox(_log.GetArrayElementAtIndex(i).stringValue, MessageType.Info);
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }
            EditorGUILayout.EndScrollView();
        
            EditorPrefs.SetFloat(ScrollLogPositionYKey, scrollPosition.y);
        }
    
        private void DrawIsDebug()
        {
            if (_isDebug == null) return;
            EditorGUILayout.PropertyField(_isDebug, _isDebugContent);
        }
    
        private void DrawBase() => 
            DrawPropertiesExcluding(serializedObject, PropertiesExcluding);

        protected virtual void DrawAfterLog() { }
    
        protected virtual void DrawBeforeLog() { }

        protected static int? Popup<T>(T value, string[] menus)
        {
            if (menus.Length == 0)
            {
                EditorGUILayout.Popup(0, new[] { "None" } );
                return null;
            }
            
            var index = Array.IndexOf(menus, value);
            if (index < 0) index = 0;

            index = EditorGUILayout.Popup(index, menus);
            return index;
        }
    }
}
