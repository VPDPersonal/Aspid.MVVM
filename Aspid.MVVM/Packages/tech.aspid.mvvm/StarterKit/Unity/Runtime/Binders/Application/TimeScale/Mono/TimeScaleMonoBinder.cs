using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="FloatMonoBinder"/> that binds <see cref="Time.timeScale"/>.
    /// </summary>
    /// <remarks>
    /// Negative and non-finite values are clamped to zero, which pauses the game rather than being rejected. Audio
    /// does not follow the timescale — see <see cref="AudioListenerPauseMonoBinder"/> to silence a paused game.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/Application/Application Binder – Time Scale")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Application/TimeScale")]
    public class TimeScaleMonoBinder : FloatMonoBinder
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => Time.timeScale;
            set => Time.timeScale = this.SafeClamp(value, 0f, float.MaxValue);
        }
    }
}
