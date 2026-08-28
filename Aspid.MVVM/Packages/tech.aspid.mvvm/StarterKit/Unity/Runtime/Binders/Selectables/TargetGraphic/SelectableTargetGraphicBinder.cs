#nullable enable
using System;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetObjectBinder{T1, T2}">TargetObjectBinder&lt;Selectable, Graphic&gt;</see> that binds
    /// <see cref="Selectable.targetGraphic"/>.
    /// </summary>
    /// <remarks>
    /// A destroyed graphic arrives as <see langword="null"/>, which leaves the control untinted rather than pointing at
    /// a graphic that no longer exists.
    /// </remarks>
    [Serializable]
    public class SelectableTargetGraphicBinder : TargetObjectBinder<Selectable, Graphic>
    {
        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> — the property raises no change event to listen to.</exception>
        public SelectableTargetGraphicBinder(Selectable target, IConverter<Graphic?, Graphic?>? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

        /// <inheritdoc/>
        protected sealed override Graphic? Property
        {
            get => Target.targetGraphic;
            set => Target.targetGraphic = value;
        }
    }
}
