#nullable enable
using TMPro;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetIntBinder{TTarget}"/> that binds <see cref="TMP_Dropdown.value"/>.
    /// </summary>
    [Serializable]
    public class DropdownValueBinder : TargetIntBinder<TMP_Dropdown>
    {
        [Tooltip("When writes from the ViewModel raise onValueChanged; user input always does.")]
        [SerializeField] private NotifyMode _notifyMode = NotifyMode.Never;

        /// <summary>
        /// Creates a binder without a target for Unity deserialization, preserving silent writes in existing assets.
        /// </summary>
        protected DropdownValueBinder() { }

        /// <param name="target">The dropdown to bind.</param>
        /// <param name="notifyMode">When writes from the ViewModel raise <see cref="TMP_Dropdown.onValueChanged"/>.</param>
        /// <param name="converter"> The converter applied to the bound value, or <see langword="null"/> to use it as-is. </param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="ArgumentException"><paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public DropdownValueBinder(
            TMP_Dropdown target,
            NotifyMode notifyMode = NotifyMode.Never,
            IConverter<int, int>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
            _notifyMode = notifyMode;
        }

        /// <inheritdoc/>
        protected sealed override int Property
        {
            get => Target.value;
            set
            {
                if (_notifyMode.ShouldNotify(IsBound)) Target.value = value;
                else Target.SetValueWithoutNotify(value);
            }
        }
    }
}
