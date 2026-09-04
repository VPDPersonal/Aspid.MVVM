using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds <see cref="LayoutElement.ignoreLayout"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(LayoutElement), serializePropertyNames: "m_IgnoreLayout")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LayoutElement/LayoutElement Binder – Ignore Layout")]
    public class LayoutElementIgnoreLayoutMonoBinder : ComponentMonoBinder<LayoutElement, bool>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.ignoreLayout;
            set => CachedComponent.ignoreLayout = value;
        }
    }
}
