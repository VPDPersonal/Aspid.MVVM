using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{Transform, Vector3}"/> that switches the <see cref="Transform.localScale"/>
    /// between two values based on the bound boolean ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder – Scale Switcher")]
    [AddBinderContextMenu(typeof(Transform), serializePropertyNames: "m_LocalScale", SubPath = "Switcher")]
    public sealed class TransformScaleSwitcherMonoBinder : SwitcherMonoBinder<Transform, Vector3>
    {
        /// <summary>
        /// Called when applying the selected value to the <see cref="Transform.localScale"/>.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        protected override void SetValue(Vector3 value) =>
            CachedComponent.localScale = value;
    }
}