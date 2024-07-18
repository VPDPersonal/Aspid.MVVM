using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Bindings
{
    public interface IColorTargetBinding : ITargetBinding<Color>, ITargetBinding<string>
    {
        void ITargetBinding<string>.SetValue(string value)
        {
            ColorUtility.TryParseHtmlString(value, out var color);
            SetValue(color);
        }
    }
}