using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{GameObject, string}"/> that sets the <see cref="GameObject.tag"/> property
    /// on each <see cref="GameObject"/> in the group based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/GameObject/GameObject Binder – Tag EnumGroup")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/GameObject/EnumGroup/GameObject Binder – Tag EnumGroup")]
    public sealed class GameObjectTagEnumGroupMonoBinder : EnumGroupMonoBinder<GameObject, string>
    {
        /// <inheritdoc/>
        protected override void SetValue(GameObject element, string value) =>
            element.tag = value;
    }
}