using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [BindModeOverride(modes: BindMode.OneTime)]
    [AddComponentMenu("Aspid/MVVM/Binders/GameObject/GameObject Binder – Visible By Bind")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/GameObject/GameObject Binder – Visible By Bind")]
    public sealed class GameObjectVisibleByBindMonoBinder : MonoBinder, IAnyBinder
    {
        [SerializeField] private bool _isInvert;
        
        private void OnEnable() =>
            SetVisible();

        protected override void OnBound() => 
            SetVisible();

        protected override void OnUnbound() => 
            SetVisible();

        public void SetValue<T>(T value) { }
        
        private void SetVisible() =>
            gameObject.SetActive(_isInvert ? !IsBound : IsBound);
    }
}