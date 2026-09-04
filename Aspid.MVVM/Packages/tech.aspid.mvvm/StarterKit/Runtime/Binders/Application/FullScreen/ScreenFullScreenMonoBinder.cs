using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder{TProperty}"/> that binds <see cref="Screen.fullScreen"/>.
    /// </summary>
    /// <remarks>
    /// Unity applies the change at the end of the frame.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Application/Full Screen")]
    [AddComponentMenu("Aspid/MVVM/Binders/Application/Application Binder – Full Screen")]
    public class ScreenFullScreenMonoBinder : MonoBinder<bool>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Screen.fullScreen;
            set => Screen.fullScreen = value;
        }
    }
}
