#if !ASPID_MVVM_EDITOR_DISABLED
using System;
using System.Linq;
using UnityEditor;
using Aspid.MVVM.StarterKit;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    // TODO Aspid.MVVM Unity – Write summary
    [CanEditMultipleObjects]
    [CustomEditor(typeof(GeneralView), editorForChildClasses: true)]
    public class GeneralViewEditor : MonoViewEditor<GeneralView, GeneralViewEditor>
    {
        public BinderListProperty BindersList { get; private set; }
        
        public SerializedProperty DesignViewModel { get; private set; }
        
        protected override ViewVisualElement<GeneralView, GeneralViewEditor> BuildVisualElement() =>
            new GeneralViewVisualElement(this);
        
        protected override void OnEnabled()
        {
            BindersList = new BinderListProperty(serializedObject);
            DesignViewModel = serializedObject.FindProperty("_designViewModel");
        }
        
        protected override void OnCreatingInspectorGUI() =>
            UpdateMetaData();

        protected override void OnGeometryChangedEventOnce(GeometryChangedEvent e) =>
            ((GeneralViewVisualElement)Root).UpdateGeneralBinders();

        protected override void OnSerializedPropertyChanged(SerializedPropertyChangeEvent e)
        {
            ValidateChangedInView();
            
            if (e.changedProperty.propertyPath == DesignViewModel.propertyPath)
            {
                UpdateMetaData();
                ((GeneralViewVisualElement)Root).UpdateGeneralBinders();
            }

            ValidateChangedInView();
        }

        private void UpdateMetaData()
        {
            var viewModelType = string.IsNullOrWhiteSpace(DesignViewModel.stringValue) 
                ? null
                : Type.GetType(DesignViewModel.stringValue);
            
            if (viewModelType is null)
            {
                BindersList.ArraySize = 0;
                return;
            }

            var viewModelMeta = new ViewModelMeta(viewModelType);
            var viewMeta = new ViewMeta(TargetAsView.GetType());

            var bindableProperties = viewModelMeta.BindableProperties
                .Where(bindableProperty => viewMeta.BinderProperties.All(binderProperty => binderProperty.Id != bindableProperty.Id))
                .ToArray();

            BindersList.ArraySize = bindableProperties.Length;
                
            for (var i = 0; i < bindableProperties.Length; i++)
            {
                var element = BindersList.GetArrayElementAtIndex(i);
                    
                element.Id = bindableProperties[i].Id;
                element.AssemblyQualifiedName = bindableProperties[i].Type?.AssemblyQualifiedName;
            }
        }
    }
}
#endif