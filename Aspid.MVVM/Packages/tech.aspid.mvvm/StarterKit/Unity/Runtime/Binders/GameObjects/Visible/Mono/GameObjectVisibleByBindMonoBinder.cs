using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> that shows or hides the <see cref="GameObject"/> this component is attached to
    /// depending on whether the ViewModel exposes a matching binding field for this binder.
    /// </summary>
    /// <remarks>
    /// Unlike conventional binders, this binder does not forward ViewModel values to a property.
    /// Instead, it calls <see cref="GameObject.SetActive"/> with <see langword="true"/> when the ViewModel
    /// contains the expected binding field, and with <see langword="false"/> when it does not
    /// (or vice-versa when inversion is enabled).
    /// <para>
    /// Only <see cref="BindMode.OneTime"/> is supported.
    /// </para>
    /// </remarks>
    [BindModeOverride(modes: BindMode.OneTime)]
    [AddComponentMenu("Aspid/MVVM/Binders/GameObject/GameObject Binder – Visible By Bind")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/GameObject/GameObject Binder – Visible By Bind")]
    public sealed partial class GameObjectVisibleByBindMonoBinder : MonoBinder, IAnyBinder
    {
        [Tooltip("When enabled, inverts the bound bool value before applying it.")]
        [SerializeField] private bool _isInvert;
        
        private void OnEnable() =>
            SetVisible();
        
        protected override void OnBound() => 
            SetVisible();

        protected override void OnUnbound() => 
            SetVisible();

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue<T>(T value) { }
        
        private void SetVisible() =>
            gameObject.SetActive(_isInvert ? !IsBound : IsBound);
    }
}