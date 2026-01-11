using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/GameObject/GameObject Binder – Visible Enum")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/GameObject/GameObject Binder – Visible Enum")]
    public sealed class GameObjectVisibleEnumMonoBinder : EnumMonoBinder<bool>
    {
        protected override void SetValue(bool value) =>
            gameObject.SetActive(value);
    }
}