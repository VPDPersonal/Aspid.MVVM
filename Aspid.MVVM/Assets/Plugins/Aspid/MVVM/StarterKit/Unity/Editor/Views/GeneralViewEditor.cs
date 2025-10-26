// using System.Collections.Generic;
// using Aspid.CustomEditors;
// using Aspid.UnityFastTools;
// using UnityEditor;
// using UnityEngine.UIElements;
//
// // ReSharper disable once CheckNamespace
// namespace Aspid.MVVM.StarterKit
// {
//     [CanEditMultipleObjects]
//     [CustomEditor(typeof(GeneralView))]
//     public class GeneralViewEditor : ViewEditor<GeneralView, GeneralViewEditor>
//     {
//         public SerializedProperty DesignViewModelProperty;
//         public SerializedProperty BindersProperty;
//
//         protected virtual void OnEnable()
//         {
//             BindersProperty = serializedObject.FindProperty("_binders");
//             DesignViewModelProperty = serializedObject.FindProperty("_designViewModel");
//         }
//
//         protected override ViewVisualElement<GeneralView, GeneralViewEditor> BuildVisualElement() =>
//             new GeneralViewVisualElement(this);
//     }
//
//     public class GeneralViewVisualElement : ViewVisualElement<GeneralView, GeneralViewEditor>
//     {
//         protected override IEnumerable<string> PropertiesExcluding
//         {
//             get
//             {
//                 foreach (var property in base.PropertiesExcluding)
//                 {
//                     yield return property;
//                 }
//
//                 yield return "_binders";
//                 yield return "_designViewModel";
//             }
//         }
//         
//         public GeneralViewVisualElement(GeneralViewEditor editor) 
//             : base(editor) { }
//
//         protected override VisualElement OnBuiltHeader()
//         {
//             var root = Elements.CreateContainer(EditorColor.LightContainer);
//             root.AddChild(new AspidPropertyField(Editor.DesignViewModelProperty));
//             
//             
//             root.AddChild(new AspidPropertyField(Editor.BindersProperty));
//
//             return root;
//         }
//     }
// }