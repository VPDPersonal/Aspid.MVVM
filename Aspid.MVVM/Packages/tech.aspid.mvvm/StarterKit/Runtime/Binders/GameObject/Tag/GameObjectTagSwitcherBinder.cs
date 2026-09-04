#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherBinder{TTarget, T}"/> that switches <see cref="GameObject.tag"/>.
    /// </summary>
    /// <remarks>
    /// Unity throws when the tag is not declared in Tags and Layers.
    /// </remarks>
    [Serializable]
    public sealed class GameObjectTagSwitcherBinder : SwitcherBinder<GameObject, string>
    {
        /// <inheritdoc/>
        public GameObjectTagSwitcherBinder(
            GameObject target,
            string trueValue,
            string falseValue,
            IConverter<string?, string?>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode) { }

        /// <inheritdoc/>
        protected override void SetValue(string value) =>
            Target.tag = value;
    }
}
