using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherVector3MonoBinder{Transform}"/> that switches the <see cref="Transform.localScale"/>
    /// between two values based on the bound boolean ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder – Scale Switcher")]
    [AddBinderContextMenu(typeof(Transform), serializePropertyNames: "m_LocalScale", SubPath = "Switcher")]
    public sealed class TransformScaleSwitcherMonoBinder : SwitcherVector3MonoBinder<Transform>
    {
        /// <summary>
        /// Called when applying the selected value to the <see cref="Transform.localScale"/>.
        /// </summary>
        protected override void SetValue(Vector3 value) =>
            transform.localScale = value;
    }
}