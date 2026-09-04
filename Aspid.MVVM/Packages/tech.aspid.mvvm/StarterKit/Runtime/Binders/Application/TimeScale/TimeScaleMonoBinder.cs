using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="FloatMonoBinder"/> that binds <see cref="Time.timeScale"/>.
    /// </summary>
    /// <remarks>
    /// A negative value is raised to zero. Audio does not follow the time scale, see
    /// <see cref="AudioListenerPauseMonoBinder"/>.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Application/Time Scale")]
    [AddComponentMenu("Aspid/MVVM/Binders/Application/Application Binder – Time Scale")]
    public class TimeScaleMonoBinder : FloatMonoBinder
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => Time.timeScale;
            set => Time.timeScale = this.NonNegative(value);
        }
    }
}
