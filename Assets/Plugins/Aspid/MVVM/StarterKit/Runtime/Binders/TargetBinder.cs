#nullable enable
using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public abstract class TargetBinder<TTarget> : Binder
    {
        [field: Header("Target")]
        [field: SerializeField]
        protected TTarget Target { get; private set; }

        protected TargetBinder(TTarget target, BindMode mode)
            : base(mode)
        {
            Target = target ?? throw new ArgumentNullException(nameof(target));
        }
    }
}