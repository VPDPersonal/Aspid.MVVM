#nullable enable
using UnityEngine;
using Aspid.UnityFastTools;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    // TODO Aspid.MVVM Unity – Write summary
    public class AspidTitle : VisualElement
    {
        public static readonly StyleSheet StyleSheet = Resources.Load<StyleSheet>("Editor/Styles/aspid-mvvm-title");
        
        public AspidTitle(string? text)
        {
            text ??= string.Empty;
            
            // TODO Aspid.MVVM Unity – Rename Name
            var textContainer = new VisualElement().SetName("TextContainer")
                .AddChild(new Label(text).SetName("Text"));
            
            // TODO Aspid.MVVM Unity – Rename Name
            var line = new VisualElement().SetName("Line");
            
            Add(textContainer);
            Add(line);
            
            styleSheets.Add(StyleSheet);
        }
    }
}