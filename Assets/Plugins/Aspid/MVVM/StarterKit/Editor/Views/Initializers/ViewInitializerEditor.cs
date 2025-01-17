using System;
using UnityEditor;
using UnityEngine;
using Aspid.CustomEditors;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace Aspid.MVVM.StarterKit.Views
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

            var scriptName = target.GetScriptName();
            var header = Elements.CreateHeader("Aspid Icon", scriptName);
            
            header.Q<Image>("HeaderIcon").AddOpenScriptCommand(target);

            var viewHelpBox = Elements.CreateHelpBox(
                text: "The View must be assigned",
                type: HelpBoxMessageType.Error,
                name: "ViewHelpBox");
            
            var view = Elements.CreateContainer(EditorColor.LightContainer)
                .AddTitle(EditorColor.LightText, "View")
                .AddChild(new IMGUIContainer(() => DrawComponent<IView>(_view, "Views", ref _isViewSet)))
                .AddChild(viewHelpBox
                    .SetMargin(top: 5));
            
            var viewModelHelpBox = Elements.CreateHelpBox(
                text: "The ViewModel must be assigned",
                type: HelpBoxMessageType.Error,
                name: "ViewModelHelpBox");

            var viewModel = Elements.CreateContainer(EditorColor.LightContainer)
                .AddTitle(EditorColor.LightText, "View Model")
                .AddChild(new IMGUIContainer(() =>
                {
                    DrawComponent<IViewModel>(_viewModel, "View Model", ref _isViewModelSet);
                    var headerText = _root.Q<Label>("HeaderText");
                    headerText.text = $"{scriptName}{GetInitializeComponentName(_viewModel)}";
                }))
                .AddChild(viewModelHelpBox
                    .SetMargin(top: 5));
            
            return _root
                .AddChild(header)
                .AddChild(view
                    .SetMargin(top: 10))
                .AddChild(viewModel
                    .SetMargin(top: 10));
        }

        private void DrawComponent<TInterface>(SerializedProperty component, string label, ref bool isSet)
            where TInterface : class
        {
            serializedObject.UpdateIfRequiredOrScript();
            {
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
            switch ((InitializeComponent.Resolve)resolve.enumValueIndex)
            {
                case InitializeComponent.Resolve.References:
                    {
                        var references = component.FindPropertyRelative("References");
                        return references.managedReferenceValue is not null;
                    }
                case InitializeComponent.Resolve.ScriptableObject:
                    {
                        var scriptable = component.FindPropertyRelative("Scriptable");
                        if (scriptable.objectReferenceValue is not TInterface)
                            scriptable.objectReferenceValue = null;

                        return scriptable.objectReferenceValue;
                    }
#if ASPID_MVVM_ZENJECT_INTEGRATION || ASPID_MVVM_VCONTAINER_INTEGRATION
                case InitializeComponent.Resolve.Di:
                    {
                        var type = component.FindPropertyRelative("Type");
                        var typeName = type.FindPropertyRelative("_typeName");
                        var typeNameIsEmpty = string.IsNullOrEmpty(typeName.stringValue);

                        return !typeNameIsEmpty;
                    }
#endif
                
                case InitializeComponent.Resolve.Mono:
                default:
                    {
                        var mono = component.FindPropertyRelative("Mono");
                        if (mono.objectReferenceValue is not null and not TInterface)
                            mono.objectReferenceValue =
                                ((Component)mono.objectReferenceValue).GetComponent<TInterface>() as Object;

                        return mono.objectReferenceValue;
                    }
            }
        }

        private static string GetInitializeComponentName(SerializedProperty component)
        {
            var resolve = component.FindPropertyRelative("Resolve");
            
            switch ((InitializeComponent.Resolve)resolve.enumValueIndex)
            {
                case InitializeComponent.Resolve.References:
                    {
                        var references = component.FindPropertyRelative("References");
                        var typeName = references.managedReferenceValue?.GetType().Name;
                        
                        return !string.IsNullOrWhiteSpace(typeName) 
                            ? $" ({typeName})" 
                            : string.Empty;
                    }
                case InitializeComponent.Resolve.ScriptableObject:
                    {
                        var scriptable = component.FindPropertyRelative("Scriptable");

                        return scriptable.objectReferenceValue
                            ? $" ({scriptable.objectReferenceValue.GetType().Name})"
                            : string.Empty;
                    }
#if ASPID_MVVM_ZENJECT_INTEGRATION || ASPID_MVVM_VCONTAINER_INTEGRATION
                case InitializeComponent.Resolve.Di:
                    {
                        var typeProperty = component.FindPropertyRelative("Type");
                        var typeNameProperty = typeProperty.FindPropertyRelative("_typeName");
                        
                        var typeName = Type.GetType(typeNameProperty.stringValue)?.Name;
                        
                        return !string.IsNullOrWhiteSpace(typeName) 
                            ? $" ({typeName})" 
                            : string.Empty;
                    }
#endif
                case InitializeComponent.Resolve.Mono:
                default:
                    {
                        var mono = component.FindPropertyRelative("Mono");
                        
                        return mono.objectReferenceValue
                            ? $" ({mono.objectReferenceValue.GetType().Name})"
                            : string.Empty;
                    }
            }
        }
    }
}