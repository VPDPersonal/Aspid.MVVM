using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Aspid.MVVM.StarterKit.Views.Initializers
{
    public static class InitializeComponentDrawer
    {
        public static void Draw<TInterface>(SerializedObject serializedObject, SerializedProperty component, Action<bool> isSet)
            where TInterface : class
        {
            serializedObject.UpdateIfRequiredOrScript();
            {
                var resolve = component.FindPropertyRelative("Resolve");
                EditorGUILayout.PropertyField(resolve);
                switch (resolve.enumValueIndex)
                {
                    case 0:
                        {
                            var mono = component.FindPropertyRelative("Mono");
                            EditorGUILayout.PropertyField(mono);

                            if (mono.objectReferenceValue is not null and not TInterface)
                            {
                                mono.objectReferenceValue =
                                    ((Component)mono.objectReferenceValue).GetComponent<TInterface>() as Object;
                            }
                    
                            isSet?.Invoke(mono.objectReferenceValue);
                            break;
                        }
                    case 1:
                        {
                            var references = component.FindPropertyRelative("References");
                            EditorGUILayout.PropertyField(references);
                    
                            isSet?.Invoke(references.managedReferenceValue is not null);
                            break;
                        }
#if ASPID_MVVM_ZENJECT_INTEGRATION || ASPID_MVVM_VCONTAINER_INTEGRATION
                    default:
                        {
                            var type = component.FindPropertyRelative("Type");
                            EditorGUILayout.PropertyField(type);

                            var typeName = type.FindPropertyRelative("_typeName");
                            var typeNameIsEmpty = string.IsNullOrEmpty(typeName.stringValue);
                    
                            isSet?.Invoke(!typeNameIsEmpty);
                            break;
                        }
#endif
                }
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}