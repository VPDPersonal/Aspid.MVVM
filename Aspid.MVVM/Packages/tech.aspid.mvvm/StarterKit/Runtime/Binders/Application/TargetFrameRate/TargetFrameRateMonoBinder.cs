using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="IntMonoBinder"/> that binds <see cref="Application.targetFrameRate"/>.
    /// </summary>
    /// <remarks>
    /// Values below -1 are raised to -1, the platform default. VSync overrides the cap.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Application/Target Frame Rate")]
    [AddComponentMenu("Aspid/MVVM/Binders/Application/Application Binder – Target Frame Rate")]
    public class TargetFrameRateMonoBinder : IntMonoBinder
    {
        /// <inheritdoc/>
        protected sealed override int Property
        {
            get => Application.targetFrameRate;
            set => Application.targetFrameRate = Mathf.Max(-1, value);
        }
    }
}
