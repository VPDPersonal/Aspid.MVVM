using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="BoolMonoBinder"/> that binds <see cref="Screen.fullScreen"/>.
    /// </summary>
    /// <remarks>
    /// Unity applies the change at the end of the frame, so reading the property back immediately still reports
    /// the old state.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/Application/Application Binder – Full Screen")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Application/FullScreen")]
    public class ScreenFullScreenMonoBinder : BoolMonoBinder
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Screen.fullScreen;
            set => Screen.fullScreen = value;
        }
    }
}
