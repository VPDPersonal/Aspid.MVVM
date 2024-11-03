using System;
using UnityEditor;
using Aspid.UI.MVVM.Views;
using UnityEngine.UIElements;
using Aspid.UI.MVVM.ViewModels;
using Aspid.CustomEditors.Configs;
using Aspid.CustomEditors.Components;
using Aspid.CustomEditors.Components.Extensions;
using Aspid.CustomEditors.Extensions.VisualElements;

namespace Aspid.UI.MVVM.StarterKit.Views.Initializers
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
            _view = serializedObject.FindProperty("_viewComponent");
            _viewModel = serializedObject.FindProperty("_viewModelComponent");
        }

        public override VisualElement CreateInspectorGUI()
        {
            _root = new VisualElement();

            var viewHelpBox = Elements.CreateHelpBox(
                text: "The View must be assigned",
                type: HelpBoxMessageType.Error,
                name: "ViewHelpBox");

            var view = Elements.CreateContainer(EditorColor.LightContainer)
                .AddTitle(EditorColor.LightText, "View")
                .AddChild(new IMGUIContainer(() => DrawComponent<IView>(_view, value => _isViewSet = value)))
                .AddChild(viewHelpBox
                    .SetMargin(top: 5));
            
            var viewModelHelpBox = Elements.CreateHelpBox(
                text: "The ViewModel must be assigned",
                type: HelpBoxMessageType.Error,
                name: "ViewModelHelpBox");
            
            var viewModel = Elements.CreateContainer(EditorColor.LightContainer)
                .AddTitle(EditorColor.LightText, "View Model")
                .AddChild(new IMGUIContainer(() => DrawComponent<IViewModel>(_viewModel, value => _isViewModelSet = value)))
                .AddChild(viewModelHelpBox
                    .SetMargin(top: 5));
            
            return _root
                .AddHeader(target, "Aspid Icon")
                .AddChild(view
                    .SetMargin(top: 10))
                .AddChild(viewModel
                    .SetMargin(top: 10));
        }

        private void DrawComponent<TInterface>(SerializedProperty component, Action<bool> isSet)
            where TInterface : class
        {
            InitializeComponentDrawer.Draw<TInterface>(serializedObject, component, isSet);
            
            _root.Q<VisualElement>("Header")
                .Q<Image>().SetImageFromResource(IconPath);
            
            _root.Q<HelpBox>("ViewHelpBox")
                .SetDisplay(_isViewSet ? DisplayStyle.None : DisplayStyle.Flex);
            
            _root.Q<HelpBox>("ViewModelHelpBox")
                .SetDisplay(_isViewModelSet ? DisplayStyle.None : DisplayStyle.Flex);
        }
    }
}