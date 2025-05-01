using System;

namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public abstract class TargetBinder<TTarget> : Binder
    {
#if UNITY_2022_1_OR_NEWER
        [field: UnityEngine.Header("Target")]
        [field: UnityEngine.SerializeField]
#endif
        protected TTarget Target { get; private set; }

        protected TargetBinder(TTarget target, BindMode mode)
            : base(mode)
        {
            Target = target ?? throw new ArgumentNullException(nameof(target));
        }
    }
}