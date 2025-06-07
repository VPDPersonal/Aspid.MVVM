using UnityEngine;
using Aspid.MVVM.Unity;

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddComponentMenu("Aspid/MVVM/Binders/GameObject/GameObject Binder - Tag Switcher")]
    [AddComponentContextMenu(typeof(Component),"Add GameObject Binder/GameObject Binder - Tag Switcher")]
    public sealed class GameObjectTagSwitcherMonoBinder : SwitcherMonoBinder<string>
    {
        protected override void SetValue(string value) =>
            gameObject.tag = value;
    }
}