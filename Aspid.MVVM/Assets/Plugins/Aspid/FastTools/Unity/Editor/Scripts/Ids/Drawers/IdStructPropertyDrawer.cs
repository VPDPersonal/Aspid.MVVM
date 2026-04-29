#nullable enable
using System;
using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Aspid.FastTools.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Ids.Editors
{
    [CustomPropertyDrawer(typeof(IId), useForChildren: true)]
    internal sealed class IdStructPropertyDrawer : PropertyDrawer
    {
        private static readonly System.Collections.Generic.Dictionary<string, (double time, bool isUnique)> _cache = new();
        // Bumped to 10s pending a full rework of CheckIsUnique (see spec §4 row 11).
        private const double CacheLifetime = 10.0;

        private bool? _isUnique;
        private bool IsUnique => _isUnique ??= fieldInfo.GetCustomAttributes(typeof(UniqueIdAttribute), false).Length > 0;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var height = IdStructDrawer.GetIMGUIHeight(property);

            if (IsUnique && !string.IsNullOrEmpty(property.FindPropertyRelative(Constants.StringIdFieldName)?.stringValue) && !GetIsUnique(property))
                height += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            return height;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var drawH    = IdStructDrawer.GetIMGUIHeight(property);
            var drawRect = new Rect(position.x, position.y, position.width, drawH);
            IdStructDrawer.DrawIMGUI(drawRect, property, label, fieldInfo.FieldType);

            if (!IsUnique) return;

            var stringId = property.FindPropertyRelative(Constants.StringIdFieldName)?.stringValue;
            if (string.IsNullOrEmpty(stringId) || GetIsUnique(property)) return;

            var warnY    = position.y + drawH + EditorGUIUtility.standardVerticalSpacing;
            var warnRect = new Rect(position.x, warnY, position.width, EditorGUIUtility.singleLineHeight);
            EditorGUI.HelpBox(warnRect, "ID is not unique among assets of this type", MessageType.Warning);
        }

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var mainElement = IdStructDrawer.DrawUIToolkit(property, preferredLabel, fieldInfo.FieldType);

            if (!IsUnique)
                return mainElement;

            var warningLabel = new Label("⚠ ID is not unique among assets of this type")
                .SetDisplay(DisplayStyle.None)
                .SetMargin(top: 2)
                .SetColor(new Color(1f, 0.75f, 0f))
                .SetFontSize(11);

            var declaringType = fieldInfo.DeclaringType;
            var propPath      = property.propertyPath;
            var so            = property.serializedObject;

            void Refresh()
            {
                var p        = so.FindProperty(propPath);
                var stringId = p?.FindPropertyRelative(Constants.StringIdFieldName)?.stringValue;
                var isUnique = string.IsNullOrEmpty(stringId) || CheckIsUnique(p!, stringId!, declaringType);
                warningLabel.SetDisplay(isUnique ? DisplayStyle.None : DisplayStyle.Flex);
            }

            var idProp = property.FindPropertyRelative(Constants.StringIdFieldName);
            if (idProp != null)
                warningLabel.TrackPropertyValue(idProp, _ => Refresh());
            warningLabel.schedule.Execute(Refresh).StartingIn(0);

            return new VisualElement()
                .SetFlexDirection(FlexDirection.Column)
                .AddChild(mainElement)
                .AddChild(warningLabel);
        }

        private bool GetIsUnique(SerializedProperty structProp)
        {
            var idProp = structProp.FindPropertyRelative(Constants.StringIdFieldName);
            if (idProp == null) return true;

            var key = $"{structProp.serializedObject.targetObject.GetInstanceID()}:{structProp.propertyPath}:{idProp.stringValue}";
            var now = EditorApplication.timeSinceStartup;

            if (_cache.TryGetValue(key, out var cached) && now - cached.time < CacheLifetime)
                return cached.isUnique;

            var result = CheckIsUnique(structProp, idProp.stringValue, fieldInfo.DeclaringType);
            _cache[key] = (now, result);
            return result;
        }

        private static bool CheckIsUnique(SerializedProperty structProp, string idValue, Type? assetType)
        {
            if (assetType == null || string.IsNullOrEmpty(idValue)) return true;

            var currentObject = structProp.serializedObject.targetObject;
            var fullPath      = $"{structProp.propertyPath}.{Constants.StringIdFieldName}";
            var guids         = AssetDatabase.FindAssets($"t:{assetType.Name}");

            foreach (var guid in guids)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(guid);
                var asset     = AssetDatabase.LoadAssetAtPath(assetPath, assetType);
                if (asset == null || asset == currentObject) continue;

                var otherSo    = new SerializedObject(asset);
                var otherValue = otherSo.FindProperty(fullPath)?.stringValue;
                if (otherValue == idValue) return false;
            }

            return true;
        }
    }
}
