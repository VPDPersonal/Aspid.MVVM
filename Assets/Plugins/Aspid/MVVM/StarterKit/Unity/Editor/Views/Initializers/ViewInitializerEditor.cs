#if !ASPID_MVVM_EDITOR_DISABLED
using UnityEditor;
using UnityEngine;
using Aspid.CustomEditors;
using UnityEngine.UIElements;

namespace Aspid.MVVM.StarterKit.Unity
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(ViewInitializer))]
    public sealed class ViewInitializerEditor : ViewInitializerBaseEditor<ViewInitializer>
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

            var scriptName = target.GetScriptName();
            var header = Elements.CreateHeader("Aspid Icon", scriptName);
            header.Q<Image>("HeaderIcon").AddOpenScriptCommand(target);

            var viewHelpBox = Elements.CreateHelpBox(
                text: "The View must be assigned",
                type: HelpBoxMessageType.Error,
                name: "ViewHelpBox");
            
            var stage = Elements.CreateContainer(EditorColor.LightContainer)
                .AddTitle(EditorColor.LightText, "Stage")
                .AddChild(new IMGUIContainer(DrawStage));
            
            var view = Elements.CreateContainer(EditorColor.LightContainer)
                .AddTitle(EditorColor.LightText, "View")
                .AddChild(new IMGUIContainer(DrawViewInitializeComponent))
                .AddChild(viewHelpBox
                    .SetMargin(top: 5));
            
            var viewModelHelpBox = Elements.CreateHelpBox(
                text: "The ViewModel must be assigned",
                type: HelpBoxMessageType.Error,
                name: "ViewModelHelpBox");
            
            var viewModel = Elements.CreateContainer(EditorColor.LightContainer)
                .AddTitle(EditorColor.LightText, "View Model")
                .AddChild(new IMGUIContainer(DrawViewModelInitializeComponent))
                .AddChild(viewModelHelpBox
                    .SetMargin(top: 5));

            return _root
                .AddChild(header)
                .AddChild(view
                    .SetMargin(top: 10))
                .AddChild(viewModel
                    .SetMargin(top: 10))
                .AddChild(stage
                    .SetMargin(top: 10));
        }
        
        private void DrawViewInitializeComponent()
        {
            serializedObject.UpdateIfRequiredOrScript();
            {
                EditorGUILayout.PropertyField(_isDisposeViewOnDestroy, _disposeOnDestroyLabel);
                _isViewSet = DrawInitializeComponent<IView>(_view, "View");
            }
            serializedObject.ApplyModifiedProperties();
            
            UpdateHelpBoxes();
        }

        private void DrawViewModelInitializeComponent()
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
        
        private void DrawStage()
        {
            serializedObject.UpdateIfRequiredOrScript();
            {
                EditorGUILayout.PropertyField(_initializeStage);

                if (_initializeStage.enumValueIndex == 0) _isDeinitialize.boolValue = false;
                else EditorGUILayout.PropertyField(_isDeinitialize);
            }
            serializedObject.ApplyModifiedProperties();
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