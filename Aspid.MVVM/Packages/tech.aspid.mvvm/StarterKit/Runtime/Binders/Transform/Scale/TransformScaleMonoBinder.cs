using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds <see cref="Transform.localScale"/>,
    /// as a <see cref="Vector3"/> or a single number applied to all three axes.
    /// </summary>
    /// <remarks>
    /// Only a finite value is applied.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(Transform), serializePropertyNames: "m_LocalScale")]
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder – Scale")]
    public partial class TransformScaleMonoBinder : ComponentMonoBinder<Transform, Vector3>, IVector3Binder, IFloatBinder
    {
        /// <inheritdoc/>
        protected sealed override Vector3 Property
        {
            get => CachedComponent.localScale;
            set
            {
                if (this.RequireFinite(value))
                    CachedComponent.localScale = value;
            }
        }

        /// <summary>
        /// Applies <paramref name="value"/> as a uniform scale.
        /// </summary>
        /// <param name="value">The scale received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(float value) =>
            SetValue(new Vector3(value, value, value));
    }
}
