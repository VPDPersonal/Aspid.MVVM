using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Aspid.CustomEditors.Configs;
using Aspid.CustomEditors.Components;
using Aspid.CustomEditors.Components.Extensions;
using Aspid.CustomEditors.Extensions.VisualElements;

namespace Aspid.MVVM.StarterKit.Views.Initializers
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(ViewInitializer))]
    public sealed class ViewInitializerEditor : Editor
    {
        private VisualElement _root;
        private SerializedProperty _view;
        private SerializedProperty _viewModel;
        
        private bool _isViewSet;
        private bool _isViewModelSet;

        private string IconPath => _isViewSet && _isViewModelSet
            ? "Aspid Icon"
            : "Aspid Icon Red";
        
        private void OnEnable()
        {
            _view = serializedObject.FindProperty("_viewComponents");
            _viewModel = serializedObject.FindProperty("_viewModelComponent");
        }

        public override VisualElement CreateInspectorGUI()
        {
            _root = new VisualElement();

            var header = Elements.CreateHeader(target, "Aspid Icon");
            header.Q<Image>("HeaderIcon").AddOpenScriptCommand(target);

            var viewHelpBox = Elements.CreateHelpBox(
                text: "The View must be assigned",
                type: HelpBoxMessageType.Error,
                name: "ViewHelpBox");
            
            var view = Elements.CreateContainer(EditorColor.LightContainer)
                .AddTitle(EditorColor.LightText, "View")
                .AddChild(new IMGUIContainer(() => DrawComponent<IView>(_view, ref _isViewSet)))
                .AddChild(viewHelpBox
                    .SetMargin(top: 5));
            
            var viewModelHelpBox = Elements.CreateHelpBox(
                text: "The ViewModel must be assigned",
                type: HelpBoxMessageType.Error,
                name: "ViewModelHelpBox");

            var viewModel = Elements.CreateContainer(EditorColor.LightContainer)
                .AddTitle(EditorColor.LightText, "View Model")
                .AddChild(new IMGUIContainer(() => DrawComponent<IViewModel>(_viewModel, ref _isViewModelSet)))
                .AddChild(viewModelHelpBox
                    .SetMargin(top: 5));
            
            return _root
                .AddChild(header)
                .AddChild(view
                    .SetMargin(top: 10))
                .AddChild(viewModel
                    .SetMargin(top: 10));
        }

        private void DrawComponent<TInterface>(SerializedProperty component, ref bool isSet)
            where TInterface : class
        {
            serializedObject.UpdateIfRequiredOrScript();
            {
                var label = typeof(TInterface) == typeof(IView)
                    ? "Views"
                    : "View Model";

                EditorGUILayout.PropertyField(component, new GUIContent(label));

                if (component.isArray)
                {
                    if (component.arraySize is 0)
                        isSet = false;

                    for (var i = 0; i < component.arraySize; i++)
                    {
                        isSet = ValidateInitializeComponent<TInterface>(component.GetArrayElementAtIndex(i));
                        if (!isSet) break;
                    }
                }
                else isSet = ValidateInitializeComponent<TInterface>(component);
            }
            serializedObject.ApplyModifiedProperties();

            _root.Q<VisualElement>("Header")
                .Q<Image>().SetImageFromResource(IconPath);
            
            _root.Q<HelpBox>("ViewHelpBox")
                .SetDisplay(_isViewSet ? DisplayStyle.None : DisplayStyle.Flex);
            
            _root.Q<HelpBox>("ViewModelHelpBox")
                .SetDisplay(_isViewModelSet ? DisplayStyle.None : DisplayStyle.Flex);
        }
        
        private static bool ValidateInitializeComponent<TInterface>(SerializedProperty component)
            where TInterface : class
        {
            var resolve = component.FindPropertyRelative("Resolve");
            switch (resolve.enumValueIndex)
            {
                case 0:
                    {
                        var mono = component.FindPropertyRelative("Mono");
                        if (mono.objectReferenceValue is not null and not TInterface)
                            mono.objectReferenceValue =
                                ((Component)mono.objectReferenceValue).GetComponent<TInterface>() as Object;

                        return mono.objectReferenceValue;
                    }
                case 1:
                    {
                        var references = component.FindPropertyRelative("References");
                        return references.managedReferenceValue is not null;
                    }
                case 2:
                    {
                        var scriptable = component.FindPropertyRelative("Scriptable");
                        if (scriptable.objectReferenceValue is not TInterface)
                            scriptable.objectReferenceValue = null;

                        return scriptable.objectReferenceValue;
                    }
                default:
                    {
                        var type = component.FindPropertyRelative("Type");
                        var typeName = type.FindPropertyRelative("_typeName");
                        var typeNameIsEmpty = string.IsNullOrEmpty(typeName.stringValue);

                        return !typeNameIsEmpty;
                    }
            }
        }
    }
}