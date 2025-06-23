using UnityEngine;
using Aspid.MVVM.Unity;

namespace Aspid.MVVM.StarterKit.Unity
{
    [BindModeOverride(BindMode.OneTime)]
    [AddComponentMenu("Aspid/MVVM/Binders/GameObject/GameObject Binder - Visible By Bind")]
    [AddComponentContextMenu(typeof(Component),"Add General Binder/GameObject/GameObject Binder - Visible By Bind")]
    public sealed class GameObjectVisibleByBindMonoBinder : MonoBinder, IAnyBinder
    {
        [SerializeField] private bool _isInvert;

        private void OnValidate() =>
            SetVisible();

        private void OnEnable() =>
            SetVisible();

        protected override void OnBound() => SetVisible();

        protected override void OnUnbound() => SetVisible();

        public void SetValue<T>(T value) { }
        
        private void SetVisible() =>
            gameObject.SetActive(_isInvert ? !IsBound : IsBound);
    }
}