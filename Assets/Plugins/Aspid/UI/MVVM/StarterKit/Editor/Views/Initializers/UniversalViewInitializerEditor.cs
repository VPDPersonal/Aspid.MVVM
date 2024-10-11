using UnityEditor;
using UnityEngine;
using Aspid.UI.MVVM.Views;
using Aspid.UI.MVVM.ViewModels;

namespace Aspid.UI.MVVM.StarterKit.Views.Initializers
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(UniversalViewInitializer))]
    public sealed class UniversalViewInitializerEditor : Editor
    {
        private SerializedProperty _view;
        private SerializedProperty _viewModel;
        
        private void OnEnable()
        {
            _view = serializedObject.FindProperty("_viewComponent");
            _viewModel = serializedObject.FindProperty("_viewModelComponent");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            {
                DrawComponent<IView>("View", _view);
                DrawComponent<IViewModel>("ViewModel", _viewModel);
            }
            serializedObject.ApplyModifiedProperties();
        }

        private static void DrawComponent<TInterface>(string label, SerializedProperty component)
            where TInterface : class
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            {
                EditorGUILayout.LabelField(label);
                    
                var resolve = component.FindPropertyRelative("Resolve");
                EditorGUILayout.PropertyField(resolve);

                if (resolve.enumValueIndex == 0)
                {
                    EditorGUILayout.PropertyField(component.FindPropertyRelative("Type"));
                }
                else if (resolve.enumValueIndex == 1)
                {
                    var mono = component.FindPropertyRelative("Mono");
                    EditorGUILayout.PropertyField(mono);

                    if (mono.objectReferenceValue is not null and not TInterface)
                    {
                        mono.objectReferenceValue = ((Component)mono.objectReferenceValue).GetComponent<TInterface>() as Object;
                    }
                }
                else
                {
                    EditorGUILayout.PropertyField(component.FindPropertyRelative("References"));
                }
            }
            EditorGUILayout.EndVertical();
        }
    }
}