using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds <see cref="RectTransform.sizeDelta"/>.
    /// </summary>
    /// <remarks>
    /// Only a finite value is applied. In <see cref="BindMode.OneWayToSource"/> the size is reported both as
    /// <see cref="Vector3"/> and as <see cref="Vector2"/>, so the ViewModel binds whichever type it holds.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(RectTransform), serializePropertyNames: "m_SizeDelta")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RectTransform/RectTransform Binder – SizeDelta")]
    public class RectTransformSizeDeltaMonoBinder : ComponentMonoBinder<RectTransform, Vector3>,
        IVector3Binder,
        IReverseBinder<Vector2>
    {
        [Tooltip("Which axes of sizeDelta are written.")]
        [SerializeField] private SizeDeltaMode _sizeMode = SizeDeltaMode.SizeDelta;

        private Action<Vector2> _sizeChanged;

        event Action<Vector2> IReverseBinder<Vector2>.ValueChanged
        {
            add => _sizeChanged += value;
            remove => _sizeChanged -= value;
        }

        /// <inheritdoc/>
        protected sealed override Vector3 Property
        {
            get => CachedComponent.sizeDelta;
            set => CachedComponent.SetSizeDelta(value, _sizeMode);
        }

        /// <inheritdoc/>
        protected override void OnBound()
        {
            base.OnBound();

            if (Mode is BindMode.OneWayToSource)
                _sizeChanged?.Invoke(CachedComponent.sizeDelta);
        }
    }
}
