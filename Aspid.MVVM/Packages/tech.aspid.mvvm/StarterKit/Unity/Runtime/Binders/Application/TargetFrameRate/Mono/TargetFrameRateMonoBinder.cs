using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> implementing <see cref="INumberBinder"/> and
    /// <see cref="IReverseBinder{T}">IReverseBinder&lt;int&gt;</see> that binds <see cref="Application.targetFrameRate"/>.
    /// </summary>
    /// <remarks>
    /// The frame cap a battery-saver or a performance option sets. <c>-1</c> hands the decision back to the platform,
    /// and is the only negative value that means anything — everything below it is clamped to it.
    /// <para/>
    /// On a platform where <see cref="QualitySettings.vSyncCount"/> is not zero, vsync wins and the cap is ignored;
    /// that is Unity's rule, not this binder's.
    /// </remarks>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    [AddComponentMenu("Aspid/MVVM/Binders/Application/Application Binder – Target Frame Rate")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Application/TargetFrameRate")]
    public partial class TargetFrameRateMonoBinder : MonoBinder, INumberBinder, IReverseBinder<int>
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
            Application.targetFrameRate = Mathf.Max(-1, value);
        }

        /// <summary>
        /// Called when the binder is bound. Sends the current value to the ViewModel when using
        /// <see cref="BindMode.OneWayToSource"/>.
        /// </summary>
        protected override void OnBound()
        {
            if (Mode is BindMode.OneWayToSource)
                ValueChanged?.Invoke(Application.targetFrameRate);
        }
    }
}
