using System;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public class AspidHelpBox : HelpBox
    {
        public AspidHelpBox(string message, HelpBoxMessageType type)
            : base(message, type)
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Editor/Styles/aspid-mvvm-help-box"));

            switch (type)
            {
                case HelpBoxMessageType.None: AddToClassList("aspid-help-box-none"); break;
                case HelpBoxMessageType.Info: AddToClassList("aspid-help-box-info"); break;
                case HelpBoxMessageType.Warning: AddToClassList("aspid-help-box-warning"); break;
                case HelpBoxMessageType.Error: AddToClassList("aspid-help-box-error"); break;
                default: throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}