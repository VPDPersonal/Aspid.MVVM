using UnityEngine;
using Aspid.UnityFastTools;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public sealed class AspidTitle : VisualElement
    {
        public AspidTitle(string text)
        {
            var textContainer = new VisualElement().SetName("TextContainer")
                .AddChild(new Label(text).SetName("Text"));
            
            var line = new VisualElement().SetName("Line");
            
            Add(textContainer);
            Add(line);
            
            styleSheets.Add(Resources.Load<StyleSheet>("Editor/Styles/aspid-mvvm-title"));
        }
    }
}