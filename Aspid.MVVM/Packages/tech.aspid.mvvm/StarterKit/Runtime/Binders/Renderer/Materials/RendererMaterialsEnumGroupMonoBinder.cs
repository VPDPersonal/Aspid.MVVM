using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement}"/> that sets <see cref="Renderer.materials"/> on each element.
    /// </summary>
    [AddBinderContextMenu(typeof(Renderer), serializePropertyNames: "m_Materials", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/Renderer Binder – Materials EnumGroup")]
    public sealed class RendererMaterialsEnumGroupMonoBinder : EnumGroupMonoBinder<Renderer>
    {
        [Tooltip("Materials applied to elements that do not match the bound enum value.")]
        [SerializeField] private Material[] _defaultValue;

        [Tooltip("Materials applied to the element that matches the bound enum value.")]
        [SerializeField] private Material[] _selectedValue;

        [Tooltip("Optional converter applied to each default material; empty leaves it as-is.")]
        [SerializeReference] private IConverter<Material, Material> _defaultValueConverter;

        [Tooltip("Optional converter applied to each selected material; empty leaves it as-is.")]
        [SerializeReference] private IConverter<Material, Material> _selectedValueConverter;

        private readonly MaterialBindingScope _materialScope = new();

        /// <inheritdoc/>
        protected override void OnDestroy()
        {
            try
            {
                base.OnDestroy();
            }
            finally
            {
                _materialScope.Dispose();
            }
        }

        /// <inheritdoc/>
        protected override void SetDefaultValue(Renderer element) =>
            _materialScope.Set(element, _defaultValueConverter, _defaultValue);

        /// <inheritdoc/>
        protected override void SetSelectedValue(Renderer element) =>
            _materialScope.Set(element, _selectedValueConverter, _selectedValue);

        /// <inheritdoc/>
        protected override void OnDetach()
        {
            _materialScope.Dispose();
            base.OnDetach();
        }
    }
}
