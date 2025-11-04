using System.CodeDom.Compiler;
using System.Linq;
using System.Reflection;
using UnityEditor;
using Aspid.MVVM.StarterKit;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(GeneralView), editorForChildClasses: true)]
    public class GeneralViewEditor : MonoViewEditor<GeneralView, GeneralViewEditor>
    {
        public SerializedProperty BindersList { get; private set; }
        
        public SerializedProperty DesignViewModel { get; private set; }
        
        protected override ViewVisualElement<GeneralView, GeneralViewEditor> BuildVisualElement() =>
            new GeneralViewVisualElement(this);
        
        protected override void OnEnabled()
        {
            BindersList =  serializedObject.FindProperty("_bindersList");
            DesignViewModel = serializedObject.FindProperty("_designViewModel");
        }
    }
}