using UnityEngine;

namespace UltimateUI.MVVM.StarterKit.Binders
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