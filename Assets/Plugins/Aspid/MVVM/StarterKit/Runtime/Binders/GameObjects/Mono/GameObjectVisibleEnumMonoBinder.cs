using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/GameObject/GameObject Binder - Visible Enum")]
    public sealed class GameObjectVisibleEnumMonoBinder : EnumMonoBinder<bool>
    {
        protected override void SetValue(bool value) =>
            gameObject.SetActive(value);
    }
}