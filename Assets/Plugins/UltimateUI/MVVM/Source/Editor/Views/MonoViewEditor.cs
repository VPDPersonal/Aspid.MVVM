using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UltimateUI.MVVM.Unity;
using UltimateUI.MVVM.Unity.Views;
using UnityEditor;
using UnityEngine;

namespace UltimateUI.MVVM.Views
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(MonoView), editorForChildClasses: true)]
    public class MonoViewEditor : Editor
    {
        private bool _isShowOtherPosition;
        
        protected MonoView View => (MonoView)target;

        private void OnEnable()
        {
            ViewUtility.FindAllBindersInChildren(View);
        }

        public sealed override void OnInspectorGUI() =>
            DrawInspector();
        
        protected virtual void DrawInspector()
        {
            var oldBindersDictionary = GetMonoBinderValues();
            
            DrawBaseInspector();
            
            var newBindersDictionary = GetMonoBinderValues();
            var binderFields = GetMonoBinderFields();
            var changedBinderFields = GetChangedFields(oldBindersDictionary, newBindersDictionary);
            
            foreach (var changedField in changedBinderFields)
            {
                if (changedField.Value is { oldField: MonoBinder[] oldBinders, newField: MonoBinder[] newBinders })
                {
                    // Если Удалили какие-то binder, то очищаем удаленные binder
                    foreach (var oldBinder in oldBinders)
                    {
                        if (!oldBinder) continue;
                        var isExist = newBinders.Contains(oldBinder);
                        if (isExist) continue;
                            
                        oldBinder.Id = null;
                        oldBinder.View = null;
                    }

                    var addedBinders = newBinders.Where(newBinder => !oldBinders.Contains(newBinder));

                    foreach (var addedBinder in addedBinders)
                    {
                        if (!addedBinder) continue;
                        if (addedBinder.Id == null) continue;
                        
                        if (binderFields.TryGetValue(addedBinder.Id, out var field))
                            DeleteValue(addedBinder, field);
                    }
                }
            }

            DrawFindAllBindersButton();
            ViewUtility.ValidateMonoBinders(View);

            var binders = View.GetComponentsInChildren<MonoBinder>()
                .Where(binder => binder.Id == "None" || string.IsNullOrEmpty(binder.Id)).ToArray();

            if (binders.Length > 0)
            {
                EditorGUILayout.Space();
                _isShowOtherPosition = EditorGUILayout.Foldout(_isShowOtherPosition, "Other Binders");
            
                GUI.enabled = false;
                if (_isShowOtherPosition)
                {
                    foreach (var binder in binders)
                        EditorGUILayout.ObjectField(binder, binder.GetType(), false);
                }
                GUI.enabled = true;
            }
        }
        
        protected void DrawBaseInspector() =>
            base.OnInspectorGUI();

        private void DeleteValue(MonoBinder deleteBinder, FieldInfo field)
        {
            var isArray = field.FieldType.IsArray;
            var binders = isArray 
                ? (MonoBinder[])field.GetValue(View)
                : new[] { (MonoBinder)field.GetValue(null) };
            
            binders = binders.Where(binder => !binder.Equals(deleteBinder)).ToArray();
            field.SetValue(View, isArray ? binders : binders.FirstOrDefault());
        }
        
        protected bool DrawFindAllBindersButton()
        {
            if (!GUILayout.Button("Find All Binders")) return false;
            
            serializedObject.UpdateIfRequiredOrScript();
            ViewUtility.FindAllBindersInChildren(View);
            serializedObject.ApplyModifiedProperties();
            
            return true;
        }
        
        private Dictionary<string, object> GetMonoBinderValues()
        {
            var fields = ViewUtility.GetMonoBinderFields(View.GetType());
            return fields.ToDictionary(field => ViewUtility.GetPropertyName(field.Name), field => field.GetValue(View));
        }

        private Dictionary<string, FieldInfo> GetMonoBinderFields()
        {
            return ViewUtility.GetMonoBinderFields(View.GetType())
                .ToDictionary(field => ViewUtility.GetPropertyName(field.Name), field => field);
        }

        private static IReadOnlyDictionary<string, (object oldField, object newField)> GetChangedFields(
            IReadOnlyDictionary<string, object> oldFields,
            IReadOnlyDictionary<string, object> newFields)
        {
            var changedFields = new Dictionary<string, (object oldField, object newField)>();

            foreach (var key in oldFields.Keys)
            {
                var oldValue = oldFields[key];
                var newValue = newFields[key];
                if (oldValue == newValue) continue;
                
                changedFields.Add(key, (oldValue, newValue));
            }

            return changedFields;
        }
    }
}