using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="IntMonoBinder"/> that binds the active <see cref="QualitySettings"/> level.
    /// </summary>
    /// <remarks>
    /// Clamped to the range of levels the project defines, rather than letting Unity throw on an out-of-range
    /// index. Expensive changes are applied immediately instead of being deferred to the next frame.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/Application/Application Binder – Quality Level")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Application/QualityLevel")]
    public class QualityLevelMonoBinder : IntMonoBinder
    {
        /// <inheritdoc/>
        protected sealed override int Property
        {
            get => QualitySettings.GetQualityLevel();
            set
            {
                var levels = QualitySettings.names.Length;
                var index = Mathf.Clamp(value, min: 0, max: levels - 1);

                QualitySettings.SetQualityLevel(index, applyExpensiveChanges: true);
            }
        }
    }
}
