using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{GameObject, bool}"/> that shows or hides each <see cref="GameObject"/>
    /// in the group based on a value resolved from the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/GameObject/GameObject Binder – Visible EnumGroup")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/GameObject/EnumGroup/GameObject Binder – Visible EnumGroup")]
    public sealed class GameObjectVisibleEnumGroupMonoBinder : EnumGroupMonoBinder<GameObject, bool>
    {
        /// <inheritdoc/>
        protected override void SetValue(GameObject element, bool value) =>
            element.SetActive(value);
    }
}