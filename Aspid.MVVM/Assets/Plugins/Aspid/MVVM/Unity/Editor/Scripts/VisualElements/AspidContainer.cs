#nullable enable
using System;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// A styled container visual element used in the Aspid MVVM inspector UI.
    /// Supports dark, light, and lighter visual style variants.
    /// </summary>
    public class AspidContainer : VisualElement
    {
        public static readonly StyleSheet StyleSheet = Resources.Load<StyleSheet>("Styles/Aspid-MVVM-AspidContainer");
        
        public AspidContainer(StyleType style = StyleType.Light)
        {
            styleSheets.Add(StyleSheet);
            AddToClassList(GetStyleClass(style));
        }

        public static string GetStyleClass(StyleType style) => style switch
        {
            StyleType.Dark => "aspid-dark-container",
            StyleType.Light => "aspid-light-container",
            StyleType.Lighter => "aspid-lighter-container",
            _ => throw new ArgumentOutOfRangeException(nameof(style), style, null)
        };

        public enum StyleType
        {
            Dark,
            Light,
            Lighter,
        }
    }
}