using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{bool}"/> that shows or hides the <see cref="GameObject"/> this component
    /// is attached to based on a value resolved from the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/GameObject/GameObject Binder – Visible Enum")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/GameObject/Enum/GameObject Binder – Visible Enum")]
    public sealed class GameObjectVisibleEnumMonoBinder : EnumMonoBinder<bool>
    {
        /// <inheritdoc/>
        protected override void SetValue(bool value) =>
            gameObject.SetActive(value);
    }
}