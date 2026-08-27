using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentBoolMonoBinder{LayoutElement}"/> that binds <see cref="LayoutElement.ignoreLayout"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(LayoutElement), serializePropertyNames: "m_IgnoreLayout")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LayoutElement/LayoutElement Binder – Ignore Layout")]
    public class LayoutElementIgnoreLayoutMonoBinder : ComponentBoolMonoBinder<LayoutElement>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.ignoreLayout;
            set => CachedComponent.ignoreLayout = value;
        }
    }
}
