using System;
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent}"/> that binds <see cref="Renderer.material"/> or
    /// <see cref="Renderer.materials"/>.
    /// </summary>
    /// <remarks>
    /// In <see cref="BindMode.OneWayToSource"/> the shared materials are reported, so no instance copies are
    /// created.
    /// </remarks>
    [GenerateSerializableBinder]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    [AddBinderContextMenu(typeof(Renderer), serializePropertyNames: "m_Materials")]
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/Renderer Binder – Materials")]
    public sealed partial class RendererMaterialsMonoBinder : ComponentMonoBinder<Renderer>,
        IBinder<Material>,
        IBinder<IReadOnlyCollection<Material>>,
        IReverseBinder<Material>,
        IReverseBinder<Material[]>
    {
        [Tooltip("Optional converter applied to each material; empty leaves it as-is.")]
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

        /// <summary>
        /// Sets <see cref="Renderer.material"/>.
        /// </summary>
        /// <param name="value">The material received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(Material value) =>
            CachedComponent.material = _converter?.Convert(value) ?? value;

        /// <summary>
        /// Sets <see cref="Renderer.materials"/>; <see langword="null"/> or empty clears the array.
        /// </summary>
        /// <param name="values">The materials received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(IReadOnlyCollection<Material> values) =>
            CachedComponent.SetMaterials(_converter, values);

        /// <inheritdoc/>
        protected override void OnBound()
        {
            if (Mode is not BindMode.OneWayToSource) return;

            _reverseMaterial?.Invoke(GetConvertedBackValue(CachedComponent.sharedMaterial));

            if (_reverseMaterials is null) return;

            var materials = CachedComponent.sharedMaterials;
            for (var i = 0; i < materials.Length; i++)
                materials[i] = GetConvertedBackValue(materials[i]);

            _reverseMaterials.Invoke(materials);
        }

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            _reverseMaterial?.Invoke(null);
            _reverseMaterials?.Invoke(null);
        }

        private Material GetConvertedBackValue(Material value) =>
            _converter is ITwoWayConverter<Material, Material> twoWay ? twoWay.ConvertBack(value) : value;
    }
}
