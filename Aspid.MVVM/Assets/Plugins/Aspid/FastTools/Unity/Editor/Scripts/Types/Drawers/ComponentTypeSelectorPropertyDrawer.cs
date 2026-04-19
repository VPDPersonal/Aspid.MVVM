using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Types.Editors
{
	[CustomPropertyDrawer(typeof(ComponentTypeSelector))]
	internal sealed class ComponentTypeSelectorPropertyDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) =>
			ComponentTypeSelectorDrawer.DrawIMGUI(position, property, fieldInfo.DeclaringType);

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
			EditorGUIUtility.singleLineHeight;

		public override VisualElement CreatePropertyGUI(SerializedProperty property) =>
			ComponentTypeSelectorDrawer.DrawUIToolkit(property, fieldInfo.DeclaringType);
	}
}
