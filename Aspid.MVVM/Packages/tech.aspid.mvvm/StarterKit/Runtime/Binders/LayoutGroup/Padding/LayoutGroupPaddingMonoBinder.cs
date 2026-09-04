using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds <see cref="LayoutGroup.padding"/>, also
    /// from a number applied to every selected side.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(LayoutGroup), serializePropertyNames: "m_Padding")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LayoutGroup/LayoutGroup Binder – Padding")]
    public partial class LayoutGroupPaddingMonoBinder : ComponentMonoBinder<LayoutGroup, RectOffset>, IIntBinder
    {
        [Tooltip("Padding sides the value writes.")]
        [SerializeField] private RectSides _sides = RectSides.All;

        [NonSerialized] private RectOffset _uniform;

        /// <inheritdoc/>
        protected sealed override RectOffset Property
        {
            get => CachedComponent.padding;
            set => CachedComponent.SetPadding(value, _sides);
        }

        /// <summary>
        /// Applies <paramref name="value"/> to every selected side.
        /// </summary>
        /// <param name="value">The padding received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(int value)
        {
            _uniform ??= new RectOffset();
            _uniform.left = _uniform.right = _uniform.top = _uniform.bottom = value;

            base.SetValue(_uniform);
        }
    }
}
