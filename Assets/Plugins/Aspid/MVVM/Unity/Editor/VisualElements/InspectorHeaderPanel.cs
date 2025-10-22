using UnityEngine;
using Aspid.CustomEditors;
using Aspid.UnityFastTools;
using UnityEngine.UIElements;
using Aspid.UnityFastTools.Editors;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public sealed class InspectorHeaderPanel : VisualElement
    {
        public readonly Image Icon;
        public readonly Label Label;
        
        public InspectorHeaderPanel(Object obj, string iconPath) 
            : this(obj.GetScriptName(), obj, iconPath) { }
        
        public InspectorHeaderPanel(string label, Object obj, string iconPath)
        {
            Icon = new Image()
                .SetName("InspectorHeaderPanelImage")
                .SetImageFromResource(iconPath)
                .SetSize(40, 40)
                .AddOpenScriptCommand(obj);
            
            Label = new Label(label)
                .SetName("InspectorHeaderPanelLabel")
                .SetFlexGrow(1)
                .SetFontSize(16)
                .SetFlexShrink(1)
                .SetAlignSelf(Align.Center)
                .SetOverflow(Overflow.Hidden)
                .SetColor(EditorColor.LightText)
                .SetWhiteSpace(WhiteSpace.NoWrap)
                .SetTextOverflow(TextOverflow.Ellipsis)
                .SetUnityFontStyleAndWeight(FontStyle.Bold);
            
            this.SetName("InspectorHeaderPanel")
                .SetBackgroundColor(EditorColor.DarkContainer)
                .SetFlexDirection(FlexDirection.Row)
                .SetPadding(5, 5, 10, 10)
                .SetBorderRadius(10, 10, 10, 10);

            this.AddChild(Icon)
                .AddChild(Label
                    .SetMargin(left: 10));
        }
    }
}