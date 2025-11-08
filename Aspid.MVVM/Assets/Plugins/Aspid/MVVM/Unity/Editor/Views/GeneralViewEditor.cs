using System.Linq;
using UnityEditor;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
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
            BindersList = new BinderListProperty(serializedObject.FindProperty("_bindersList"));
            DesignViewModel = serializedObject.FindProperty("_designViewModel");
        }

        protected override void OnCreatingInspectorGUI()
        {
            var viewModelType = (DesignViewModel.objectReferenceValue as MonoScript)?.GetClass();
            
            if (viewModelType is null)
            {
                BindersList.ArraySize = 0;
                return;
            }

            var viewModelMeta = new ViewModelMeta(viewModelType);
            var viewMeta = new ViewMeta(TargetAsSpecificView.GetType());

            var bindableProperties = viewModelMeta.BindableProperties
                .Where(bindableProperty => viewMeta.BinderProperties.All(binderProperty => binderProperty.Id != bindableProperty.Id))
                .ToArray();

            BindersList.ArraySize = bindableProperties.Length;
                
            for (var i = 0; i < bindableProperties.Length; i++)
            {
                var element = BindersList.GetArrayElementAtIndex(i);
                    
                element.Id = bindableProperties[i].Id;
                element.AssemblyQualifiedName = bindableProperties[i].Type.AssemblyQualifiedName;
            }
        }
    }
}