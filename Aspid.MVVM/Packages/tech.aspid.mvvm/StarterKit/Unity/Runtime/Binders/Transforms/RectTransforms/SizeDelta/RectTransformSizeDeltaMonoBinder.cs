using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{RectTransform, Vector3}"/> that sets the <see cref="RectTransform.sizeDelta"/>
    /// according to the configured <see cref="SizeDeltaMode"/>.
    /// </summary>
    /// <remarks>
    /// Also implements <see cref="IReverseBinder{T}">IReverseBinder&lt;Vector2&gt;</see>. A rect's size is two numbers,
    /// and the Vector3 base this family is built on reports <c>Vector3(width, height, 0)</c> in
    /// <see cref="BindMode.OneWayToSource"/> — a value the property never held, and one a ViewModel field of type
    /// <see cref="Vector2"/> could not receive at all. The Vector2 channel is raised alongside the Vector3 one, so a
    /// ViewModel binds whichever type it holds.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(RectTransform))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RectTransform/RectTransform Binder – SizeDelta")]
    public class RectTransformSizeDeltaMonoBinder : ComponentMonoBinder<RectTransform, Vector3>, IVector3Binder, IReverseBinder<Vector2>
    {
        [Tooltip("Which axes of sizeDelta are modified.")]
        [SerializeField] private SizeDeltaMode _sizeMode = SizeDeltaMode.SizeDelta;

        private Action<Vector2> _sizeChanged;

        /// <summary>
        /// Raised with the rect's size as a <see cref="Vector2"/> when binding is established in
        /// <see cref="BindMode.OneWayToSource"/>.
        /// </summary>
        /// <remarks>
        /// Declared as an explicit interface implementation because the base class already publishes a
        /// <see cref="IReverseBinder{T}">IReverseBinder&lt;Vector3&gt;</see> event of the same name; interface mapping
        /// prefers the inherited member, so a second channel has to be spelled out to exist at all.
        /// </remarks>
        event Action<Vector2> IReverseBinder<Vector2>.ValueChanged
        {
            add => _sizeChanged += value;
            remove => _sizeChanged -= value;
        }

        protected sealed override Vector3 Property
        {
            get => CachedComponent.sizeDelta;
            set => CachedComponent.SetSizeDelta(value, _sizeMode);
        }

        /// <summary>
        /// Called when the binder is bound. Reports the size on both channels when using
        /// <see cref="BindMode.OneWayToSource"/>.
        /// </summary>
        /// <remarks>
        /// Calls the base implementation, which raises the Vector3 channel, and then raises the Vector2 one.
        /// </remarks>
        protected override void OnBound()
        {
            base.OnBound();

            if (Mode is BindMode.OneWayToSource)
                _sizeChanged?.Invoke(CachedComponent.sizeDelta);
        }
    }
}