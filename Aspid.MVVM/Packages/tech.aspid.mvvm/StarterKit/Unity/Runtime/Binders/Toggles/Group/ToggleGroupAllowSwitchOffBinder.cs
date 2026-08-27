#nullable enable
using System;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBoolBinder{ToggleGroup}"/> that binds <see cref="ToggleGroup.allowSwitchOff"/>.
    /// </summary>
    /// <remarks>
    /// Whether the group may end up with nothing selected — the difference between a filter the player can clear and a
    /// set of tabs that must always have one open.
    /// <para/>
    /// Turning it off does not select anything: Unity leaves an already-empty group empty.
    /// </remarks>
    [Serializable]
    public class ToggleGroupAllowSwitchOffBinder : TargetBoolBinder<ToggleGroup>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.allowSwitchOff;
            set => Target.allowSwitchOff = value;
        }

        /// <inheritdoc/>
        public ToggleGroupAllowSwitchOffBinder(
            ToggleGroup target,
            IConverter<bool, bool>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
