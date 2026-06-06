using UnityEditor;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Ids.Editors
{
    internal static class IdStructIMGUIPropertyDrawer
    {
        private const float ButtonGap = 2f;
        private const float OpenButtonWidth = 22f;
        private const string OpenButtonIconName = "d_ScriptableObject Icon";

        private static GUIStyle _missingDropdownStyle;
        private static readonly Color _missingTextColor = new(r: 1f, g: 0.4f, b: 0.4f);

        public static float GetIMGUIHeight(IdStructDrawerContext ctx, bool isUnique, ref string lastStringId)
        {
            var height = EditorGUIUtility.singleLineHeight;
            if (!isUnique) return height;

            var stringId = ctx.StringProperty.stringValue;

            if (stringId != lastStringId)
            {
                lastStringId = stringId;
                UniqueIdIndex.RefreshAsset(ctx.SerializedObject.targetObject);
            }

            if (IsUniqueFor(ctx)) return height;
            return height + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        }

        public static void Draw(Rect position, IdStructDrawerContext ctx, bool isUnique)
        {
            IdStructDrawerHelper.SyncStringFromInt(ctx);

            if (!string.IsNullOrWhiteSpace(ctx.Label))
            {
                EditorGUI.LabelField(position, ctx.Label);
                position.x += EditorGUIUtility.labelWidth;
                position.width -= EditorGUIUtility.labelWidth;
            }

            var (dropRect, openRect) = SplitRect(position);

            var caption = IdStructDrawerHelper.BuildCaption(ctx, out var isMissing);
            var dropdownStyle = isMissing ? GetMissingDropdownStyle() : EditorStyles.miniPullDown;
            
            if (EditorGUI.DropdownButton(dropRect, new GUIContent(caption), FocusType.Passive, dropdownStyle))
            {
                var screen = GUIUtility.GUIToScreenPoint(new Vector2(dropRect.x, dropRect.y));
                var screenRect = new Rect(screen.x, screen.y, dropRect.width, dropRect.height);
                IdSelectorWindow.Show(screenRect, ctx.FieldType, ctx.StringProperty.stringValue, selected => IdStructDrawerHelper.ApplySelection(selected, ctx));
            }

            using (new EditorGUI.DisabledScope(ctx.FindRegistry() is null))
            {
                if (GUI.Button(openRect, EditorGUIUtility.IconContent(OpenButtonIconName)))
                    ctx.OpenRegistryAsset();
            }
            
            if (!isUnique) return;
            if (IsUniqueFor(ctx)) return;

            var warningY = position.y + position.height + EditorGUIUtility.standardVerticalSpacing;
            var warningRect = new Rect(position.x, warningY, position.width, EditorGUIUtility.singleLineHeight);
           
            EditorGUI.HelpBox(warningRect, "ID is not unique among assets of this type", MessageType.Warning);
        }
        
        private static bool IsUniqueFor(IdStructDrawerContext ctx) =>
            UniqueIdIndex.IsUnique(ctx.DeclaringType, ctx.StringProperty.stringValue, ctx.GetCurrentAssetGuid());

        private static (Rect dropRect, Rect openRect) SplitRect(Rect position)
        {
            var height = EditorGUIUtility.singleLineHeight;
            var dropRect = new Rect(position.x, position.y, position.width - OpenButtonWidth - ButtonGap, height);
            var openRect = new Rect(dropRect.xMax + ButtonGap, position.y, OpenButtonWidth, height);
          
            return (dropRect, openRect);
        }

        private static GUIStyle GetMissingDropdownStyle()
        {
            if (_missingDropdownStyle is not null)
                return _missingDropdownStyle;
          
            var state = new GUIStyleState
            {
                textColor = _missingTextColor
            };
            
            return _missingDropdownStyle = new GUIStyle(EditorStyles.miniPullDown)
            {
                normal = state,
                hover = state,
                focused = state,
                active = state,
            };
        }
    }
}
