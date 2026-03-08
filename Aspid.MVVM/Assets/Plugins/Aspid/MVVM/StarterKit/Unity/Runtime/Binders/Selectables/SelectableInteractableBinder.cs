#nullable enable
using System;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Binder that sets the <see cref="Selectable.interactable"/> property on a <see cref="Selectable"/>
    /// when the bound ViewModel value changes. Supports optional value inversion.
    /// </summary>
    [Serializable]
    public class SelectableInteractableBinder : TargetBoolBinder<Selectable>
    {
        protected sealed override bool Property
        {
            get => Target.interactable;
            set => Target.interactable = value;
        }

        public SelectableInteractableBinder(Selectable target, BindMode mode)
            : this(target, isInvert: false, mode) { }

        public SelectableInteractableBinder(Selectable target, bool isInvert = false, BindMode mode = BindMode.OneTime)
            : base(target, isInvert, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}