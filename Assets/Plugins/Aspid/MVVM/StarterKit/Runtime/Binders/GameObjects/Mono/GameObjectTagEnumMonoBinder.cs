using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Aspid/MVVM/Binders/GameObject/GameObject Binder - Tag Enum")]
    public sealed class GameObjectTagEnumMonoBinder : EnumMonoBinder<string>
    {
        protected override void SetValue(string value) =>
            gameObject.tag = value;
    }
}