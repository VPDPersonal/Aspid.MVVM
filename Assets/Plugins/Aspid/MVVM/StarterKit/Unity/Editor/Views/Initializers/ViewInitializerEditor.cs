#if !ASPID_MVVM_EDITOR_DISABLED
using UnityEditor;
using UnityEngine;
using System.Reflection;
using Aspid.CustomEditors;
using Aspid.UnityFastTools;
using UnityEngine.UIElements;
using Aspid.UnityFastTools.Editors;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(ViewInitializer))]
    public sealed class ViewInitializerEditor : ViewInitializerBaseEditor
    {
        private static readonly GUIContent _disposeOnDestroyLabel = new("Dispose On Destroy");
        
        private VisualElement _root;
        
        private SerializedProperty _view;
        private SerializedProperty _viewModel;
        private SerializedProperty _isDeinitialize;
        private SerializedProperty _initializeStage;
        private SerializedProperty _isDisposeViewOnDestroy;
        private SerializedProperty _isDisposeViewModelOnDestroy;
        
        private bool _isViewSet;
        private bool _isViewModelSet;

        private string IconPath => _isViewSet && _isViewModelSet
            ? "Aspid Icon"
            : "Aspid Icon Red";
        
        private void OnEnable()
        {
            _view = serializedObject.FindProperty("_viewComponents");
            _viewModel = serializedObject.FindProperty("_viewModelComponent");
            
            _isDeinitialize = serializedObject.FindProperty("_isDeinitialize");
            _initializeStage = serializedObject.FindProperty("_initializeStage");
            
            _isDisposeViewOnDestroy = serializedObject.FindProperty("_isDisposeViewOnDestroy");
            _isDisposeViewModelOnDestroy = serializedObject.FindProperty("_isDisposeViewModelOnDestroy");
        }

        public override VisualElement CreateInspectorGUI()
        {
            _root = new VisualElement();
            
            var header = new InspectorHeaderPanel(target, "Aspid Icon");
            var stage = BuildStage();
            var view = BuildViewInitializeComponent();
            var viewModel = BuildViewModelInitializeComponent();
            var debug = BuildDebugPanel();

            return _root
                .AddChild(header)
                .AddChild(view
                    .SetMargin(top: 10))
                .AddChild(viewModel
                    .SetMargin(top: 10))
                .AddChild(stage
                    .SetMargin(top: 10))
                .AddChild(debug
                    ?.SetMargin(top: 10));
        }

        private VisualElement BuildStage()
        {
            return Elements.CreateContainer(EditorColor.LightContainer)
                .AddTitle(EditorColor.LightText, "Stage")
                .AddChild(new IMGUIContainer(Draw));
            
            void Draw()
            {
                serializedObject.UpdateIfRequiredOrScript();
                {
                    EditorGUILayout.PropertyField(_initializeStage);

                    if (_initializeStage.enumValueIndex == 0) _isDeinitialize.boolValue = false;
                    else EditorGUILayout.PropertyField(_isDeinitialize);
                }
                serializedObject.ApplyModifiedProperties();
            }
        }

        private VisualElement BuildViewInitializeComponent()
        {
            var viewHelpBox = Elements.CreateHelpBox(
                text: "The View must be assigned",
                type: HelpBoxMessageType.Error,
                name: "ViewHelpBox");
            
            return Elements.CreateContainer(EditorColor.LightContainer)
                .AddTitle(EditorColor.LightText, "View")
                .AddChild(new IMGUIContainer(Draw))
                .AddChild(viewHelpBox
                    .SetMargin(top: 5));
            
            void Draw()
            {
                serializedObject.UpdateIfRequiredOrScript();
                {
                    EditorGUILayout.PropertyField(_isDisposeViewOnDestroy, _disposeOnDestroyLabel);
                    _isViewSet = DrawInitializeComponent<IView>(_view, "View");
                }
                serializedObject.ApplyModifiedProperties();
            
                UpdateHelpBoxes();
            }
        }

        private VisualElement BuildViewModelInitializeComponent()
        {
            var viewModelHelpBox = Elements.CreateHelpBox(
                text: "The ViewModel must be assigned",
                type: HelpBoxMessageType.Error,
                name: "ViewModelHelpBox");
            
            return Elements.CreateContainer(EditorColor.LightContainer)
                .AddTitle(EditorColor.LightText, "View Model")
                .AddChild(new IMGUIContainer(Draw))
                .AddChild(viewModelHelpBox
                    .SetMargin(top: 5));
            
            void Draw()
            {
                serializedObject.UpdateIfRequiredOrScript();
                {
                    EditorGUILayout.PropertyField(_isDisposeViewModelOnDestroy, _disposeOnDestroyLabel);
                    _isViewModelSet = DrawInitializeComponent<IViewModel>(_viewModel, "View Model");
                }
                serializedObject.ApplyModifiedProperties();
            
                UpdateHelpBoxes();
                UpdateHeaderText();
            }
        }

        private VisualElement BuildDebugPanel()
        {
            var initializer = (ViewInitializerBase)target;

            if (initializer.IsInitialized 
                || initializer is not ViewInitializerManual)
            {
                return Elements.CreateContainer(EditorColor.LightContainer)
                    .AddTitle(EditorColor.LightText, "Debug")
                    .AddChild(new IMGUIContainer(Draw));
            }

            return null;
            
            void Draw()
            {
                initializer = (ViewInitializerBase)target;
                var initializerType = initializer.GetType();
                    
                if (initializer is ViewInitializerManual)
                {
                    if (initializer.IsInitialized && GUILayout.Button("Reinitialize"))
                    {
                        var viewModel = (IViewModel)initializerType
                            .GetProperty("VeiwModel")!
                            .GetValue(initializer);

                        initializerType
                            .GetMethod("Initialize", BindingFlags.NonPublic)!
                            .Invoke(initializer, new object[] { viewModel });
                    }
                }
                else
                {
                    if (initializer.IsInitialized)
                    {
                        if (GUILayout.Button("Deinitialize"))
                        {
                            initializerType
                                .GetMethod("DeinitializeInternal", BindingFlags.NonPublic | BindingFlags.Instance)!
                                .Invoke(initializer, null);
                        }
                    }
                    else
                    {
                        if (GUILayout.Button("Initialize"))
                        {
                            initializerType
                                .GetMethod("InitializeInternal", BindingFlags.NonPublic | BindingFlags.Instance)!
                                .Invoke(initializer, null);
                        }
                    }
                }
            }
        }

        private void UpdateHelpBoxes()
        {
            _root.Q<VisualElement>("Header")
                .Q<Image>().SetImageFromResource(IconPath);
            
            _root.Q<HelpBox>("ViewHelpBox")
                .SetDisplay(_isViewSet ? DisplayStyle.None : DisplayStyle.Flex);
            
            _root.Q<HelpBox>("ViewModelHelpBox")
                .SetDisplay(_isViewModelSet ? DisplayStyle.None : DisplayStyle.Flex);
        }
        
        private void UpdateHeaderText()
        {
            var scriptName = target.GetScriptName();
            var headerText = _root.Q<Label>("HeaderText");
            headerText.text = $"{scriptName}{GetInitializeComponentName(_viewModel)}";
        }
    }
}
#endif