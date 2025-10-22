#nullable enable
using UnityEngine;
using Aspid.UnityFastTools;
using UnityEngine.UIElements;
using Aspid.UnityFastTools.Editors;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.CustomEditors
{
    public static class Elements
    {
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
            var textContainer = new VisualElement()
                .SetFlexDirection(FlexDirection.Row)
                .SetName("TextContainer")
                .SetAlignContent(Align.Center)
                .SetSize(width: Length.Percent(100));
            
            textContainer.style.justifyContent = Justify.SpaceBetween;

            textContainer.AddChild(new Label(text)
                .SetName("Text")
                .SetColor(color)
                .SetFontSize(14)
                .SetUnityFontStyleAndWeight(FontStyle.Bold));

            var line = new VisualElement()
                .SetName("Line")
                .SetBackgroundColor(color)
                .SetMargin(top: 5, bottom: 5)
                .SetSize(new StyleLength(new Length(100, LengthUnit.Percent)), 2);
            
            var title = new VisualElement()
                .AddChild(textContainer)
                .AddChild(line);

            if (!string.IsNullOrEmpty(name)) title.SetName(name);
            return title;
        }
        
        public static HelpBox CreateHelpBox(string text, HelpBoxMessageType type, string? name = "HelpBox")
        {
            var helpBox = new HelpBox(text, type)
                .SetSize(width: new StyleLength(new Length(100, LengthUnit.Percent)));
            
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