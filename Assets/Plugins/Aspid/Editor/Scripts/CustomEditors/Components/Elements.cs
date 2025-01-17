#nullable enable
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace Aspid.CustomEditors
{
    public static class Elements
    {
	    public static VisualElement CreateHeader(Object obj, string iconPath) =>
		    CreateHeader(iconPath, obj.GetScriptName());
	    
        public static VisualElement CreateHeader(string iconPath, string scriptName)
        {
            var headerIcon = new Image()
                .SetName("HeaderIcon")
                .SetImageFromResource(iconPath)
                .SetSize(40, 40);

            var headerText = new Label(scriptName)
                .SetFlexGrow(1)
                .SetFontSize(16)
                .SetFlexShrink(1)
                .SetName("HeaderText")
                .SetAlignSelf(Align.Center)
                .SetOverflow(Overflow.Hidden)
                .SetColor(EditorColor.LightText)
                .SetWhiteSpace(WhiteSpace.NoWrap)
                .SetTextOverflow(TextOverflow.Ellipsis)
                .SetUnityFontStyleAndWeight(FontStyle.Bold);

            return CreateContainer(EditorColor.DarkContainer, "Header")
                .SetFlexDirection(FlexDirection.Row)
                .AddChild(headerIcon)
                .AddChild(headerText
                    .SetMargin(left: 10));
        }
        
        public static VisualElement CreateContainer(StyleColor color, string? name = null)
        {
            var container = new VisualElement()
                .SetBackgroundColor(color)
                .SetPadding(5, 5, 10, 10)
                .SetBorderRadius(10, 10, 10, 10);

            if (!string.IsNullOrEmpty(name)) container.SetName(name);
            return container;
        }
        
        public static VisualElement CreateTitle(StyleColor color, string text, string? name = null)
        {
            var label = new Label(text)
                .SetName("Text")
                .SetColor(color)
                .SetFontSize(14)
                .SetUnityFontStyleAndWeight(FontStyle.Bold);

            var line = new VisualElement()
                .SetName("Line")
                .SetBackgroundColor(color)
                .SetMargin(top: 5, bottom: 5)
                .SetSize(new StyleLength(new Length(100, LengthUnit.Percent)), 2);
            
            var title = new VisualElement()
                .AddChild(label)
                .AddChild(line);

            if (!string.IsNullOrEmpty(name)) title.SetName(name);
            return title;
        }
        
        public static HelpBox CreateHelpBox(string text, HelpBoxMessageType type, string? name = "HelpBox")
        {
            var helpBox = new HelpBox(text, type)
                .SetBackgroundColor(EditorColor.ErrorBackground)
                .SetSize(new StyleLength(new Length(100, LengthUnit.Percent)));
            
            switch (type)
            {
                case HelpBoxMessageType.Error:
                    helpBox.SetBackgroundColor(EditorColor.ErrorBackground);
                    break;
                
                case HelpBoxMessageType.Warning:
                    helpBox.SetBackgroundColor(EditorColor.WarningBackground);
                    break;
            }
            
            if (!string.IsNullOrEmpty(name)) helpBox.name = name;
            return helpBox;
        }
    }
}