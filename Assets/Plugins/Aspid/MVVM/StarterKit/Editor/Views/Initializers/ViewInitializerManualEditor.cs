#if !ASPID_MVVM_EDITOR_DISABLED
using UnityEditor;
using UnityEngine;
using System.Reflection;
using Aspid.CustomEditors;
using UnityEngine.UIElements;

namespace Aspid.MVVM.StarterKit.Views
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(ViewInitializerManual))]
    public sealed class ViewInitializerManualEditor : ViewInitializerBaseEditor<ViewInitializerManual>
    {
        private static readonly GUIContent _disposeOnDestroyLabel = new("Dispose On Destroy");
        
        private VisualElement _root;
        
        private SerializedProperty _view;
        private SerializedProperty _isDisposeViewOnDestroy;
        
        private bool _isViewSet;
        
        private string IconPath => _isViewSet
            ? "Aspid Icon"
            : "Aspid Icon Red";
        
        private void OnEnable()
        {
            _view = serializedObject.FindProperty("_viewComponents");
            _isDisposeViewOnDestroy = serializedObject.FindProperty("_isDisposeViewOnDestroy");
        }
        
        public override VisualElement CreateInspectorGUI()
        {
            _root = new VisualElement();
            
            var header = Elements.CreateHeader("Aspid Icon", GetScriptName());
            header.Q<Image>("HeaderIcon").AddOpenScriptCommand(target);

            var viewHelpBox = Elements.CreateHelpBox(
                text: "The View must be assigned",
                type: HelpBoxMessageType.Error,
                name: "ViewHelpBox");
            
            var view = Elements.CreateContainer(EditorColor.LightContainer)
                .AddTitle(EditorColor.LightText, "View")
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
            _root.Q<VisualElement>("Header")
                .Q<Image>().SetImageFromResource(IconPath);
            
            _root.Q<HelpBox>("ViewHelpBox")
                .SetDisplay(_isViewSet ? DisplayStyle.None : DisplayStyle.Flex);
        }

        private string GetScriptName()
        {
            const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
           
            var scriptName = target.GetScriptName();
            var field = typeof(ViewInitializerManual).GetField("_viewModel", bindingFlags);
            var viewModel = field.GetValue(target);
            
            return viewModel is null ? scriptName : $"{scriptName} ({viewModel.GetType().Name})";
        }
    }
}
#endif