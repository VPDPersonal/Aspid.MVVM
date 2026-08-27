#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{T1, T2}">TargetBinder&lt;Selectable, Selectable.Transition&gt;</see> that binds
    /// <see cref="Selectable.transition"/>.
    /// </summary>
    [Serializable]
    public class SelectableTransitionBinder : TargetBinder<Selectable, Selectable.Transition>
    {
        /// <inheritdoc/>
        protected sealed override Selectable.Transition Property
        {
            get => Target.transition;
            set => Target.transition = value;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> — the property raises no change event to listen to.</exception>
        public SelectableTransitionBinder(Selectable target, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}
