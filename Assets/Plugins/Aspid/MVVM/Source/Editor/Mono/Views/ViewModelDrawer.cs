using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using System.Reflection;
using System.Collections;
using Aspid.CustomEditors;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace Aspid.MVVM.Mono
{
    public static class ViewModelDrawer
    {
        public static void DrawViewModelData(Object target)
        {
            EditorGUILayout.Space();
            var property = target.GetType().GetProperty("ViewModel", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)?.GetValue(target);

            var viewModelName = property is null ? "View Model" : $"View Model ({property.GetType().Namespace}.{property.GetType().Name})";
            DrawCompositeValue(property, viewModelName, ignoreNicify: true);
        }
            
        private static void DrawValue(object obj, string fieldName, string parentFieldName)
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
                                using (AspidEditorGUILayout.BeginVertical(style))
                                {
                                    if (delegateValue.Method.DeclaringType is null)
                                    {
                                        EditorGUILayout.TextField("null");
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
                                        using (AspidEditorGUILayout.BeginHorizontal())
                                        {
                                            var targetNamespace = targetType.Namespace;
                                            EditorGUILayout.LabelField("Type", GUILayout.Width(45));
                                            EditorGUILayout.TextField($"{targetNamespace}.{targetName}");
                                        }
                                    }

                                    using (AspidEditorGUILayout.BeginHorizontal())
                                    {
                                        EditorGUILayout.LabelField("Method", GUILayout.Width(45));
                                        EditorGUILayout.TextField(@delegate.Method.Name);
                                    }
                                }
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
                default: DrawCompositeValue(obj, fieldName, parentFieldName); break;
            }
            return;

            bool IsKeyValuePairInEnumerable(object obj)
            {
                return obj.GetType()
                    .GetInterfaces()
                    .Any(@interface =>
                    {
                        if (!@interface.IsGenericType) return false;
                        if (!typeof(IEnumerable<>).IsAssignableFrom(@interface.GetGenericTypeDefinition()))
                            return false;

                        var interfaceArgument = @interface.GetGenericArguments()[0];
                        return interfaceArgument.IsGenericType &&
                            typeof(KeyValuePair<,>).IsAssignableFrom(interfaceArgument.GetGenericTypeDefinition());
                    });
            }
        }

        private static void DrawCompositeValue(object obj, string fieldName, string parentFieldName = "",
            bool ignoreNicify = false)
        {
            if (obj == null)
            {
                GUI.enabled = false;
                {
                    EditorGUILayout.TextField(fieldName, "null");
                }
                GUI.enabled = true;
                return;
            }

            var prefsKey = string.IsNullOrEmpty(parentFieldName) ? $"{parentFieldName}.{fieldName}" :
                $"{fieldName}";
            if (!Foldout(prefsKey, fieldName, ignoreNicify)) return;

            EditorGUI.indentLevel++;
            {
                var fields = obj.GetType()
                    .GetFieldInfosIncludingBaseClasses(BindingFlags.Instance | BindingFlags.Public |
                        BindingFlags.NonPublic);
                foreach (var field in fields)
                    DrawValue(field.GetValue(obj), field.Name, prefsKey);
            }
            EditorGUI.indentLevel--;
        }

        private static bool Foldout(string prefsKey, string fieldName, bool ignoreNicify = false)
        {
            var prefsValue = EditorPrefs.GetBool(prefsKey, false);

            prefsValue = EditorGUILayout.Foldout(prefsValue,
                ignoreNicify ? fieldName : ObjectNames.NicifyVariableName(fieldName));
            EditorPrefs.SetBool(prefsKey, prefsValue);

            return prefsValue;
        }
    }
}