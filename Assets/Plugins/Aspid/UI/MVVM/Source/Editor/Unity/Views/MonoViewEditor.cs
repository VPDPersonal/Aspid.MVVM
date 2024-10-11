using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using System.Reflection;
using System.Collections;
using Aspid.UI.MVVM.Mono;
using Aspid.UI.MVVM.Mono.Views;
using System.Collections.Generic;
using Aspid.UI.MVVM.Unity.Views.Extensions;
using Object = UnityEngine.Object;

namespace Aspid.UI.MVVM.Unity.Views
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
            
            DrawViewModelData();
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawViewModelData()
        {
            GUI.enabled = false;
            EditorGUILayout.Space();
            var property = target.GetType().GetProperty("ViewModel", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)?.GetValue(target);

            var viewModelName = property is null ? "View Model" : $"View Model ({property.GetType().Namespace}.{property.GetType().Name})";
            DrawCompositeValue(property, viewModelName, ignoreNicify: true);
            GUI.enabled = true;
        }
        
        private void DrawValue(object obj, string fieldName, string parentFieldName)
        {
            var label = ObjectNames.NicifyVariableName(fieldName);
            
            if (obj == null)
            {
                EditorGUILayout.TextField(label, "null");
                return;
            }
            
            switch (obj)
            {
                case int intValue: EditorGUILayout.IntField(label, intValue); break;
                case bool boolValue: EditorGUILayout.Toggle(label, boolValue); break;
                case long longValue: EditorGUILayout.LongField(label, longValue); break;
                case Rect rectValue: EditorGUILayout.RectField(label, rectValue); break;
                case Color colorValue: EditorGUILayout.ColorField(label, colorValue); break;
                case float floatValue: EditorGUILayout.FloatField(label, floatValue); break;
                case Enum enumValue: EditorGUILayout.EnumFlagsField(label, enumValue); break;
                case double doubleValue: EditorGUILayout.DoubleField(label, doubleValue); break;
                case Bounds boundsValue: EditorGUILayout.BoundsField(label, boundsValue); break;
                case string stringValue: EditorGUILayout.TextField(label, stringValue); break;
                case RectInt rectIntValue: EditorGUILayout.RectIntField(label, rectIntValue); break;
                case Vector2 vector2Value: EditorGUILayout.Vector2Field(label, vector2Value); break;
                case Vector3 vector3Value: EditorGUILayout.Vector3Field(label, vector3Value); break;
                case Vector4 vector4Value: EditorGUILayout.Vector4Field(label, vector4Value); break;
                case Gradient gradientValue: EditorGUILayout.GradientField(label, gradientValue); break;
                case BoundsInt boundsIntValue: EditorGUILayout.BoundsIntField(label, boundsIntValue); break;
                case Vector2Int vector2IntValue: EditorGUILayout.Vector2IntField(label, vector2IntValue); break;
                case Vector3Int vector3IntValue: EditorGUILayout.Vector3IntField(label, vector3IntValue); break;
                case AnimationCurve animationCurveValue: EditorGUILayout.CurveField(label, animationCurveValue); break;
                case Object unityObjValue: EditorGUILayout.ObjectField(label, unityObjValue, unityObjValue.GetType()); break;
                case Delegate delegateValue: 
                    {
                        if (!Foldout($"{parentFieldName}.{fieldName}", fieldName)) break;

                        EditorGUI.indentLevel++;
                        {
                            var indentLevel = EditorGUI.indentLevel;
                            var style = new GUIStyle(EditorStyles.helpBox)
                            {
                                margin = new RectOffset((EditorGUI.indentLevel + 1) * 15, 0, 0, 0),
                            };
                            EditorGUI.indentLevel = 0;

                            foreach (var @delegate in delegateValue.GetInvocationList())
                            {
                                EditorGUILayout.BeginVertical(style);
                                {
	                                if (delegateValue.Method.DeclaringType is null)
	                                {
		                                EditorGUILayout.TextField("null");
		                                EditorGUILayout.EndHorizontal();
		                                continue;
	                                }

	                                var targetType = @delegate.Method.DeclaringType!;
                                    var targetName = targetType.Name;

                                    if (@delegate.Target is Component component)
                                    {
                                        EditorGUILayout.ObjectField(component.gameObject, targetType, true);
                                    }
                                    else
                                    {
                                        EditorGUILayout.BeginHorizontal();
                                        {
                                            var targetNamespace = targetType.Namespace;
                                            EditorGUILayout.LabelField("Type", GUILayout.Width(45));
                                            EditorGUILayout.TextField($"{targetNamespace}.{targetName}");
                                        }
                                        EditorGUILayout.EndHorizontal();
                                    }

                                    EditorGUILayout.BeginHorizontal();
                                    {
                                        EditorGUILayout.LabelField("Method", GUILayout.Width(45));
                                        EditorGUILayout.TextField(@delegate.Method.Name);
                                    }
                                    EditorGUILayout.EndHorizontal();
                                }
                                EditorGUILayout.EndVertical();
                            }
                            
                            EditorGUI.indentLevel = indentLevel;
                        }
                        EditorGUI.indentLevel--;
                    }
                    break;
                case IEnumerable enumerable:
                    {
                        if (!Foldout($"{parentFieldName}.{fieldName}", fieldName)) break;

                        EditorGUI.indentLevel++;
                        {
                            if (IsKeyValuePairInEnumerable(enumerable))
                            {
                                foreach (var item in enumerable)
                                {
                                    var keyProperty = item.GetType().GetProperty("Key");
                                    var valueProperty = item.GetType().GetProperty("Value");

                                    var key = keyProperty!.GetValue(item).ToString();
                                    var value = valueProperty!.GetValue(item);

                                    if (!Foldout($"{parentFieldName}.{fieldName}.{key}", key)) continue;
                                    
                                    EditorGUI.indentLevel++;
                                    {
                                        DrawValue(value, "Value", $"{parentFieldName}.{fieldName}.{key}");
                                    }
                                    EditorGUI.indentLevel--;
                                }
                            }
                            else
                            {
                                var index = 0;
                                foreach (var item in enumerable)
                                {
                                    DrawValue(item, index.ToString(), parentFieldName);
                                    index++;
                                }

                            }
                        }
                        EditorGUI.indentLevel--;
                    }
                    break;
                default:
                    {
                        DrawCompositeValue(obj, fieldName, parentFieldName);
                        break;
                    }
            }
            return;

            bool IsKeyValuePairInEnumerable(object obj)
            {
                return obj.GetType().GetInterfaces().Any(@interface =>
                {
                    if (!@interface.IsGenericType) return false;
                    if (!typeof(IEnumerable<>).IsAssignableFrom(@interface.GetGenericTypeDefinition())) return false;

                    var interfaceArgument = @interface.GetGenericArguments()[0];
                    return interfaceArgument.IsGenericType && typeof(KeyValuePair<,>).IsAssignableFrom(interfaceArgument.GetGenericTypeDefinition());
                });
            }
        }

        private void DrawCompositeValue(object obj, string fieldName, string parentFieldName = "", bool ignoreNicify = false)
        {
            if (obj == null)
            {
                EditorGUILayout.TextField(fieldName, "null");
                return;
            }

            var prefsKey = string.IsNullOrEmpty(parentFieldName) ? $"{parentFieldName}.{fieldName}" : $"{fieldName}";
            if (!Foldout(prefsKey, fieldName, ignoreNicify)) return;

            EditorGUI.indentLevel++;
            {
                var fields = obj.GetType().GetFieldInfosIncludingBaseClasses(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                foreach (var field in fields)
                    DrawValue(field.GetValue(obj), field.Name, prefsKey);
            }
            EditorGUI.indentLevel--;
        }

        private static bool Foldout(string prefsKey, string fieldName, bool ignoreNicify = false)
        {
            var prefsValue = EditorPrefs.GetBool(prefsKey, false);

            prefsValue = EditorGUILayout.Foldout(prefsValue, ignoreNicify ? fieldName : ObjectNames.NicifyVariableName(fieldName));
            EditorPrefs.SetBool(prefsKey, prefsValue);

            return prefsValue;
        }
        
        protected void DrawBaseInspector() =>
            base.OnInspectorGUI();
    }

}