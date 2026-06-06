using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Ids.Editors
{
    [CustomPropertyDrawer(typeof(IId), useForChildren: true)]
    internal sealed class IdStructPropertyDrawer : PropertyDrawer
    {
        private bool? _isUnique;
        private string _lastStringId;

        private bool IsUnique => _isUnique ??=
            fieldInfo.GetCustomAttributes(typeof(UniqueIdAttribute), false).Length > 0;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var ctx = new IdStructDrawerContext(label.text, fieldInfo, property);
            return IdStructIMGUIPropertyDrawer.GetIMGUIHeight(ctx, IsUnique, ref _lastStringId);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var height = GetPropertyHeight(property, label);
            var drawRect = new Rect(position.x, position.y, position.width, height);

            var ctx = new IdStructDrawerContext(label.text, fieldInfo, property);
            IdStructIMGUIPropertyDrawer.Draw(drawRect, ctx, IsUnique);
        }

        public override VisualElement CreatePropertyGUI(SerializedProperty property) => IdStructUIToolkitPropertyDrawer.Draw(
            ctx: new IdStructDrawerContext(preferredLabel, fieldInfo, property),
            isUnique: IsUnique);
    }
}
