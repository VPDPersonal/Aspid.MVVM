#nullable enable
using System;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// A styled help box visual element used in the Aspid MVVM inspector UI.
    /// Extends <see cref="HelpBox"/> with the <c>aspid-mvvm-help-box</c> style sheet and type-specific style classes.
    /// </summary>
    public class AspidHelpBox : HelpBox
    {
        public static readonly StyleSheet StyleSheet = Resources.Load<StyleSheet>("Styles/aspid-mvvm-help-box");
        
        public AspidHelpBox(string message, HelpBoxMessageType type)
            : base(message, type)
        {
            styleSheets.Add(StyleSheet);
            AddToClassList(GetStyleClass(type));
        }
        
        public static string GetStyleClass(HelpBoxMessageType type) => type switch
        {
            HelpBoxMessageType.None => "aspid-help-box-none",
            HelpBoxMessageType.Info => "aspid-help-box-info",
            HelpBoxMessageType.Warning => "aspid-help-box-warning",
            HelpBoxMessageType.Error => "aspid-help-box-error",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}