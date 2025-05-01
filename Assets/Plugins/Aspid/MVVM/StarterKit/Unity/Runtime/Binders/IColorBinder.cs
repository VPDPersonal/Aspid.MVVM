using UnityEngine;

namespace Aspid.MVVM.StarterKit.Unity
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