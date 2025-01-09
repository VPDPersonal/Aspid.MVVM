using UnityEngine;
using Aspid.MVVM.Mono;
using Aspid.MVVM.Mono.Generation;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("MVVM/Binders/GameObject/GameObject Binder - Tag")]
    public partial class GameObjectTagMonoBinder : MonoBinder, IBinder<string>
    {
        [BinderLog]
        public void SetValue(string value) =>
            gameObject.tag = value;
    }
}