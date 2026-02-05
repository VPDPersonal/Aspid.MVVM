#nullable enable
using System;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    // TODO Aspid.MVVM Unity – Write summary
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