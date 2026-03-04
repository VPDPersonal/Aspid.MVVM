using UnityEditor;
using UnityEngine;
using Aspid.Internal;
using UnityEngine.UIElements;
using Aspid.FastTools.Editors;

// ReSharper disable CheckNamespace
namespace Aspid.FastTools.Samples.VisualElements
{
    [CustomEditor(typeof(VisualElementInspector))]
    public class VisualElementInspectorEditor : Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            const string iconPath = EditorConstants.AspidIconGreen;
            
            var scriptName = target.GetScriptName();
            var darkColor = new Color(0.15f, 0.15f, 0.15f);
            var lightColor = new Color(0.75f, 0.75f, 0.75f);
            
            return new VisualElement()
                .SetName("Header")
                .SetBackgroundColor(darkColor)
                .SetFlexDirection(FlexDirection.Row)
                .SetPadding(top : 5, bottom : 5, left: 10, right: 10)
                .SetBorderRadius(topLeft: 10, topRight:10, bottomLeft:10, bottomRight:10)
                .AddChild(new Image()
                    .SetName("HeaderIcon")
                    .AddOpenScriptCommand(target)
                    .SetImageFromResource(iconPath)
                    .SetSize(width: 40, height: 40))
                .AddChild(new Label(scriptName)
                    .SetName("HeaderText")
                    .SetFlexGrow(1)
                    .SetFontSize(16)
                    .SetFlexShrink(1)
                    .SetMargin(left: 10)
                    .SetColor(lightColor)
                    .SetAlignSelf(Align.Center)
                    .SetOverflow(Overflow.Hidden)
                    .SetWhiteSpace(WhiteSpace.NoWrap)
                    .SetTextOverflow(TextOverflow.Ellipsis)
                    .SetUnityFontStyleAndWeight(FontStyle.Bold)
                );
        }
    }
}