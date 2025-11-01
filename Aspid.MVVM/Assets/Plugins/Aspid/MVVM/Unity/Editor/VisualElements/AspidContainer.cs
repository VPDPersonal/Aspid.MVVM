using System;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public class AspidContainer : VisualElement
    {
        public AspidContainer(StyleType style = StyleType.Light)
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Editor/Styles/aspid-mvvm-container"));
            
            switch (style)
            {
                case StyleType.Dark: AddToClassList("aspid-dark-container"); break;
                case StyleType.Light: AddToClassList("aspid-light-container"); break;
                case StyleType.Lighter: AddToClassList("aspid-lighter-container"); break;
                default: throw new ArgumentOutOfRangeException(nameof(style), style, null);
            }
        }

        public enum StyleType
        {
            Dark,
            Light,
            Lighter,
        }
    }
}