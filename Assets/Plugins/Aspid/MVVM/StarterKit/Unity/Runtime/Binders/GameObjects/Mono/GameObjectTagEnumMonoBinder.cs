using UnityEngine;
using Aspid.MVVM.Unity;

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddComponentMenu("Aspid/MVVM/Binders/GameObject/GameObject Binder - Tag Enum")]
    [AddComponentContextMenu(typeof(Component),"Add GameObject Binder/GameObject Binder - Tag Enum")]
    public sealed class GameObjectTagEnumMonoBinder : EnumMonoBinder<string>
    {
        protected override void SetValue(string value) =>
            gameObject.tag = value;
    }
}