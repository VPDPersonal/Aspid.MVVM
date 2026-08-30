using System;
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{Renderer}"/> that sets the <see cref="Renderer.material"/> or <see cref="Renderer.materials"/> array.
    /// </summary>
    [GenerateSerializableBinder]
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/Renderer Binder – Materials")]
    [AddBinderContextMenu(typeof(Renderer), serializePropertyNames: "m_Materials")]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public sealed partial class RendererMaterialsMonoBinder : ComponentMonoBinder<Renderer>, 
        IBinder<Material>, 
        IReverseBinder<Material>,
        IReverseBinder<Material[]>,
        IBinder<IReadOnlyCollection<Material>>
    {
        [Tooltip("The optional converter applied to each material before assignment.")]
        [SerializeReference] private IConverter<Material, Material> _converter;

        private Action<Material> _reverseMaterial;
        private Action<Material[]> _reverseMaterials;

        event Action<Material> IReverseBinder<Material>.ValueChanged
        {
            add => _reverseMaterial += value;
            remove => _reverseMaterial -= value;
        }

        event Action<Material[]> IReverseBinder<Material[]>.ValueChanged
        {
            add => _reverseMaterials += value;
            remove => _reverseMaterials -= value;
        }

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(Material value) =>
            CachedComponent.material = GetConvertedValue(value);

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(IReadOnlyCollection<Material> values) =>
            CachedComponent.SetMaterials(_converter, values);

        /// <summary>
        /// Called after binding is established.
        /// Sends the current material(s) back to the ViewModel when in <see cref="BindMode.OneWayToSource"/> mode.
        /// </summary>
        protected override void OnBound()
        {
            if (Mode is BindMode.OneWayToSource)
            {
                _reverseMaterial?.Invoke(CachedComponent.material);

                if (_reverseMaterials is not null)
                {
                    var materials = CachedComponent.materials;
                    
                    if (_converter is not null)
                    {
                        materials = new Material[CachedComponent.materials.Length];

                        for (var i = 0; i < materials.Length; i++)
                            materials[i] = GetConvertedBackValue(CachedComponent.materials[i]);
                    }
                    
                    _reverseMaterials?.Invoke(materials);
                }
            }
        }

        /// <summary>
        /// Called when the binding is removed.
        /// Sends <see langword="null"/> to all reverse subscribers.
        /// </summary>
        protected override void OnUnbound()
        {
            _reverseMaterial?.Invoke(null);
            _reverseMaterials?.Invoke(null);
        }

        private Material GetConvertedValue(Material value) =>
            _converter?.Convert(value) ?? value;

        /// <summary>
        /// Converts a value on its way back to the ViewModel.
        /// </summary>
        /// <param name="value">The value read from the View.</param>
        /// <returns>
        /// The value as the ViewModel expects it: undone by the converter when it offers
        /// <see cref="ITwoWayConverter{TFrom, TTo}"/>, and unchanged when it does not.
        /// </returns>
        /// <remarks>
        /// The forward converter must not be applied here — it is a View-side presentation concern
        /// that must not leak back into the ViewModel.
        /// </remarks>
        private Material GetConvertedBackValue(Material value) =>
            _converter is ITwoWayConverter<Material, Material> twoWay ? twoWay.ConvertBack(value) : value;
    }
}