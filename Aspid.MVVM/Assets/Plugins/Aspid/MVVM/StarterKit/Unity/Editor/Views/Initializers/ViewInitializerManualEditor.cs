#if !ASPID_MVVM_EDITOR_DISABLED
using UnityEditor;
using UnityEngine;
using System.Reflection;
using UnityEngine.UIElements;
using Aspid.FastTools.Editors;
using Aspid.FastTools.UIElements;

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

        private MessageType MessageType => _isViewSet
            ? MessageType.None
            : MessageType.Error;
        
        private void OnEnable()
        {
            _view = serializedObject.FindProperty("_viewComponents");
            _isDisposeViewOnDestroy = serializedObject.FindProperty("_isDisposeViewOnDestroy");
        }
        
        public override VisualElement CreateInspectorGUI()
        {
            _root = new VisualElement();

            var header = new AspidInspectorHeader(GetScriptName(), target);

            var viewHelpBox = new AspidHelpBox("The View must be assigned", HelpBoxMessageType.Error)
                .SetName("ViewHelpBox");
            
            var view = new AspidContainer()
                .AddChild(new AspidTitle("View"))
                .AddChild(new IMGUIContainer(DrawViewInitializeComponent))
                .AddChild(viewHelpBox
                    .SetMargin(top: 5));
            
            return _root
                .AddChild(header)
                .AddChild(view
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
        
        private void UpdateHelpBoxes()
        {
            _root.Q<AspidInspectorHeader>().SetMessageType(MessageType);
            
            _root.Q<HelpBox>("ViewHelpBox")
                .SetDisplay(_isViewSet ? DisplayStyle.None : DisplayStyle.Flex);
        }

        private string GetScriptName()
        {
            const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
           
            var scriptName = target.GetScriptName();
            var property = typeof(ViewInitializerManual).GetProperty("ViewModel", bindingFlags);
            var viewModel = property!.GetValue(target);
            
            return viewModel is null ? scriptName : $"{scriptName} ({viewModel.GetType().Name})";
        }
    }
}
#endif