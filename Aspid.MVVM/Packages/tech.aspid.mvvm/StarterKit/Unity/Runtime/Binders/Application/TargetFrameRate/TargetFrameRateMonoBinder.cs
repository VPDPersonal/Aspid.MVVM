using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="IntMonoBinder"/> that binds <see cref="Application.targetFrameRate"/>.
    /// </summary>
    /// <remarks>
    /// Values below <c>-1</c> are clamped to <c>-1</c>, which hands the decision back to the platform. When
    /// <see cref="QualitySettings.vSyncCount"/> is not zero, vsync wins and the cap is ignored.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Application/TargetFrameRate")]
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
