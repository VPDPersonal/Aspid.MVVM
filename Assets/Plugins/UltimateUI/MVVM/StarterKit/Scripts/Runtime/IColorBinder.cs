using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit
{
    public interface IColorBinder : IBinder<Color>, IBinder<string>
    {
        void IBinder<string>.SetValue(string value)
        {
            ColorUtility.TryParseHtmlString(value, out var color);
            SetValue(color);
        }
    }
}