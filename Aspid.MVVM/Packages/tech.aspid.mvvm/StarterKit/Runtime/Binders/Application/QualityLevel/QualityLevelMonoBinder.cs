using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="IntMonoBinder"/> that binds <see cref="QualitySettings.GetQualityLevel"/>.
    /// </summary>
    /// <remarks>
    /// The index is clamped to the levels the project defines; expensive changes apply immediately.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Application/Quality Level")]
    [AddComponentMenu("Aspid/MVVM/Binders/Application/Application Binder – Quality Level")]
    public class QualityLevelMonoBinder : IntMonoBinder
    {
        /// <inheritdoc/>
        protected sealed override int Property
        {
            get => QualitySettings.GetQualityLevel();
            set => QualitySettings.SetQualityLevel(
                index: Mathf.Clamp(value, 0, QualitySettings.names.Length - 1),
                applyExpensiveChanges: true);
        }
    }
}
