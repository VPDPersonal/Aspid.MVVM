using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Aspid.FastTools.SerializeReferences.Editors;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="PropertyDrawer"/> that gives every <c>[SerializeReference]</c> slot of a StarterKit contract
    /// the FastTools type picker, so the slots no longer need <c>[TypeSelector]</c>.
    /// </summary>
    /// <remarks>
    /// <c>useForChildren</c> also matches implementations serialized by value or as an asset; those fall back
    /// to Unity's default drawing.
    /// </remarks>
    [CustomPropertyDrawer(typeof(PluralRule), useForChildren: true)]
    [CustomPropertyDrawer(typeof(IConverter<,>), useForChildren: true)]
    [CustomPropertyDrawer(typeof(IViewFactory<>), useForChildren: true)]
    [CustomPropertyDrawer(typeof(ICollectionOrder<>), useForChildren: true)]
    [CustomPropertyDrawer(typeof(ICanExecuteHandler), useForChildren: true)]
    [CustomPropertyDrawer(typeof(ICollectionFilter<>), useForChildren: true)]
    [CustomPropertyDrawer(typeof(IViewFactoryWithKey<>), useForChildren: true)]
    public sealed class StarterKitTypePickerDrawer : PropertyDrawer
    {
        // null makes PropertyField fall back to the IMGUI path below.
        public override VisualElement CreatePropertyGUI(SerializedProperty property) =>
            IsManagedReference(property)
                ? SerializeReferenceEditorGUI.CreateField(property, preferredLabel)
                : null;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (IsManagedReference(property))
                SerializeReferenceIMGUIPropertyDrawer.Draw(position, label, property);
            else
                EditorGUI.PropertyField(position, property, label, includeChildren: true);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
            IsManagedReference(property)
                ? SerializeReferenceIMGUIPropertyDrawer.GetHeight(property)
                : EditorGUI.GetPropertyHeight(property, label, includeChildren: true);

        private static bool IsManagedReference(SerializedProperty property) =>
            property.propertyType is SerializedPropertyType.ManagedReference;
    }
}
