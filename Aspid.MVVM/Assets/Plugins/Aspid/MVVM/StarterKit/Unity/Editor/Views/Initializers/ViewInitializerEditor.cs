#if !ASPID_MVVM_EDITOR_DISABLED
using UnityEditor;
using UnityEngine;
using System.Reflection;
using UnityEngine.UIElements;
using Aspid.FastTools.Editors;
using Aspid.FastTools.UIElements;
using Aspid.FastTools.UIElements.Editors.Internal;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    // TODO Aspid.MVVM – Refactor
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

        private StatusStyle Status => _isViewSet && _isViewModelSet
            ? StatusStyle.Success
            : StatusStyle.Error;
        
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
            _root = new VisualElement()
                .AddStyleSheetsFromResource(StyleClasses.DefaultStyleSheet);

            var header = new AspidInspectorHeader(target)
                .SetMargin(top: 3, left: -10f);
            var stage = BuildStage();
            var view = BuildViewInitializeComponent();
            var viewModel = BuildViewModelInitializeComponent();
            var debug = BuildDebugPanel();

            return _root
                .AddChild(header)
                .AddChild(view)
                .AddChild(viewModel)
                .AddChild(stage)
                .AddChild(debug);
        }

        private VisualElement BuildStage()
        {
            return new AspidBox()
                .SetMargin(top: 5, left: -10f)
                .AddChild(new AspidLabel("Stage").SetMarginBottom(5))
                .AddChild(new IMGUIContainer(Draw));
            
            void Draw()
            {
                serializedObject.UpdateIfRequiredOrScript();
                {
                    EditorGUILayout.PropertyField(_initializeStage);

                    if (_initializeStage.enumValueIndex is 0 or 4)
                    {
                        _isDeinitialize.boolValue = false;
                    }
                    else EditorGUILayout.PropertyField(_isDeinitialize);
                }
                serializedObject.ApplyModifiedProperties();
            }
        }

        private VisualElement BuildViewInitializeComponent()
        {
            var viewHelpBox = new AspidHelpBox(
                    title: "Missing View Reference",
                    message: "This initializer requires a View component to drive bindings. Assign at least one View in the field above, otherwise the initializer will skip binding on enter play mode.",
                    HelpBoxMessageType.Error)
                .SetName("ViewHelpBox");
            
            return new AspidBox()
                .SetMargin(top: 5, left: -10f)
                .AddChild(new AspidLabel("View").SetMarginBottom(5))
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
            var viewModelHelpBox = new AspidHelpBox(
                    title: "Missing ViewModel Reference",
                    message: "This initializer requires a ViewModel to supply data to the View. Assign a ViewModel using the resolve strategy above, otherwise bindings will not receive values.",
                    HelpBoxMessageType.Error)
                .SetName("ViewModelHelpBox");
            
            return new AspidBox()
                .SetMargin(top: 5, left: -10f)
                .AddChild(new AspidLabel("ViewModel").SetMarginBottom(5))
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
                return new AspidBox()
                    .SetMargin(top: 5, left: -10f)
                    .AddChild(new AspidLabel("Debug").SetMarginBottom(5))
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
            _root.Q<AspidInspectorHeader>().Status = Status;

            _root.Q<AspidHelpBox>("ViewHelpBox")
                .SetDisplay(_isViewSet ? DisplayStyle.None : DisplayStyle.Flex);

            _root.Q<AspidHelpBox>("ViewModelHelpBox")
                .SetDisplay(_isViewModelSet ? DisplayStyle.None : DisplayStyle.Flex);
        }
        
        private void UpdateHeaderText()
        {
            var header = _root.Q<AspidInspectorHeader>();
            header.Text = target.GetScriptName();
            header.Subtext = GetInitializeComponentName(_viewModel);
        }
    }
}
#endif