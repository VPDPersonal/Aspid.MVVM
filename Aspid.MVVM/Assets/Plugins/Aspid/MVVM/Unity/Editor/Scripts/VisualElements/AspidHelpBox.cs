#nullable enable
using System;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// A styled help box visual element used in the Aspid MVVM inspector UI.
    /// Extends <see cref="HelpBox"/> with the <c>aspid__mvvm__aspid-help-box</c> style sheet and type-specific style classes.
    /// </summary>
    public class AspidHelpBox : HelpBox
    {
        private static readonly StyleSheet _styleSheet = Resources.Load<StyleSheet>(path: "Styles/Aspid-MVVM-AspidHelpBox");
        
        public AspidHelpBox(string message, HelpBoxMessageType type)
            : base(message, type)
        {
            styleSheets.Add(_styleSheet);
            AddToClassList(GetStyleClass(type));
        }

        private static string GetStyleClass(HelpBoxMessageType type) => type switch
        {
            HelpBoxMessageType.None => "aspid-help-box-none",
            HelpBoxMessageType.Info => "aspid-help-box-info",
            HelpBoxMessageType.Error => "aspid-help-box-error",
            HelpBoxMessageType.Warning => "aspid-help-box-warning",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}