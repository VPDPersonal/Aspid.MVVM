using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> implementing <see cref="INumberBinder"/> and
    /// <see cref="IReverseBinder{T}">IReverseBinder&lt;int&gt;</see> that binds the active <see cref="QualitySettings"/> level.
    /// </summary>
    /// <remarks>
    /// The graphics preset a settings screen offers. It is an index into the levels the project defines, so the
    /// binder clamps to the ones that exist rather than letting Unity throw on an index it does not have.
    /// <para/>
    /// The level is applied without waiting for the next frame, which is what a settings screen wants: the change is
    /// visible while the user is still looking at the option they picked.
    /// </remarks>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    [AddComponentMenu("Aspid/MVVM/Binders/Application/Application Binder – Quality Level")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Application/QualityLevel")]
    public partial class QualityLevelMonoBinder : MonoBinder, INumberBinder, IReverseBinder<int>
    {
        /// <inheritdoc/>
        public event Action<int> ValueChanged;

        /// <summary>
        /// Casts the value to <see langword="int"/> and applies it.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(long value) => SetValue((int)value);

        /// <inheritdoc cref="SetValue(long)"/>
        [BinderLog]
        public void SetValue(float value) => SetValue((int)value);

        /// <inheritdoc cref="SetValue(long)"/>
        [BinderLog]
        public void SetValue(double value) => SetValue((int)value);

        /// <summary>
        /// Applies the value.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(int value)
        {
            var levels = QualitySettings.names.Length;
            QualitySettings.SetQualityLevel(Mathf.Clamp(value, 0, levels - 1), applyExpensiveChanges: true);
        }

        /// <summary>
        /// Called when the binder is bound. Sends the current value to the ViewModel when using
        /// <see cref="BindMode.OneWayToSource"/>.
        /// </summary>
        protected override void OnBound()
        {
            if (Mode is BindMode.OneWayToSource)
                ValueChanged?.Invoke(QualitySettings.GetQualityLevel());
        }
    }
}
