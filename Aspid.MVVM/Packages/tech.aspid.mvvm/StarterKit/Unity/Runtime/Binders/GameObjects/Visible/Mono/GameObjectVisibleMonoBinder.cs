using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="BoolMonoBinder"/> that shows or hides the <see cref="GameObject"/> this component is attached to.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/GameObject/GameObject Binder – Visible")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/GameObject/GameObject Binder – Visible")]
    public sealed class GameObjectVisibleMonoBinder : BoolMonoBinder
    {
        /// <inheritdoc/>
        protected override bool Property
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }
    }
}
