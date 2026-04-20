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
    [CustomEditor(typeof(ViewInitializerManual))]
    public sealed class ViewInitializerManualEditor : ViewInitializerBaseEditor
    {
        private static readonly GUIContent _disposeOnDestroyLabel = new("Dispose On Destroy");
        
        private VisualElement _root;
        
        private SerializedProperty _view;
        private SerializedProperty _isDisposeViewOnDestroy;
        
        private bool _isViewSet;

        private StatusStyle Status => _isViewSet
            ? StatusStyle.Success
            : StatusStyle.Error;
        
        private void OnEnable()
        {
            _view = serializedObject.FindProperty("_viewComponents");
            _isDisposeViewOnDestroy = serializedObject.FindProperty("_isDisposeViewOnDestroy");
        }
        
        public override VisualElement CreateInspectorGUI()
        {
            _root = new VisualElement()
                .AddStyleSheetsFromResource(StyleClasses.DefaultStyleSheet);

            var header = new AspidInspectorHeader(GetScriptName(), target) { Subtext = GetScriptSubtext() }
                .SetMargin(top: 3, left: -10f);

            var viewHelpBox = new AspidHelpBox(
                    title: "Missing View Reference",
                    message: "This manual initializer needs a View component to bind the ViewModel to. Assign at least one View in the field above before calling Initialize at runtime.",
                    HelpBoxMessageType.Error)
                .SetName("ViewHelpBox");
            
            var view = new AspidBox()
                .SetMargin(top: 5, left: -10f)
                .AddChild(new AspidLabel("View").SetMarginBottom(5))
                .AddChild(new IMGUIContainer(DrawViewInitializeComponent))
                .AddChild(viewHelpBox
                    .SetMargin(top: 5));

            return _root
                .AddChild(header)
                .AddChild(view);
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
        
        private void UpdateHelpBoxes()
        {
            _root.Q<AspidInspectorHeader>().Status = Status;
            
            _root.Q<AspidHelpBox>("ViewHelpBox")
                .SetDisplay(_isViewSet ? DisplayStyle.None : DisplayStyle.Flex);
        }

        private string GetScriptName() =>
            target.GetScriptName();

        private string GetScriptSubtext()
        {
            const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

            var property = typeof(ViewInitializerManual).GetProperty("ViewModel", bindingFlags);
            var viewModel = property!.GetValue(target);

            return viewModel?.GetType().Name ?? string.Empty;
        }
    }
}
#endif